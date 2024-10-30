const std = @import("std");
const Build = std.Build;

pub const LibType = enum { Shared, Static };

pub fn compileFlecs(options: anytype, b: *Build, lib_type: LibType) void {
    const lib = switch (lib_type) {
        .Shared => b.addSharedLibrary(.{
            .name = "flecs",
            .target = options.target,
            .optimize = options.optimize,
            .strip = options.optimize != .Debug,
        }),
        .Static => b.addStaticLibrary(.{
            .name = "flecs",
            .target = options.target,
            .optimize = options.optimize,
            .strip = options.optimize != .Debug,
        }),
    };

    lib.linkLibC();
    lib.addCSourceFile(.{ .file = b.path("../../native/flecs/distr/flecs.c"), .flags = &.{"-fno-sanitize=undefined"} });
    lib.addCSourceFile(.{ .file = b.path("../../native/flecs_helpers.c"), .flags = &.{"-fno-sanitize=undefined"} });
    lib.defineCMacro(if (options.optimize == .Debug) "FLECS_DEBUG" else "FLECS_NDEBUG", null);

    if (options.soft_assert) {
        lib.defineCMacro("FLECS_SOFT_ASSERT", null);
    }

    switch (options.target.result.os.tag) {
        .windows => {
            lib.linkSystemLibrary("ws2_32");
        },
        .ios => {
            if (b.sysroot == null) {
                @panic("A --sysroot path to an IOS SDK needs to be provided when compiling for IOS.");
            }

            lib.addSystemFrameworkPath(.{ .cwd_relative = b.pathJoin(&.{ b.sysroot.?, "/System/Library/Frameworks" }) });
            lib.addSystemIncludePath(.{ .cwd_relative = b.pathJoin(&.{ b.sysroot.?, "/usr/include" }) });
            lib.addLibraryPath(.{ .cwd_relative = b.pathJoin(&.{ b.sysroot.?, "/usr/lib" }) });
        },
        .emscripten => {
            if (b.sysroot == null) {
                @panic("Pass '--sysroot \"$EMSDK/upstream/emscripten\"'");
            }

            const cache_include = b.pathJoin(&.{ b.sysroot.?, "cache", "sysroot", "include" });
            var dir = std.fs.openDirAbsolute(cache_include, std.fs.Dir.OpenDirOptions{ .access_sub_paths = true, .no_follow = true }) catch @panic("No emscripten cache. Generate it!");
            dir.close();
            lib.addIncludePath(.{ .cwd_relative = cache_include });
        },
        else => {},
    }

    b.installArtifact(lib);
}

pub fn build(b: *Build) void {
    const options = .{
        .optimize = b.standardOptimizeOption(.{}),
        .target = b.standardTargetOptions(.{}),
        .soft_assert = b.option(bool, "soft-assert", "Compile with the FLECS_SOFT_ASSERT define.") orelse false,
    };

    if (options.target.result.os.tag != .emscripten) {
        compileFlecs(options, b, LibType.Shared);
    }

    compileFlecs(options, b, LibType.Static);
}
