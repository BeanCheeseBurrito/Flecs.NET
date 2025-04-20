const std = @import("std");
const builtin = @import("builtin");
const Build = std.Build;

pub const LibraryType = enum { Shared, Static };

const BuildOptions = struct {
    optimize: std.builtin.OptimizeMode,
    target: Build.ResolvedTarget,
    library_type: LibraryType,
    compiler_rt_path: ?[]const u8,
};

const src_flags = [_][]const u8{
    "-std=c99",
    "-fno-sanitize=undefined",
};

const src_files = [_][]const u8{
    "../../native/flecs_helpers.c",

    "../../native/flecs/src/bootstrap.c",
    "../../native/flecs/src/each.c",
    "../../native/flecs/src/entity_name.c",
    "../../native/flecs/src/entity.c",
    "../../native/flecs/src/id.c",
    "../../native/flecs/src/iter.c",
    "../../native/flecs/src/misc.c",
    "../../native/flecs/src/observable.c",
    "../../native/flecs/src/observer.c",
    "../../native/flecs/src/os_api.c",
    "../../native/flecs/src/poly.c",
    "../../native/flecs/src/search.c",
    "../../native/flecs/src/stage.c",
    "../../native/flecs/src/value.c",
    "../../native/flecs/src/world.c",
    "../../native/flecs/src/addons/alerts.c",
    "../../native/flecs/src/addons/app.c",
    "../../native/flecs/src/addons/doc.c",
    "../../native/flecs/src/addons/flecs_cpp.c",
    "../../native/flecs/src/addons/http.c",
    "../../native/flecs/src/addons/journal.c",
    "../../native/flecs/src/addons/log.c",
    "../../native/flecs/src/addons/metrics.c",
    "../../native/flecs/src/addons/module.c",
    "../../native/flecs/src/addons/rest.c",
    "../../native/flecs/src/addons/timer.c",
    "../../native/flecs/src/addons/units.c",
    "../../native/flecs/src/addons/json/deserialize_value.c",
    "../../native/flecs/src/addons/json/deserialize.c",
    "../../native/flecs/src/addons/json/json.c",
    "../../native/flecs/src/addons/json/serialize_entity.c",
    "../../native/flecs/src/addons/json/serialize_field_info.c",
    "../../native/flecs/src/addons/json/serialize_iter_result_query.c",
    "../../native/flecs/src/addons/json/serialize_iter_result_table.c",
    "../../native/flecs/src/addons/json/serialize_iter_result.c",
    "../../native/flecs/src/addons/json/serialize_iter.c",
    "../../native/flecs/src/addons/json/serialize_query_info.c",
    "../../native/flecs/src/addons/json/serialize_type_info.c",
    "../../native/flecs/src/addons/json/serialize_value.c",
    "../../native/flecs/src/addons/json/serialize_world.c",
    "../../native/flecs/src/addons/meta/api.c",
    "../../native/flecs/src/addons/meta/c_utils.c",
    "../../native/flecs/src/addons/meta/cursor.c",
    "../../native/flecs/src/addons/meta/definitions.c",
    "../../native/flecs/src/addons/meta/meta.c",
    "../../native/flecs/src/addons/meta/rtt_lifecycle.c",
    "../../native/flecs/src/addons/meta/serialized.c",
    "../../native/flecs/src/addons/os_api_impl/os_api_impl.c",
    "../../native/flecs/src/addons/parser/tokenizer.c",
    "../../native/flecs/src/addons/pipeline/pipeline.c",
    "../../native/flecs/src/addons/pipeline/worker.c",
    "../../native/flecs/src/addons/query_dsl/parser.c",
    "../../native/flecs/src/addons/script/ast.c",
    "../../native/flecs/src/addons/script/function.c",
    "../../native/flecs/src/addons/script/functions_builtin.c",
    "../../native/flecs/src/addons/script/functions_math.c",
    "../../native/flecs/src/addons/script/parser.c",
    "../../native/flecs/src/addons/script/script.c",
    "../../native/flecs/src/addons/script/serialize.c",
    "../../native/flecs/src/addons/script/template.c",
    "../../native/flecs/src/addons/script/vars.c",
    "../../native/flecs/src/addons/script/visit_check.c",
    "../../native/flecs/src/addons/script/visit_eval.c",
    "../../native/flecs/src/addons/script/visit_free.c",
    "../../native/flecs/src/addons/script/visit_to_str.c",
    "../../native/flecs/src/addons/script/visit.c",
    "../../native/flecs/src/addons/script/expr/ast.c",
    "../../native/flecs/src/addons/script/expr/parser.c",
    "../../native/flecs/src/addons/script/expr/stack.c",
    "../../native/flecs/src/addons/script/expr/util.c",
    "../../native/flecs/src/addons/script/expr/visit_eval.c",
    "../../native/flecs/src/addons/script/expr/visit_fold.c",
    "../../native/flecs/src/addons/script/expr/visit_free.c",
    "../../native/flecs/src/addons/script/expr/visit_to_str.c",
    "../../native/flecs/src/addons/script/expr/visit_type.c",
    "../../native/flecs/src/addons/stats/monitor.c",
    "../../native/flecs/src/addons/stats/pipeline_monitor.c",
    "../../native/flecs/src/addons/stats/stats.c",
    "../../native/flecs/src/addons/stats/system_monitor.c",
    "../../native/flecs/src/addons/stats/world_monitor.c",
    "../../native/flecs/src/addons/stats/world_summary.c",
    "../../native/flecs/src/addons/system/system.c",
    "../../native/flecs/src/datastructures/allocator.c",
    "../../native/flecs/src/datastructures/bitset.c",
    "../../native/flecs/src/datastructures/block_allocator.c",
    "../../native/flecs/src/datastructures/hash.c",
    "../../native/flecs/src/datastructures/hashmap.c",
    "../../native/flecs/src/datastructures/map.c",
    "../../native/flecs/src/datastructures/name_index.c",
    "../../native/flecs/src/datastructures/sparse.c",
    "../../native/flecs/src/datastructures/stack_allocator.c",
    "../../native/flecs/src/datastructures/strbuf.c",
    "../../native/flecs/src/datastructures/switch_list.c",
    "../../native/flecs/src/datastructures/vec.c",
    "../../native/flecs/src/query/api.c",
    "../../native/flecs/src/query/util.c",
    "../../native/flecs/src/query/validator.c",
    "../../native/flecs/src/query/compiler/compiler_term.c",
    "../../native/flecs/src/query/compiler/compiler.c",
    "../../native/flecs/src/query/engine/cache_iter.c",
    "../../native/flecs/src/query/engine/cache_order_by.c",
    "../../native/flecs/src/query/engine/cache.c",
    "../../native/flecs/src/query/engine/change_detection.c",
    "../../native/flecs/src/query/engine/eval_iter.c",
    "../../native/flecs/src/query/engine/eval_member.c",
    "../../native/flecs/src/query/engine/eval_pred.c",
    "../../native/flecs/src/query/engine/eval_toggle.c",
    "../../native/flecs/src/query/engine/eval_trav.c",
    "../../native/flecs/src/query/engine/eval_union.c",
    "../../native/flecs/src/query/engine/eval_up.c",
    "../../native/flecs/src/query/engine/eval_utils.c",
    "../../native/flecs/src/query/engine/eval.c",
    "../../native/flecs/src/query/engine/trav_cache.c",
    "../../native/flecs/src/query/engine/trav_down_cache.c",
    "../../native/flecs/src/query/engine/trav_up_cache.c",
    "../../native/flecs/src/query/engine/trivial_iter.c",
    "../../native/flecs/src/storage/entity_index.c",
    "../../native/flecs/src/storage/id_index.c",
    "../../native/flecs/src/storage/table_cache.c",
    "../../native/flecs/src/storage/table_graph.c",
    "../../native/flecs/src/storage/table.c",
};

pub fn compileFlecs(b: *Build, options: BuildOptions) void {
    const lib = switch (options.library_type) {
        .Shared => b.addSharedLibrary(.{
            .name = "flecs",
            .target = options.target,
            .optimize = options.optimize,
            .strip = options.optimize != .Debug,
            .link_libc = true,
        }),
        .Static => b.addStaticLibrary(.{
            .name = "flecs",
            .target = options.target,
            .optimize = options.optimize,
            .strip = options.optimize != .Debug,
            .link_libc = true,
            .root_source_file = if (options.compiler_rt_path) |path| .{ .cwd_relative = path } else null,
        }),
    };

    lib.defineCMacro(if (options.optimize == .Debug) "FLECS_DEBUG" else "FLECS_NDEBUG", null);
    lib.defineCMacro(if (options.library_type == LibraryType.Shared) "flecs_EXPORTS" else "flecs_STATIC", null);

    lib.addIncludePath(b.path("../../native/flecs/include"));

    for (src_files) |file| {
        lib.addCSourceFile(.{ .file = b.path(file), .flags = &src_flags });
    }

    switch (options.target.result.os.tag) {
        .windows => {
            lib.linkSystemLibrary("ws2_32");

            // Temporary fix to get rid of undefined symbol errors when statically linking in Native AOT.
            if (options.library_type == LibraryType.Static) {
                lib.addCSourceFile(.{ .file = b.path("../../native/windows.c"), .flags = &src_flags });
            }
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
        .linux => {
            if (options.target.result.abi == .android) {
                if (b.sysroot == null) {
                    @panic("A --sysroot path to an Android NDK needs to be provided when compiling for Android.");
                }

                const host_tuple = switch (builtin.target.os.tag) {
                    .linux => "linux-x86_64",
                    .windows => "windows-x86_64",
                    .macos => "darwin-x86_64",
                    else => @panic("unsupported host OS"),
                };

                const triple = switch (options.target.result.cpu.arch) {
                    .aarch64 => "aarch64-linux-android",
                    .x86_64 => "x86_64-linux-android",
                    else => @panic("Unsupported Android architecture"),
                };
                const android_api_level: []const u8 = "21";

                const android_sysroot = b.pathJoin(&.{ b.sysroot.?, "/toolchains/llvm/prebuilt/", host_tuple, "/sysroot" });
                const android_lib_path = b.pathJoin(&.{ android_sysroot, "/usr/lib/", triple, android_api_level });
                const android_include_path = b.pathJoin(&.{ android_sysroot, "/usr/include" });
                const android_system_include_path = b.pathJoin(&.{ android_sysroot, "/usr/include/", triple });

                lib.addLibraryPath(.{ .cwd_relative = android_lib_path });

                const libc_file_name = "android-libc.conf";
                var libc_content = std.ArrayList(u8).init(b.allocator);
                errdefer libc_content.deinit();

                const writer = libc_content.writer();
                const libc_installation = std.zig.LibCInstallation{
                    .include_dir = android_include_path,
                    .sys_include_dir = android_system_include_path,
                    .crt_dir = android_lib_path,
                };
                libc_installation.render(writer) catch @panic("Failed to render libc");
                const libc_path = b.addWriteFiles().add(libc_file_name, libc_content.items);

                lib.setLibCFile(libc_path);
                lib.libc_file.?.addStepDependencies(&lib.step);
            }
        },
        else => {},
    }

    b.installArtifact(lib);
}

pub fn build(b: *Build) void {
    compileFlecs(b, .{
        .optimize = b.standardOptimizeOption(.{}),
        .target = b.standardTargetOptions(.{}),
        .library_type = b.option(LibraryType, "library-type", "Compile as a static or shared library.") orelse LibraryType.Shared,
        // When building static libraries for Windows, zig's compiler-rt needs to be bundled.
        // For some reason, setting "bundle_compiler_rt" to true doesn't produce a static library that works with NativeAOT.
        // As a work-around, we manually build the compiler_rt.zig file alongside flecs.
        .compiler_rt_path = b.option([]const u8, "compiler-rt-path", "Path to the compiler_rt file.") orelse null,
    });
}
