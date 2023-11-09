const std = @import("std");
const Build = std.build;

pub const Options = struct {
    ios_sdk_path: ?[]const u8 = null,
    ios_simulator_sdk_path: ?[]const u8 = null,
};

pub fn compileFlecs(options: Options, b: *Build, lib: *Build.Step.Compile) void {
    lib.addCSourceFile(.{ .file = Build.LazyPath.relative("../../submodules/flecs/flecs.c"), .flags = &.{} });
    lib.linkLibC();
    lib.strip = lib.optimize != .Debug;

    if (lib.optimize != .Debug) {
        lib.defineCMacro("NDEBUG", null);
    }

    switch (lib.target.getOsTag()) {
        .windows => {
            lib.linkSystemLibraryName("ws2_32");
        },
        .ios => {
            if (options.ios_sdk_path) |ios_sdk_path| {
                b.sysroot = ios_sdk_path;
                lib.addSystemIncludePath(.{ .path = b.fmt("{s}/usr/include", .{ios_sdk_path}) });
            } else {
                @panic("A path to an IOS SDK needs to be provided when compiling for IOS.");
            }
        },
        else => {},
    }

    b.installArtifact(lib);
}

pub fn build(b: *Build) void {
    const name = "flecs";
    const target = b.standardTargetOptions(.{});
    const optimize = b.standardOptimizeOption(.{});
    const options = Options{ .ios_sdk_path = b.option([]const u8, "ios-sdk-path", "Path to an IOS SDK."), .ios_simulator_sdk_path = b.option([]const u8, "ios-simulator-sdk-path", "Path to an IOS simulator SDK.") };

    compileFlecs(options, b, b.addSharedLibrary(.{ .name = name, .target = target, .optimize = optimize }));
    compileFlecs(options, b, b.addStaticLibrary(.{ .name = name, .target = target, .optimize = optimize }));
}
