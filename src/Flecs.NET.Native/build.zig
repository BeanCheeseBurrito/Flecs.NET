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

    lib.addCSourceFile(.{ .file = b.path("../../submodules/flecs/flecs.c"), .flags = &.{} });
    lib.linkLibC();

    if (options.optimize != .Debug) {
        lib.defineCMacro("NDEBUG", null);
    }

    if (options.soft_assert) {
        lib.defineCMacro("FLECS_SOFT_ASSERT", null);
    }

    switch (options.target.result.os.tag) {
        .windows => {
            lib.linkSystemLibrary("ws2_32");
        },
        .ios => {
            lib.addIncludePath(.{ .cwd_relative = "/usr/include" });
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
        .ios_sdk_path = b.option([]const u8, "ios-sdk-path", "Path to an IOS SDK."),
        .ios_simulator_sdk_path = b.option([]const u8, "ios-simulator-sdk-path", "Path to an IOS simulator SDK."),
    };

    compileFlecs(options, b, LibType.Shared);
    compileFlecs(options, b, LibType.Static);
}
