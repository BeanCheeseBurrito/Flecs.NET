namespace Flecs.NET.Bindings
{
    public static unsafe partial class Native
    {
        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_abort_(int error_code, byte* file, int line, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_add_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_add_path_w_sep(ecs_world_t* world, ulong entity, ulong parent, byte* path, byte* sep, byte* prefix);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_alert_init(ecs_world_t* world, ecs_alert_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_app_run(ecs_world_t* world, ecs_app_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_app_run_frame(ecs_world_t* world, ecs_app_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_app_set_frame_action(System.IntPtr callback);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_app_set_run_action(System.IntPtr callback);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_array_init(ecs_world_t* world, ecs_array_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_array_to_json(ecs_world_t* world, ulong type, void* data, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_array_to_json_buf(ecs_world_t* world, ulong type, void* data, int count, ecs_strbuf_t* buf_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_asprintf(byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_assert_(byte condition, int error_code, byte* condition_str, byte* file, int line, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_astresc(byte delimiter, byte* @in);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_async_stage_free(ecs_world_t* stage);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_t* ecs_async_stage_new(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_atfini(ecs_world_t* world, System.IntPtr action, void* ctx);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_bitmask_init(ecs_world_t* world, ecs_bitmask_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* ecs_bulk_init(ecs_world_t* world, ecs_bulk_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* ecs_bulk_new_w_id(ecs_world_t* world, ulong id, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_children(ecs_world_t* world, ulong parent);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_children_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_chresc(byte* @out, byte @in, byte delimiter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_chrparse(byte* @in, byte* @out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_clear(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_clone(ecs_world_t* world, ulong dst, ulong src, byte copy_value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_commit(ecs_world_t* world, ulong entity, ecs_record_t* record, ecs_table_t* table, ecs_type_t* added, ecs_type_t* removed);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_component_init(ecs_world_t* world, ecs_component_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_count_id(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_cpp_component_register(ecs_world_t* world, ulong id, byte* name, byte* symbol, int size, int alignment, byte implicit_name, byte* existing_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_cpp_component_register_explicit(ecs_world_t* world, ulong s_id, ulong id, byte* name, byte* type_name, byte* symbol, ulong size, ulong alignment, byte is_component, byte* existing_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_cpp_component_validate(ecs_world_t* world, ulong id, byte* name, byte* symbol, ulong size, ulong alignment, byte implicit_name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_cpp_enum_constant_register(ecs_world_t* world, ulong parent, ulong id, byte* name, int value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_cpp_enum_init(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_cpp_get_constant_name(byte* constant_name, byte* func_name, ulong len, ulong back_len);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_cpp_get_symbol_name(byte* symbol_name, byte* type_name, ulong len);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_cpp_get_type_name(byte* type_name, byte* func_name, ulong len, ulong front_len);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_member_t* ecs_cpp_last_member(ecs_world_t* world, ulong type);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_cpp_reset_count_get();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_cpp_reset_count_inc();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_cpp_trim_module(ecs_world_t* world, byte* type_name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_default_ctor(void* ptr, int count, ecs_type_info_t* ctx);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_defer_begin(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_defer_end(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_defer_resume(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_defer_suspend(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_delete(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_delete_empty_tables(ecs_world_t* world, ulong id, ushort clear_generation, ushort delete_generation, int min_id_count, double time_budget_seconds);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_delete_with(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_deprecated_(byte* file, int line, byte* msg);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_dim(ecs_world_t* world, int entity_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_doc_get_brief(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_doc_get_color(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_doc_get_detail(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_doc_get_link(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_doc_get_name(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_doc_set_brief(ecs_world_t* world, ulong entity, byte* description);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_doc_set_color(ecs_world_t* world, ulong entity, byte* color);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_doc_set_detail(ecs_world_t* world, ulong entity, byte* description);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_doc_set_link(ecs_world_t* world, ulong entity, byte* link);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_doc_set_name(ecs_world_t* world, ulong entity, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_emit(ecs_world_t* world, ecs_event_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_emplace_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_enable(ecs_world_t* world, ulong entity, byte enabled);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_enable_id(ecs_world_t* world, ulong entity, ulong id, byte enable);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_enable_range_check(ecs_world_t* world, byte enable);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_ensure(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_ensure_id(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_entity_from_json(ecs_world_t* world, ulong entity, byte* json, ecs_from_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_entity_init(ecs_world_t* world, ecs_entity_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_entity_str(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_entity_to_json(ecs_world_t* world, ulong entity, ecs_entity_to_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_entity_to_json_buf(ecs_world_t* world, ulong entity, ecs_strbuf_t* buf_out, ecs_entity_to_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_enum_init(ecs_world_t* world, ecs_enum_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_exists(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_field_column_index(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_field_id(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_field_is_readonly(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_field_is_self(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_field_is_set(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_field_is_writeonly(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_field_size(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_field_src(ecs_iter_t* it, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_field_w_size(ecs_iter_t* it, ulong size, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_filter_chain_iter(ecs_iter_t* it, ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_filter_copy(ecs_filter_t* dst, ecs_filter_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_filter_finalize(ecs_world_t* world, ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_filter_find_this_var(ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_filter_fini(ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_filter_t* ecs_filter_init(ecs_world_t* world, ecs_filter_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_filter_iter(ecs_world_t* world, ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_filter_move(ecs_filter_t* dst, ecs_filter_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_filter_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_filter_next_instanced(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_filter_pivot_term(ecs_world_t* world, ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_filter_str(ecs_world_t* world, ecs_filter_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_fini(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_flatten(ecs_world_t* world, ulong pair, ecs_flatten_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern float ecs_frame_begin(ecs_world_t* world, float delta_time);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_frame_end(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_alert(ecs_world_t* world, ulong entity, ulong alert);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_get_alert_count(ecs_world_t* world, ulong entity, ulong alert);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_alive(ecs_world_t* world, ulong e);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_context(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_get_depth(ecs_world_t* world, ulong entity, ulong rel);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_entity(void* poly);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_type_hooks_t* ecs_get_hooks_id(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern float ecs_get_interval(ecs_world_t* world, ulong tick_source);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* ecs_get_lookup_path(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_max_id(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_mut_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_get_name(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_observer_binding_ctx(ecs_world_t* world, ulong observer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_observer_ctx(ecs_world_t* world, ulong observer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_parent(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_get_path_w_sep(ecs_world_t* world, ulong parent, ulong child, byte* sep, byte* prefix);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_get_path_w_sep_buf(ecs_world_t* world, ulong parent, ulong child, byte* sep, byte* prefix, ecs_strbuf_t* buf);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_pipeline(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_scope(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_t* ecs_get_stage(ecs_world_t* world, int stage_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_get_stage_count(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_get_stage_id(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_get_symbol(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_system_binding_ctx(ecs_world_t* world, ulong system);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_get_system_ctx(ecs_world_t* world, ulong system);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_table_t* ecs_get_table(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_target(ecs_world_t* world, ulong entity, ulong rel, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_target_for_id(ecs_world_t* world, ulong entity, ulong rel, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern float ecs_get_timeout(ecs_world_t* world, ulong tick_source);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_type_t* ecs_get_type(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_type_info_t* ecs_get_type_info(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_typeid(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_get_with(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_t* ecs_get_world(void* poly);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_info_t* ecs_get_world_info(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_has_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_http_get_header(ecs_http_request_t* req, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_http_get_param(ecs_http_request_t* req, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_http_server_ctx(ecs_http_server_t* srv);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_http_server_dequeue(ecs_http_server_t* server, float delta_time);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_http_server_fini(ecs_http_server_t* server);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_http_server_http_request(ecs_http_server_t* srv, byte* req, int len, ecs_http_reply_t* reply_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_http_server_t* ecs_http_server_init(ecs_http_server_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_http_server_request(ecs_http_server_t* srv, byte* method, byte* req, ecs_http_reply_t* reply_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_http_server_start(ecs_http_server_t* server);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_http_server_stop(ecs_http_server_t* server);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_id_flag_str(ulong id_flags);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern uint ecs_id_get_flags(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_in_use(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_is_pair(ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_is_tag(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_is_union(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_is_valid(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_is_wildcard(ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_id_match(ulong id, ulong pattern);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_id_str(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_id_str_buf(ecs_world_t* world, ulong id, ecs_strbuf_t* buf);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_identifier_is_0(byte* id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_import(ecs_world_t* world, System.IntPtr module, byte* module_name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_import_c(ecs_world_t* world, System.IntPtr module, byte* module_name_c);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_import_from_library(ecs_world_t* world, byte* library_name, byte* module_name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_t* ecs_init();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_t* ecs_init_w_args(int argc, byte** argv);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_interpolate_string(ecs_world_t* world, byte* str, ecs_vars_t* vars);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_is_alive(ecs_world_t* world, ulong e);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_is_deferred(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_is_enabled_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_is_fini(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_is_valid(ecs_world_t* world, ulong e);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_iter_count(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_iter_fini(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_iter_first(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_iter_get_var(ecs_iter_t* it, int var_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_table_range_t ecs_iter_get_var_as_range(ecs_iter_t* it, int var_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_table_t* ecs_iter_get_var_as_table(ecs_iter_t* it, int var_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_iter_is_true(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_iter_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_iter_poly(ecs_world_t* world, void* poly, ecs_iter_t* iter, ecs_term_t* filter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_iter_set_var(ecs_iter_t* it, int var_id, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_iter_set_var_as_range(ecs_iter_t* it, int var_id, ecs_table_range_t* range);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_iter_set_var_as_table(ecs_iter_t* it, int var_id, ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_iter_str(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_iter_to_json(ecs_world_t* world, ecs_iter_t* iter, ecs_iter_to_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_iter_to_json_buf(ecs_world_t* world, ecs_iter_t* iter, ecs_strbuf_t* buf_out, ecs_iter_to_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_iter_to_vars(ecs_iter_t* it, ecs_vars_t* vars, int offset);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_iter_var_is_constrained(ecs_iter_t* it, int var_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_log_(int level, byte* file, int line, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_log_enable_colors(byte enabled);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_log_enable_timedelta(byte enabled);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_log_enable_timestamp(byte enabled);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_log_get_level();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_log_last_error();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_log_pop_(int level);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_log_push_(int level);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_log_set_level(int level);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_logv_(int level, byte* file, int line, byte* fmt, void* args);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_lookup(ecs_world_t* world, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_lookup_child(ecs_world_t* world, ulong parent, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_lookup_path_w_sep(ecs_world_t* world, ulong parent, byte* path, byte* sep, byte* prefix, byte recursive);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_lookup_symbol(ecs_world_t* world, byte* symbol, byte lookup_as_path, byte recursive);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_make_pair(ulong first, ulong second);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_clear(ecs_map_t* map);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_copy(ecs_map_t* dst, ecs_map_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* ecs_map_ensure(ecs_map_t* map, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_map_ensure_alloc(ecs_map_t* map, int elem_size, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_fini(ecs_map_t* map);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* ecs_map_get(ecs_map_t* map, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_map_get_deref_(ecs_map_t* map, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_init(ecs_map_t* map, ecs_allocator_t* allocator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_init_if(ecs_map_t* map, ecs_allocator_t* allocator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_init_w_params(ecs_map_t* map, ecs_map_params_t* @params);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_init_w_params_if(ecs_map_t* result, ecs_map_params_t* @params);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_insert(ecs_map_t* map, ulong key, ulong value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_map_insert_alloc(ecs_map_t* map, int elem_size, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_map_iter_t ecs_map_iter(ecs_map_t* map);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_map_next(ecs_map_iter_t* iter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_params_fini(ecs_map_params_t* @params);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_params_init(ecs_map_params_t* @params, ecs_allocator_t* allocator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_map_remove(ecs_map_t* map, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_map_remove_free(ecs_map_t* map, ulong key);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_measure_frame_time(ecs_world_t* world, byte enable);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_measure_system_time(ecs_world_t* world, byte enable);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_merge(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_meta_cursor_t ecs_meta_cursor(ecs_world_t* world, ulong type, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_dotmember(ecs_meta_cursor_t* cursor, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_elem(ecs_meta_cursor_t* cursor, int elem);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_from_desc(ecs_world_t* world, ulong component, ecs_type_kind_t kind, byte* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_meta_get_bool(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_meta_get_char(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_meta_get_entity(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern double ecs_meta_get_float(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern long ecs_meta_get_int(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_meta_get_member(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_meta_get_ptr(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_meta_get_string(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_meta_get_type(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_meta_get_uint(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_meta_get_unit(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_meta_is_collection(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_member(ecs_meta_cursor_t* cursor, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_next(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_pop(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern double ecs_meta_ptr_to_float(ecs_primitive_kind_t type_kind, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_push(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_bool(ecs_meta_cursor_t* cursor, byte value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_char(ecs_meta_cursor_t* cursor, byte value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_entity(ecs_meta_cursor_t* cursor, ulong value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_float(ecs_meta_cursor_t* cursor, double value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_int(ecs_meta_cursor_t* cursor, long value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_null(ecs_meta_cursor_t* cursor);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_string(ecs_meta_cursor_t* cursor, byte* value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_string_literal(ecs_meta_cursor_t* cursor, byte* value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_uint(ecs_meta_cursor_t* cursor, ulong value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_meta_set_value(ecs_meta_cursor_t* cursor, ecs_value_t* value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_metric_copy(ecs_metric_t* m, int dst, int src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_metric_init(ecs_world_t* world, ecs_metric_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_metric_reduce(ecs_metric_t* dst, ecs_metric_t* src, int t_dst, int t_src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_metric_reduce_last(ecs_metric_t* m, int t, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_world_t* ecs_mini();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_modified_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_module_init(ecs_world_t* world, byte* c_name, ecs_component_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_module_path_from_c(byte* c_name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_new_from_path_w_sep(ecs_world_t* world, ulong parent, byte* path, byte* sep, byte* prefix);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_new_id(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_new_low_id(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_new_w_id(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_new_w_table(ecs_world_t* world, ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_observer_default_run_action(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_observer_init(ecs_world_t* world, ecs_observer_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_opaque_init(ecs_world_t* world, ecs_opaque_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_dbg(byte* file, int line, byte* msg);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_err(byte* file, int line, byte* msg);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_fatal(byte* file, int line, byte* msg);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_fini();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_os_api_t ecs_os_get_api();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_dl();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_heap();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_logging();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_modules();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_task_support();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_threading();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_os_has_time();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_init();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_os_memdup(void* src, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_set_api(ecs_os_api_t* os_api);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_set_api_defaults();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_os_strerror(int err);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_strset(byte** str, byte* value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_trace(byte* file, int line, byte* msg);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_os_warn(byte* file, int line, byte* msg);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_override_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_owns_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_page_iter(ecs_iter_t* it, int offset, int limit);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_page_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_digit(byte* ptr, byte* token);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_expr(ecs_world_t* world, byte* ptr, ecs_value_t* value, ecs_parse_expr_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_expr_token(byte* name, byte* expr, byte* ptr, byte* token);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_identifier(byte* name, byte* expr, byte* ptr, byte* token_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_term(ecs_world_t* world, byte* name, byte* expr, byte* ptr, ecs_term_t* term_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_token(byte* name, byte* expr, byte* ptr, byte* token_out, byte delim);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_ws(byte* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_parse_ws_eol(byte* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_parser_error_(byte* name, byte* expr, long column, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_parser_errorv_(byte* name, byte* expr, long column, byte* fmt, void* args);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_pipeline_init(ecs_world_t* world, ecs_pipeline_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_pipeline_stats_copy_last(ecs_pipeline_stats_t* dst, ecs_pipeline_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_pipeline_stats_fini(ecs_pipeline_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_pipeline_stats_get(ecs_world_t* world, ulong pipeline, ecs_pipeline_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_pipeline_stats_reduce(ecs_pipeline_stats_t* dst, ecs_pipeline_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_pipeline_stats_reduce_last(ecs_pipeline_stats_t* stats, ecs_pipeline_stats_t* old, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_pipeline_stats_repeat_last(ecs_pipeline_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_plecs_from_file(ecs_world_t* world, byte* filename);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_plecs_from_str(ecs_world_t* world, byte* name, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_poly_is_(void* @object, int type);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_primitive_init(ecs_world_t* world, ecs_primitive_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_primitive_to_expr_buf(ecs_world_t* world, ecs_primitive_kind_t kind, void* data, ecs_strbuf_t* buf);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_print_(int level, byte* file, int line, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_printv_(int level, byte* file, int line, byte* fmt, void* args);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_progress(ecs_world_t* world, float delta_time);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_ptr_from_json(ecs_world_t* world, ulong type, void* ptr, byte* json, ecs_from_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_ptr_to_expr(ecs_world_t* world, ulong type, void* data);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_ptr_to_expr_buf(ecs_world_t* world, ulong type, void* data, ecs_strbuf_t* buf);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_ptr_to_json(ecs_world_t* world, ulong type, void* data);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_ptr_to_json_buf(ecs_world_t* world, ulong type, void* data, ecs_strbuf_t* buf_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_ptr_to_str(ecs_world_t* world, ulong type, void* data);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_ptr_to_str_buf(ecs_world_t* world, ulong type, void* data, ecs_strbuf_t* buf);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_quantity_init(ecs_world_t* world, ecs_entity_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_query_changed(ecs_query_t* query, ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_query_empty_table_count(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_query_entity_count(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_fini(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_filter_t* ecs_query_get_filter(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_query_get_group_ctx(ecs_query_t* query, ulong group_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_query_group_info_t* ecs_query_get_group_info(ecs_query_t* query, ulong group_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_query_t* ecs_query_init(ecs_world_t* world, ecs_query_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_query_iter(ecs_world_t* world, ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_query_next(ecs_iter_t* iter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_query_next_instanced(ecs_iter_t* iter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_query_next_table(ecs_iter_t* iter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_query_orphaned(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_query_populate(ecs_iter_t* iter, byte when_changed);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_set_group(ecs_iter_t* it, ulong group_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_skip(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_stats_copy_last(ecs_query_stats_t* dst, ecs_query_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_stats_get(ecs_world_t* world, ecs_query_t* query, ecs_query_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_stats_reduce(ecs_query_stats_t* dst, ecs_query_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_stats_reduce_last(ecs_query_stats_t* stats, ecs_query_stats_t* old, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_query_stats_repeat_last(ecs_query_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_query_str(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_query_table_count(ecs_query_t* query);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_quit(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_record_t* ecs_read_begin(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_read_end(ecs_record_t* record);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_readonly_begin(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_readonly_end(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_record_t* ecs_record_find(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_record_get_column(ecs_record_t* r, int column, ulong c_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_record_get_entity(ecs_record_t* record);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_record_get_id(ecs_world_t* world, ecs_record_t* record, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_record_get_mut_id(ecs_world_t* world, ecs_record_t* record, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_record_has_id(ecs_world_t* world, ecs_record_t* record, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_ref_get_id(ecs_world_t* world, ecs_ref_t* @ref, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_ref_t ecs_ref_init_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_ref_update(ecs_world_t* world, ecs_ref_t* @ref);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_remove_all(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_remove_id(ecs_world_t* world, ulong entity, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_reset_clock(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_rest_server_fini(ecs_http_server_t* srv);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_http_server_t* ecs_rest_server_init(ecs_world_t* world, ecs_http_server_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_rule_find_var(ecs_rule_t* rule, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_rule_fini(ecs_rule_t* rule);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_filter_t* ecs_rule_get_filter(ecs_rule_t* rule);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_rule_t* ecs_rule_init(ecs_world_t* world, ecs_filter_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_rule_iter(ecs_world_t* world, ecs_rule_t* rule);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_rule_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_rule_next_instanced(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_rule_parse_vars(ecs_rule_t* rule, ecs_iter_t* it, byte* expr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_rule_str(ecs_rule_t* rule);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_rule_str_w_profile(ecs_rule_t* rule, ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_rule_var_count(ecs_rule_t* rule);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_rule_var_is_entity(ecs_rule_t* rule, int var_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_rule_var_name(ecs_rule_t* rule, int var_id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_run(ecs_world_t* world, ulong system, float delta_time, void* param);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_run_aperiodic(ecs_world_t* world, uint flags);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_run_pipeline(ecs_world_t* world, ulong pipeline, float delta_time);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_run_post_frame(ecs_world_t* world, System.IntPtr action, void* ctx);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_run_w_filter(ecs_world_t* world, ulong system, float delta_time, int offset, int limit, void* param);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_run_worker(ecs_world_t* world, ulong system, int stage_current, int stage_count, float delta_time, void* param);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_script_clear(ecs_world_t* world, ulong script, ulong instance);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_script_init(ecs_world_t* world, ecs_script_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_script_update(ecs_world_t* world, ulong script, ulong instance, byte* str, ecs_vars_t* vars);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_search(ecs_world_t* world, ecs_table_t* table, ulong id, ulong* id_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_search_offset(ecs_world_t* world, ecs_table_t* table, int offset, ulong id, ulong* id_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_search_relation(ecs_world_t* world, ecs_table_t* table, int offset, ulong id, ulong rel, uint flags, ulong* subject_out, ulong* id_out, ecs_table_record_t** tr_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_alias(ecs_world_t* world, ulong entity, byte* alias);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_automerge(ecs_world_t* world, byte automerge);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_context(ecs_world_t* world, void* ctx);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_entity_generation(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_entity_range(ecs_world_t* world, ulong id_start, ulong id_end);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_hooks_id(ecs_world_t* world, ulong id, ecs_type_hooks_t* hooks);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_id(ecs_world_t* world, ulong entity, ulong id, ulong size, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_interval(ecs_world_t* world, ulong tick_source, float interval);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* ecs_set_lookup_path(ecs_world_t* world, ulong* lookup_path);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_name(ecs_world_t* world, ulong entity, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_set_name_prefix(ecs_world_t* world, byte* prefix);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_os_api_impl();

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_pipeline(ecs_world_t* world, ulong pipeline);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_rate(ecs_world_t* world, ulong tick_source, int rate, ulong source);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_scope(ecs_world_t* world, ulong scope);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_stage_count(ecs_world_t* world, int stages);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_symbol(ecs_world_t* world, ulong entity, byte* symbol);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_target_fps(ecs_world_t* world, float fps);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_task_threads(ecs_world_t* world, int task_threads);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_threads(ecs_world_t* world, int threads);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_tick_source(ecs_world_t* world, ulong system, ulong tick_source);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_set_time_scale(ecs_world_t* world, float scale);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_timeout(ecs_world_t* world, ulong tick_source, float timeout);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_set_with(ecs_world_t* world, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_should_log(int level);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_should_quit(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_sleepf(double t);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_snapshot_free(ecs_snapshot_t* snapshot);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_snapshot_iter(ecs_snapshot_t* snapshot);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_snapshot_next(ecs_iter_t* iter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_snapshot_restore(ecs_world_t* world, ecs_snapshot_t* snapshot);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_snapshot_t* ecs_snapshot_take(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_snapshot_t* ecs_snapshot_take_w_iter(ecs_iter_t* iter);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_sparse_add(ecs_sparse_t* sparse, int elem_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_sparse_count(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_sparse_get(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_sparse_get_dense(ecs_sparse_t* sparse, int elem_size, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_sparse_init(ecs_sparse_t* sparse, int elem_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_sparse_last_id(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_stage_is_async(ecs_world_t* stage);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_stage_is_readonly(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_start_timer(ecs_world_t* world, ulong tick_source);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_stop_timer(ecs_world_t* world, ulong tick_source);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_append(ecs_strbuf_t* buffer, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendch(ecs_strbuf_t* buffer, byte ch);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendflt(ecs_strbuf_t* buffer, double v, byte nan_delim);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendint(ecs_strbuf_t* buffer, long v);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendstr(ecs_strbuf_t* buffer, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendstr_zerocpy(ecs_strbuf_t* buffer, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendstr_zerocpy_const(ecs_strbuf_t* buffer, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendstr_zerocpyn(ecs_strbuf_t* buffer, byte* str, int n);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendstr_zerocpyn_const(ecs_strbuf_t* buffer, byte* str, int n);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_appendstrn(ecs_strbuf_t* buffer, byte* str, int n);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_strbuf_get(ecs_strbuf_t* buffer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_strbuf_get_small(ecs_strbuf_t* buffer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_list_append(ecs_strbuf_t* buffer, byte* fmt);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_list_appendch(ecs_strbuf_t* buffer, byte ch);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_list_appendstr(ecs_strbuf_t* buffer, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_list_appendstrn(ecs_strbuf_t* buffer, byte* str, int n);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_strbuf_list_next(ecs_strbuf_t* buffer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_strbuf_list_pop(ecs_strbuf_t* buffer, byte* list_close);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_strbuf_list_push(ecs_strbuf_t* buffer, byte* list_open, byte* separator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_mergebuff(ecs_strbuf_t* dst_buffer, ecs_strbuf_t* src_buffer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_strbuf_reset(ecs_strbuf_t* buffer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_strbuf_vappend(ecs_strbuf_t* buffer, byte* fmt, void* args);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_strbuf_written(ecs_strbuf_t* buffer);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_strerror(int error_code);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_stresc(byte* @out, int size, byte delimiter, byte* @in);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_strip_generation(ulong e);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_struct_init(ecs_world_t* world, ecs_struct_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_query_t* ecs_system_get_query(ecs_world_t* world, ulong system);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_system_init(ecs_world_t* world, ecs_system_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_system_stats_copy_last(ecs_system_stats_t* dst, ecs_system_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_system_stats_get(ecs_world_t* world, ulong system, ecs_system_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_system_stats_reduce(ecs_system_stats_t* dst, ecs_system_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_system_stats_reduce_last(ecs_system_stats_t* stats, ecs_system_stats_t* old, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_system_stats_repeat_last(ecs_system_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_table_t* ecs_table_add_id(ecs_world_t* world, ecs_table_t* table, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_column_count(ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_column_to_type_index(ecs_table_t* table, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_count(ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_table_t* ecs_table_find(ecs_world_t* world, ulong* ids, int id_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_table_get_column(ecs_table_t* table, int index, int offset);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_get_column_index(ecs_world_t* world, ecs_table_t* table, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_table_get_column_size(ecs_table_t* table, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_get_depth(ecs_world_t* world, ecs_table_t* table, ulong rel);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_table_get_id(ecs_world_t* world, ecs_table_t* table, ulong id, int offset);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_type_t* ecs_table_get_type(ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_get_type_index(ecs_world_t* world, ecs_table_t* table, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_table_has_flags(ecs_table_t* table, uint flags);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_table_has_id(ecs_world_t* world, ecs_table_t* table, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_table_lock(ecs_world_t* world, ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_table_t* ecs_table_remove_id(ecs_world_t* world, ecs_table_t* table, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_table_str(ecs_world_t* world, ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_table_swap_rows(ecs_world_t* world, ecs_table_t* table, int row_1, int row_2);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_table_type_to_column_index(ecs_table_t* table, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_table_unlock(ecs_world_t* world, ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_term_chain_iter(ecs_iter_t* it, ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_term_t ecs_term_copy(ecs_term_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_term_finalize(ecs_world_t* world, ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_term_fini(ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_term_id_is_set(ecs_term_id_t* id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_term_is_initialized(ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_term_iter(ecs_world_t* world, ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_term_match_0(ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_term_match_this(ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_term_t ecs_term_move(ecs_term_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_term_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_term_str(ecs_world_t* world, ecs_term_t* term);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern double ecs_time_measure(ecs_time_t* start);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_time_t ecs_time_sub(ecs_time_t t1, ecs_time_t t2);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern double ecs_time_to_double(ecs_time_t t);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_type_info_to_json(ecs_world_t* world, ulong type);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_type_info_to_json_buf(ecs_world_t* world, ulong type, ecs_strbuf_t* buf_out);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_type_str(ecs_world_t* world, ecs_type_t* type);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_unit_init(ecs_world_t* world, ecs_unit_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_unit_prefix_init(ecs_world_t* world, ecs_unit_prefix_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_using_task_threads(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_copy(ecs_world_t* world, ulong type, void* dst, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_copy_w_type_info(ecs_world_t* world, ecs_type_info_t* ti, void* dst, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_fini(ecs_world_t* world, ulong type, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_fini_w_type_info(ecs_world_t* world, ecs_type_info_t* ti, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_free(ecs_world_t* world, ulong type, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_init(ecs_world_t* world, ulong type, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_init_w_type_info(ecs_world_t* world, ecs_type_info_t* ti, void* ptr);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_move(ecs_world_t* world, ulong type, void* dst, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_move_ctor(ecs_world_t* world, ulong type, void* dst, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_move_ctor_w_type_info(ecs_world_t* world, ecs_type_info_t* ti, void* dst, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_value_move_w_type_info(ecs_world_t* world, ecs_type_info_t* ti, void* dst, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_value_new(ecs_world_t* world, ulong type);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_value_new_w_type_info(ecs_world_t* world, ecs_type_info_t* ti);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_expr_var_t* ecs_vars_declare(ecs_vars_t* vars, byte* name, ulong type);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_expr_var_t* ecs_vars_declare_w_value(ecs_vars_t* vars, byte* name, ecs_value_t* value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vars_fini(ecs_vars_t* vars);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vars_init(ecs_world_t* world, ecs_vars_t* vars);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_expr_var_t* ecs_vars_lookup(ecs_vars_t* vars, byte* name);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_vars_pop(ecs_vars_t* vars);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vars_push(ecs_vars_t* vars);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_vasprintf(byte* fmt, void* args);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_vec_append(ecs_allocator_t* allocator, ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_clear(ecs_vec_t* vec);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_vec_t ecs_vec_copy(ecs_allocator_t* allocator, ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_vec_count(ecs_vec_t* vec);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_fini(ecs_allocator_t* allocator, ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_vec_first(ecs_vec_t* vec);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_vec_get(ecs_vec_t* vec, int size, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_vec_grow(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_vec_t* ecs_vec_init(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_init_if(ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* ecs_vec_last(ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_reclaim(ecs_allocator_t* allocator, ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_remove(ecs_vec_t* vec, int size, int elem);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_remove_last(ecs_vec_t* vec);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_vec_t* ecs_vec_reset(ecs_allocator_t* allocator, ecs_vec_t* vec, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_set_count(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_set_min_count(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_set_min_count_zeromem(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_set_min_size(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_vec_set_size(ecs_allocator_t* allocator, ecs_vec_t* vec, int size, int elem_count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_vec_size(ecs_vec_t* vec);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong ecs_vector_init(ecs_world_t* world, ecs_vector_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_iter_t ecs_worker_iter(ecs_iter_t* it, int index, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte ecs_worker_next(ecs_iter_t* it);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_world_from_json(ecs_world_t* world, byte* json, ecs_from_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_world_stats_copy_last(ecs_world_stats_t* dst, ecs_world_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_world_stats_get(ecs_world_t* world, ecs_world_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_world_stats_log(ecs_world_t* world, ecs_world_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_world_stats_reduce(ecs_world_stats_t* dst, ecs_world_stats_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_world_stats_reduce_last(ecs_world_stats_t* stats, ecs_world_stats_t* old, int count);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_world_stats_repeat_last(ecs_world_stats_t* stats);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* ecs_world_to_json(ecs_world_t* world, ecs_world_to_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int ecs_world_to_json_buf(ecs_world_t* world, ecs_strbuf_t* buf_out, ecs_world_to_json_desc_t* desc);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_record_t* ecs_write_begin(ecs_world_t* world, ulong entity);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void ecs_write_end(ecs_record_t* record);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_allocator_fini(ecs_allocator_t* a);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_block_allocator_t* flecs_allocator_get(ecs_allocator_t* a, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_allocator_init(ecs_allocator_t* a);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_balloc(ecs_block_allocator_t* allocator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_ballocator_fini(ecs_block_allocator_t* ba);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_ballocator_free(ecs_block_allocator_t* ba);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_ballocator_init(ecs_block_allocator_t* ba, int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_block_allocator_t* flecs_ballocator_new(int size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_bcalloc(ecs_block_allocator_t* allocator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_bdup(ecs_block_allocator_t* ba, void* memory);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_bfree(ecs_block_allocator_t* allocator, void* memory);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_brealloc(ecs_block_allocator_t* dst, ecs_block_allocator_t* src, void* memory);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_dup(ecs_allocator_t* a, int size, void* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hashmap_copy(ecs_hashmap_t* dst, ecs_hashmap_t* src);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern flecs_hashmap_result_t flecs_hashmap_ensure_(ecs_hashmap_t* map, int key_size, void* key, int value_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hashmap_fini(ecs_hashmap_t* map);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_hashmap_get_(ecs_hashmap_t* map, int key_size, void* key, int value_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ecs_hm_bucket_t* flecs_hashmap_get_bucket(ecs_hashmap_t* map, ulong hash);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hashmap_init_(ecs_hashmap_t* hm, int key_size, int value_size, System.IntPtr hash, System.IntPtr compare, ecs_allocator_t* allocator);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern flecs_hashmap_iter_t flecs_hashmap_iter(ecs_hashmap_t* map);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_hashmap_next_(flecs_hashmap_iter_t* it, int key_size, void* key_out, int value_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hashmap_remove_(ecs_hashmap_t* map, int key_size, void* key, int value_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hashmap_remove_w_hash_(ecs_hashmap_t* map, int key_size, void* key, int value_size, ulong hash);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hashmap_set_(ecs_hashmap_t* map, int key_size, void* key, int value_size, void* value);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_hm_bucket_remove(ecs_hashmap_t* map, ecs_hm_bucket_t* bucket, ulong hash, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_add(ecs_sparse_t* sparse, int elem_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_sparse_clear(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int flecs_sparse_count(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_ensure(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_ensure_fast(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_sparse_fini(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_get(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_get_any(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_get_dense(ecs_sparse_t* sparse, int elem_size, int index);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong* flecs_sparse_ids(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_sparse_init(ecs_sparse_t* sparse, ecs_allocator_t* allocator, ecs_block_allocator_t* page_allocator, int elem_size);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte flecs_sparse_is_alive(ecs_sparse_t* sparse, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong flecs_sparse_last_id(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern ulong flecs_sparse_new_id(ecs_sparse_t* sparse);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_sparse_remove(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_sparse_set_generation(ecs_sparse_t* sparse, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void* flecs_sparse_try(ecs_sparse_t* sparse, int elem_size, ulong id);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* flecs_strdup(ecs_allocator_t* a, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void flecs_strfree(ecs_allocator_t* a, byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int flecs_table_observed_count(ecs_table_t* table);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern byte* flecs_to_snake_case(byte* str);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsAlertsImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsCoreDocImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsDocImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsMetaImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsMetricsImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsMonitorImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsPipelineImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsRestImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsScriptImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsSystemImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsTimerImport(ecs_world_t* world);

        [System.Runtime.InteropServices.DllImport(BindgenInternal.DllImportPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void FlecsUnitsImport(ecs_world_t* world);

        public partial struct ecs_alert_desc_t
        {
            public int _canary;

            public ulong entity;

            public ecs_filter_desc_t filter;

            public byte* message;

            public byte* doc_name;

            public byte* brief;

            public ulong severity;

            public severity_filters_FixedBuffer severity_filters;

            public float retain_period;

            public ulong member;

            public ulong id;

            public byte* var;

            public partial struct severity_filters_FixedBuffer
            {
                public ecs_alert_severity_filter_t Item0;

                public ecs_alert_severity_filter_t Item1;

                public ecs_alert_severity_filter_t Item2;

                public ecs_alert_severity_filter_t Item3;
            }
        }

        public partial struct ecs_alert_severity_filter_t
        {
            public ulong severity;

            public ulong with;

            public byte* var;

            public int _var_index;
        }

        public partial struct ecs_allocator_t
        {
            public ecs_block_allocator_t chunks;

            public ecs_sparse_t sizes;
        }

        public partial struct ecs_app_desc_t
        {
            public float target_fps;

            public float delta_time;

            public int threads;

            public int frames;

            public byte enable_rest;

            public byte enable_monitor;

            public ushort port;

            public System.IntPtr init; // delegate* unmanaged<ecs_world_t*, int>

            public void* ctx;
        }

        public partial struct ecs_array_desc_t
        {
            public ulong entity;

            public ulong type;

            public int count;
        }

        public partial struct ecs_bitmask_constant_t
        {
            public byte* name;

            public uint value;

            public ulong constant;
        }

        public partial struct ecs_bitmask_desc_t
        {
            public ulong entity;

            public constants_FixedBuffer constants;

            public partial struct constants_FixedBuffer
            {
                public ecs_bitmask_constant_t Item0;

                public ecs_bitmask_constant_t Item1;

                public ecs_bitmask_constant_t Item2;

                public ecs_bitmask_constant_t Item3;

                public ecs_bitmask_constant_t Item4;

                public ecs_bitmask_constant_t Item5;

                public ecs_bitmask_constant_t Item6;

                public ecs_bitmask_constant_t Item7;

                public ecs_bitmask_constant_t Item8;

                public ecs_bitmask_constant_t Item9;

                public ecs_bitmask_constant_t Item10;

                public ecs_bitmask_constant_t Item11;

                public ecs_bitmask_constant_t Item12;

                public ecs_bitmask_constant_t Item13;

                public ecs_bitmask_constant_t Item14;

                public ecs_bitmask_constant_t Item15;

                public ecs_bitmask_constant_t Item16;

                public ecs_bitmask_constant_t Item17;

                public ecs_bitmask_constant_t Item18;

                public ecs_bitmask_constant_t Item19;

                public ecs_bitmask_constant_t Item20;

                public ecs_bitmask_constant_t Item21;

                public ecs_bitmask_constant_t Item22;

                public ecs_bitmask_constant_t Item23;

                public ecs_bitmask_constant_t Item24;

                public ecs_bitmask_constant_t Item25;

                public ecs_bitmask_constant_t Item26;

                public ecs_bitmask_constant_t Item27;

                public ecs_bitmask_constant_t Item28;

                public ecs_bitmask_constant_t Item29;

                public ecs_bitmask_constant_t Item30;

                public ecs_bitmask_constant_t Item31;
            }
        }

        public partial struct ecs_block_allocator_block_t
        {
            public void* memory;

            public ecs_block_allocator_block_t* next;
        }

        public partial struct ecs_block_allocator_chunk_header_t
        {
            public ecs_block_allocator_chunk_header_t* next;
        }

        public partial struct ecs_block_allocator_t
        {
            public ecs_block_allocator_chunk_header_t* head;

            public ecs_block_allocator_block_t* block_head;

            public ecs_block_allocator_block_t* block_tail;

            public int chunk_size;

            public int data_size;

            public int chunks_per_block;

            public int block_size;

            public int alloc_count;
        }

        public partial struct ecs_bucket_entry_t
        {
            public ulong key;

            public ulong value;

            public ecs_bucket_entry_t* next;
        }

        public partial struct ecs_bucket_t
        {
            public ecs_bucket_entry_t* first;
        }

        public partial struct ecs_bulk_desc_t
        {
            public int _canary;

            public ulong* entities;

            public int count;

            public ids_FixedBuffer ids;

            public void** data;

            public ecs_table_t* table;

            public partial struct ids_FixedBuffer
            {
                public ulong Item0;

                public ulong Item1;

                public ulong Item2;

                public ulong Item3;

                public ulong Item4;

                public ulong Item5;

                public ulong Item6;

                public ulong Item7;

                public ulong Item8;

                public ulong Item9;

                public ulong Item10;

                public ulong Item11;

                public ulong Item12;

                public ulong Item13;

                public ulong Item14;

                public ulong Item15;

                public ulong Item16;

                public ulong Item17;

                public ulong Item18;

                public ulong Item19;

                public ulong Item20;

                public ulong Item21;

                public ulong Item22;

                public ulong Item23;

                public ulong Item24;

                public ulong Item25;

                public ulong Item26;

                public ulong Item27;

                public ulong Item28;

                public ulong Item29;

                public ulong Item30;

                public ulong Item31;
            }
        }

        public partial struct ecs_component_desc_t
        {
            public int _canary;

            public ulong entity;

            public ecs_type_info_t type;
        }

        public partial struct ecs_counter_t
        {
            public ecs_gauge_t rate;

            public value_FixedBuffer value;

            public partial struct value_FixedBuffer
            {
                public double Item0;

                public double Item1;

                public double Item2;

                public double Item3;

                public double Item4;

                public double Item5;

                public double Item6;

                public double Item7;

                public double Item8;

                public double Item9;

                public double Item10;

                public double Item11;

                public double Item12;

                public double Item13;

                public double Item14;

                public double Item15;

                public double Item16;

                public double Item17;

                public double Item18;

                public double Item19;

                public double Item20;

                public double Item21;

                public double Item22;

                public double Item23;

                public double Item24;

                public double Item25;

                public double Item26;

                public double Item27;

                public double Item28;

                public double Item29;

                public double Item30;

                public double Item31;

                public double Item32;

                public double Item33;

                public double Item34;

                public double Item35;

                public double Item36;

                public double Item37;

                public double Item38;

                public double Item39;

                public double Item40;

                public double Item41;

                public double Item42;

                public double Item43;

                public double Item44;

                public double Item45;

                public double Item46;

                public double Item47;

                public double Item48;

                public double Item49;

                public double Item50;

                public double Item51;

                public double Item52;

                public double Item53;

                public double Item54;

                public double Item55;

                public double Item56;

                public double Item57;

                public double Item58;

                public double Item59;
            }
        }

        public partial struct ecs_data_t
        {
        }

        public partial struct ecs_entity_desc_t
        {
            public int _canary;

            public ulong id;

            public byte* name;

            public byte* sep;

            public byte* root_sep;

            public byte* symbol;

            public byte use_low_id;

            public add_FixedBuffer add;

            public byte* add_expr;

            public partial struct add_FixedBuffer
            {
                public ulong Item0;

                public ulong Item1;

                public ulong Item2;

                public ulong Item3;

                public ulong Item4;

                public ulong Item5;

                public ulong Item6;

                public ulong Item7;

                public ulong Item8;

                public ulong Item9;

                public ulong Item10;

                public ulong Item11;

                public ulong Item12;

                public ulong Item13;

                public ulong Item14;

                public ulong Item15;

                public ulong Item16;

                public ulong Item17;

                public ulong Item18;

                public ulong Item19;

                public ulong Item20;

                public ulong Item21;

                public ulong Item22;

                public ulong Item23;

                public ulong Item24;

                public ulong Item25;

                public ulong Item26;

                public ulong Item27;

                public ulong Item28;

                public ulong Item29;

                public ulong Item30;

                public ulong Item31;
            }
        }

        public partial struct ecs_entity_to_json_desc_t
        {
            public byte serialize_path;

            public byte serialize_meta_ids;

            public byte serialize_label;

            public byte serialize_brief;

            public byte serialize_link;

            public byte serialize_color;

            public byte serialize_id_labels;

            public byte serialize_base;

            public byte serialize_private;

            public byte serialize_hidden;

            public byte serialize_values;

            public byte serialize_type_info;

            public byte serialize_alerts;

            public ulong serialize_refs;
        }

        public partial struct ecs_enum_constant_t
        {
            public byte* name;

            public int value;

            public ulong constant;
        }

        public partial struct ecs_enum_desc_t
        {
            public ulong entity;

            public constants_FixedBuffer constants;

            public partial struct constants_FixedBuffer
            {
                public ecs_enum_constant_t Item0;

                public ecs_enum_constant_t Item1;

                public ecs_enum_constant_t Item2;

                public ecs_enum_constant_t Item3;

                public ecs_enum_constant_t Item4;

                public ecs_enum_constant_t Item5;

                public ecs_enum_constant_t Item6;

                public ecs_enum_constant_t Item7;

                public ecs_enum_constant_t Item8;

                public ecs_enum_constant_t Item9;

                public ecs_enum_constant_t Item10;

                public ecs_enum_constant_t Item11;

                public ecs_enum_constant_t Item12;

                public ecs_enum_constant_t Item13;

                public ecs_enum_constant_t Item14;

                public ecs_enum_constant_t Item15;

                public ecs_enum_constant_t Item16;

                public ecs_enum_constant_t Item17;

                public ecs_enum_constant_t Item18;

                public ecs_enum_constant_t Item19;

                public ecs_enum_constant_t Item20;

                public ecs_enum_constant_t Item21;

                public ecs_enum_constant_t Item22;

                public ecs_enum_constant_t Item23;

                public ecs_enum_constant_t Item24;

                public ecs_enum_constant_t Item25;

                public ecs_enum_constant_t Item26;

                public ecs_enum_constant_t Item27;

                public ecs_enum_constant_t Item28;

                public ecs_enum_constant_t Item29;

                public ecs_enum_constant_t Item30;

                public ecs_enum_constant_t Item31;
            }
        }

        public partial struct ecs_event_desc_t
        {
            public ulong @event;

            public ecs_type_t* ids;

            public ecs_table_t* table;

            public ecs_table_t* other_table;

            public int offset;

            public int count;

            public ulong entity;

            public void* param;

            public void* observable;

            public uint flags;
        }

        public partial struct ecs_event_record_t
        {
            public ecs_event_id_record_t* any;

            public ecs_event_id_record_t* wildcard;

            public ecs_event_id_record_t* wildcard_pair;

            public ecs_map_t event_ids;

            public ulong @event;

            public partial struct ecs_event_id_record_t
            {
            }
        }

        public partial struct ecs_expr_var_scope_t
        {
            public ecs_hashmap_t var_index;

            public ecs_vec_t vars;

            public ecs_expr_var_scope_t* parent;
        }

        public partial struct ecs_expr_var_t
        {
            public byte* name;

            public ecs_value_t value;

            public byte owned;
        }

        public partial struct ecs_filter_desc_t
        {
            public int _canary;

            public terms_FixedBuffer terms;

            public ecs_term_t* terms_buffer;

            public int terms_buffer_count;

            public ecs_filter_t* storage;

            public byte instanced;

            public uint flags;

            public byte* expr;

            public ulong entity;

            public partial struct terms_FixedBuffer
            {
                public ecs_term_t Item0;

                public ecs_term_t Item1;

                public ecs_term_t Item2;

                public ecs_term_t Item3;

                public ecs_term_t Item4;

                public ecs_term_t Item5;

                public ecs_term_t Item6;

                public ecs_term_t Item7;

                public ecs_term_t Item8;

                public ecs_term_t Item9;

                public ecs_term_t Item10;

                public ecs_term_t Item11;

                public ecs_term_t Item12;

                public ecs_term_t Item13;

                public ecs_term_t Item14;

                public ecs_term_t Item15;
            }
        }

        public partial struct ecs_filter_iter_t
        {
            public ecs_filter_t* filter;

            public ecs_iter_kind_t kind;

            public ecs_term_iter_t term_iter;

            public int matches_left;

            public int pivot_term;
        }

        public partial struct ecs_filter_t
        {
            public ecs_header_t hdr;

            public ecs_term_t* terms;

            public int term_count;

            public int field_count;

            public byte owned;

            public byte terms_owned;

            public uint flags;

            public variable_names_FixedBuffer variable_names;

            public int* sizes;

            public ulong entity;

            public ecs_iterable_t iterable;

            public System.IntPtr dtor; // delegate* unmanaged<void*, void>

            public ecs_world_t* world;

            public partial struct variable_names_FixedBuffer
            {
                public byte* Item0;
            }
        }

        public partial struct ecs_flatten_desc_t
        {
            public byte keep_names;

            public byte lose_depth;
        }

        public partial struct ecs_from_json_desc_t
        {
            public byte* name;

            public byte* expr;

            public System.IntPtr lookup_action; // delegate* unmanaged<ecs_world_t*, byte*, void*, ulong>

            public void* lookup_ctx;
        }

        public partial struct ecs_gauge_t
        {
            public avg_FixedBuffer avg;

            public min_FixedBuffer min;

            public max_FixedBuffer max;

            public partial struct avg_FixedBuffer
            {
                public float Item0;

                public float Item1;

                public float Item2;

                public float Item3;

                public float Item4;

                public float Item5;

                public float Item6;

                public float Item7;

                public float Item8;

                public float Item9;

                public float Item10;

                public float Item11;

                public float Item12;

                public float Item13;

                public float Item14;

                public float Item15;

                public float Item16;

                public float Item17;

                public float Item18;

                public float Item19;

                public float Item20;

                public float Item21;

                public float Item22;

                public float Item23;

                public float Item24;

                public float Item25;

                public float Item26;

                public float Item27;

                public float Item28;

                public float Item29;

                public float Item30;

                public float Item31;

                public float Item32;

                public float Item33;

                public float Item34;

                public float Item35;

                public float Item36;

                public float Item37;

                public float Item38;

                public float Item39;

                public float Item40;

                public float Item41;

                public float Item42;

                public float Item43;

                public float Item44;

                public float Item45;

                public float Item46;

                public float Item47;

                public float Item48;

                public float Item49;

                public float Item50;

                public float Item51;

                public float Item52;

                public float Item53;

                public float Item54;

                public float Item55;

                public float Item56;

                public float Item57;

                public float Item58;

                public float Item59;
            }

            public partial struct min_FixedBuffer
            {
                public float Item0;

                public float Item1;

                public float Item2;

                public float Item3;

                public float Item4;

                public float Item5;

                public float Item6;

                public float Item7;

                public float Item8;

                public float Item9;

                public float Item10;

                public float Item11;

                public float Item12;

                public float Item13;

                public float Item14;

                public float Item15;

                public float Item16;

                public float Item17;

                public float Item18;

                public float Item19;

                public float Item20;

                public float Item21;

                public float Item22;

                public float Item23;

                public float Item24;

                public float Item25;

                public float Item26;

                public float Item27;

                public float Item28;

                public float Item29;

                public float Item30;

                public float Item31;

                public float Item32;

                public float Item33;

                public float Item34;

                public float Item35;

                public float Item36;

                public float Item37;

                public float Item38;

                public float Item39;

                public float Item40;

                public float Item41;

                public float Item42;

                public float Item43;

                public float Item44;

                public float Item45;

                public float Item46;

                public float Item47;

                public float Item48;

                public float Item49;

                public float Item50;

                public float Item51;

                public float Item52;

                public float Item53;

                public float Item54;

                public float Item55;

                public float Item56;

                public float Item57;

                public float Item58;

                public float Item59;
            }

            public partial struct max_FixedBuffer
            {
                public float Item0;

                public float Item1;

                public float Item2;

                public float Item3;

                public float Item4;

                public float Item5;

                public float Item6;

                public float Item7;

                public float Item8;

                public float Item9;

                public float Item10;

                public float Item11;

                public float Item12;

                public float Item13;

                public float Item14;

                public float Item15;

                public float Item16;

                public float Item17;

                public float Item18;

                public float Item19;

                public float Item20;

                public float Item21;

                public float Item22;

                public float Item23;

                public float Item24;

                public float Item25;

                public float Item26;

                public float Item27;

                public float Item28;

                public float Item29;

                public float Item30;

                public float Item31;

                public float Item32;

                public float Item33;

                public float Item34;

                public float Item35;

                public float Item36;

                public float Item37;

                public float Item38;

                public float Item39;

                public float Item40;

                public float Item41;

                public float Item42;

                public float Item43;

                public float Item44;

                public float Item45;

                public float Item46;

                public float Item47;

                public float Item48;

                public float Item49;

                public float Item50;

                public float Item51;

                public float Item52;

                public float Item53;

                public float Item54;

                public float Item55;

                public float Item56;

                public float Item57;

                public float Item58;

                public float Item59;
            }
        }

        public partial struct ecs_hashmap_t
        {
            public System.IntPtr hash; // delegate* unmanaged<void*, ulong>

            public System.IntPtr compare; // delegate* unmanaged<void*, void*, int>

            public int key_size;

            public int value_size;

            public ecs_block_allocator_t* hashmap_allocator;

            public ecs_block_allocator_t bucket_allocator;

            public ecs_map_t impl;
        }

        public partial struct ecs_header_t
        {
            public int magic;

            public int type;

            public ecs_mixins_t* mixins;
        }

        public partial struct ecs_hm_bucket_t
        {
            public ecs_vec_t keys;

            public ecs_vec_t values;
        }

        public partial struct ecs_http_connection_t
        {
            public ulong id;

            public ecs_http_server_t* server;

            public host_FixedBuffer host;

            public port_FixedBuffer port;

            public partial struct host_FixedBuffer
            {
                public byte Item0;

                public byte Item1;

                public byte Item2;

                public byte Item3;

                public byte Item4;

                public byte Item5;

                public byte Item6;

                public byte Item7;

                public byte Item8;

                public byte Item9;

                public byte Item10;

                public byte Item11;

                public byte Item12;

                public byte Item13;

                public byte Item14;

                public byte Item15;

                public byte Item16;

                public byte Item17;

                public byte Item18;

                public byte Item19;

                public byte Item20;

                public byte Item21;

                public byte Item22;

                public byte Item23;

                public byte Item24;

                public byte Item25;

                public byte Item26;

                public byte Item27;

                public byte Item28;

                public byte Item29;

                public byte Item30;

                public byte Item31;

                public byte Item32;

                public byte Item33;

                public byte Item34;

                public byte Item35;

                public byte Item36;

                public byte Item37;

                public byte Item38;

                public byte Item39;

                public byte Item40;

                public byte Item41;

                public byte Item42;

                public byte Item43;

                public byte Item44;

                public byte Item45;

                public byte Item46;

                public byte Item47;

                public byte Item48;

                public byte Item49;

                public byte Item50;

                public byte Item51;

                public byte Item52;

                public byte Item53;

                public byte Item54;

                public byte Item55;

                public byte Item56;

                public byte Item57;

                public byte Item58;

                public byte Item59;

                public byte Item60;

                public byte Item61;

                public byte Item62;

                public byte Item63;

                public byte Item64;

                public byte Item65;

                public byte Item66;

                public byte Item67;

                public byte Item68;

                public byte Item69;

                public byte Item70;

                public byte Item71;

                public byte Item72;

                public byte Item73;

                public byte Item74;

                public byte Item75;

                public byte Item76;

                public byte Item77;

                public byte Item78;

                public byte Item79;

                public byte Item80;

                public byte Item81;

                public byte Item82;

                public byte Item83;

                public byte Item84;

                public byte Item85;

                public byte Item86;

                public byte Item87;

                public byte Item88;

                public byte Item89;

                public byte Item90;

                public byte Item91;

                public byte Item92;

                public byte Item93;

                public byte Item94;

                public byte Item95;

                public byte Item96;

                public byte Item97;

                public byte Item98;

                public byte Item99;

                public byte Item100;

                public byte Item101;

                public byte Item102;

                public byte Item103;

                public byte Item104;

                public byte Item105;

                public byte Item106;

                public byte Item107;

                public byte Item108;

                public byte Item109;

                public byte Item110;

                public byte Item111;

                public byte Item112;

                public byte Item113;

                public byte Item114;

                public byte Item115;

                public byte Item116;

                public byte Item117;

                public byte Item118;

                public byte Item119;

                public byte Item120;

                public byte Item121;

                public byte Item122;

                public byte Item123;

                public byte Item124;

                public byte Item125;

                public byte Item126;

                public byte Item127;
            }

            public partial struct port_FixedBuffer
            {
                public byte Item0;

                public byte Item1;

                public byte Item2;

                public byte Item3;

                public byte Item4;

                public byte Item5;

                public byte Item6;

                public byte Item7;

                public byte Item8;

                public byte Item9;

                public byte Item10;

                public byte Item11;

                public byte Item12;

                public byte Item13;

                public byte Item14;

                public byte Item15;
            }
        }

        public partial struct ecs_http_key_value_t
        {
            public byte* key;

            public byte* value;
        }

        public partial struct ecs_http_reply_t
        {
            public int code;

            public ecs_strbuf_t body;

            public byte* status;

            public byte* content_type;

            public ecs_strbuf_t headers;
        }

        public partial struct ecs_http_request_t
        {
            public ulong id;

            public ecs_http_method_t method;

            public byte* path;

            public byte* body;

            public headers_FixedBuffer headers;

            public @params_FixedBuffer @params;

            public int header_count;

            public int param_count;

            public ecs_http_connection_t* conn;

            public partial struct headers_FixedBuffer
            {
                public ecs_http_key_value_t Item0;

                public ecs_http_key_value_t Item1;

                public ecs_http_key_value_t Item2;

                public ecs_http_key_value_t Item3;

                public ecs_http_key_value_t Item4;

                public ecs_http_key_value_t Item5;

                public ecs_http_key_value_t Item6;

                public ecs_http_key_value_t Item7;

                public ecs_http_key_value_t Item8;

                public ecs_http_key_value_t Item9;

                public ecs_http_key_value_t Item10;

                public ecs_http_key_value_t Item11;

                public ecs_http_key_value_t Item12;

                public ecs_http_key_value_t Item13;

                public ecs_http_key_value_t Item14;

                public ecs_http_key_value_t Item15;

                public ecs_http_key_value_t Item16;

                public ecs_http_key_value_t Item17;

                public ecs_http_key_value_t Item18;

                public ecs_http_key_value_t Item19;

                public ecs_http_key_value_t Item20;

                public ecs_http_key_value_t Item21;

                public ecs_http_key_value_t Item22;

                public ecs_http_key_value_t Item23;

                public ecs_http_key_value_t Item24;

                public ecs_http_key_value_t Item25;

                public ecs_http_key_value_t Item26;

                public ecs_http_key_value_t Item27;

                public ecs_http_key_value_t Item28;

                public ecs_http_key_value_t Item29;

                public ecs_http_key_value_t Item30;

                public ecs_http_key_value_t Item31;
            }

            public partial struct @params_FixedBuffer
            {
                public ecs_http_key_value_t Item0;

                public ecs_http_key_value_t Item1;

                public ecs_http_key_value_t Item2;

                public ecs_http_key_value_t Item3;

                public ecs_http_key_value_t Item4;

                public ecs_http_key_value_t Item5;

                public ecs_http_key_value_t Item6;

                public ecs_http_key_value_t Item7;

                public ecs_http_key_value_t Item8;

                public ecs_http_key_value_t Item9;

                public ecs_http_key_value_t Item10;

                public ecs_http_key_value_t Item11;

                public ecs_http_key_value_t Item12;

                public ecs_http_key_value_t Item13;

                public ecs_http_key_value_t Item14;

                public ecs_http_key_value_t Item15;

                public ecs_http_key_value_t Item16;

                public ecs_http_key_value_t Item17;

                public ecs_http_key_value_t Item18;

                public ecs_http_key_value_t Item19;

                public ecs_http_key_value_t Item20;

                public ecs_http_key_value_t Item21;

                public ecs_http_key_value_t Item22;

                public ecs_http_key_value_t Item23;

                public ecs_http_key_value_t Item24;

                public ecs_http_key_value_t Item25;

                public ecs_http_key_value_t Item26;

                public ecs_http_key_value_t Item27;

                public ecs_http_key_value_t Item28;

                public ecs_http_key_value_t Item29;

                public ecs_http_key_value_t Item30;

                public ecs_http_key_value_t Item31;
            }
        }

        public partial struct ecs_http_server_desc_t
        {
            public System.IntPtr callback; // delegate* unmanaged<ecs_http_request_t*, ecs_http_reply_t*, void*, byte>

            public void* ctx;

            public ushort port;

            public byte* ipaddr;

            public int send_queue_wait_ms;
        }

        public partial struct ecs_http_server_t
        {
        }

        public partial struct ecs_id_record_t
        {
        }

        public partial struct ecs_iter_cache_t
        {
            public ecs_stack_cursor_t* stack_cursor;

            public byte used;

            public byte allocated;
        }

        public partial struct ecs_iter_private_t
        {
            public iter_AnonymousRecord iter;

            public void* entity_iter;

            public ecs_iter_cache_t cache;

            [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
            public partial struct iter_AnonymousRecord
            {
                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_term_iter_t term;

                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_filter_iter_t filter;

                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_query_iter_t query;

                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_rule_iter_t rule;

                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_snapshot_iter_t snapshot;

                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_page_iter_t page;

                [System.Runtime.InteropServices.FieldOffset(0)]
                public ecs_worker_iter_t worker;
            }
        }

        public partial struct ecs_iter_t
        {
            public ecs_world_t* world;

            public ecs_world_t* real_world;

            public ulong* entities;

            public void** ptrs;

            public int* sizes;

            public ecs_table_t* table;

            public ecs_table_t* other_table;

            public ulong* ids;

            public ecs_var_t* variables;

            public int* columns;

            public ulong* sources;

            public int* match_indices;

            public ecs_ref_t* references;

            public ulong constrained_vars;

            public ulong group_id;

            public int field_count;

            public ulong system;

            public ulong @event;

            public ulong event_id;

            public ecs_term_t* terms;

            public int table_count;

            public int term_index;

            public int variable_count;

            public byte** variable_names;

            public void* param;

            public void* ctx;

            public void* binding_ctx;

            public float delta_time;

            public float delta_system_time;

            public int frame_offset;

            public int offset;

            public int count;

            public int instance_count;

            public uint flags;

            public ulong interrupted_by;

            public ecs_iter_private_t priv;

            public System.IntPtr next; // delegate* unmanaged<ecs_iter_t*, byte>

            public System.IntPtr callback; // delegate* unmanaged<ecs_iter_t*, void>

            public System.IntPtr fini; // delegate* unmanaged<ecs_iter_t*, void>

            public ecs_iter_t* chain_it;
        }

        public partial struct ecs_iter_to_json_desc_t
        {
            public byte serialize_term_ids;

            public byte serialize_ids;

            public byte serialize_sources;

            public byte serialize_variables;

            public byte serialize_is_set;

            public byte serialize_values;

            public byte serialize_entities;

            public byte serialize_entity_labels;

            public byte serialize_entity_ids;

            public byte serialize_entity_names;

            public byte serialize_variable_labels;

            public byte serialize_variable_ids;

            public byte serialize_colors;

            public byte measure_eval_duration;

            public byte serialize_type_info;

            public byte serialize_table;
        }

        public partial struct ecs_iterable_t
        {
            public System.IntPtr init; // delegate* unmanaged<ecs_world_t*, void*, ecs_iter_t*, ecs_term_t*, void>
        }

        public partial struct ecs_map_iter_t
        {
            public ecs_map_t* map;

            public ecs_bucket_t* bucket;

            public ecs_bucket_entry_t* entry;

            public ulong* res;
        }

        public partial struct ecs_map_params_t
        {
            public ecs_allocator_t* allocator;

            public ecs_block_allocator_t entry_allocator;
        }

        public partial struct ecs_map_t
        {
            public byte bucket_shift;

            public byte shared_allocator;

            public ecs_bucket_t* buckets;

            public int bucket_count;

            public int count;

            public ecs_block_allocator_t* entry_allocator;

            public ecs_allocator_t* allocator;
        }

        public partial struct ecs_member_t
        {
            public byte* name;

            public ulong type;

            public int count;

            public int offset;

            public ulong unit;

            public ecs_member_value_range_t range;

            public ecs_member_value_range_t error_range;

            public ecs_member_value_range_t warning_range;

            public int size;

            public ulong member;
        }

        public partial struct ecs_member_value_range_t
        {
            public double min;

            public double max;
        }

        public partial struct ecs_meta_cursor_t
        {
            public ecs_world_t* world;

            public scope_FixedBuffer scope;

            public int depth;

            public byte valid;

            public byte is_primitive_scope;

            public System.IntPtr lookup_action; // delegate* unmanaged<ecs_world_t*, byte*, void*, ulong>

            public void* lookup_ctx;

            public partial struct scope_FixedBuffer
            {
                public ecs_meta_scope_t Item0;

                public ecs_meta_scope_t Item1;

                public ecs_meta_scope_t Item2;

                public ecs_meta_scope_t Item3;

                public ecs_meta_scope_t Item4;

                public ecs_meta_scope_t Item5;

                public ecs_meta_scope_t Item6;

                public ecs_meta_scope_t Item7;

                public ecs_meta_scope_t Item8;

                public ecs_meta_scope_t Item9;

                public ecs_meta_scope_t Item10;

                public ecs_meta_scope_t Item11;

                public ecs_meta_scope_t Item12;

                public ecs_meta_scope_t Item13;

                public ecs_meta_scope_t Item14;

                public ecs_meta_scope_t Item15;

                public ecs_meta_scope_t Item16;

                public ecs_meta_scope_t Item17;

                public ecs_meta_scope_t Item18;

                public ecs_meta_scope_t Item19;

                public ecs_meta_scope_t Item20;

                public ecs_meta_scope_t Item21;

                public ecs_meta_scope_t Item22;

                public ecs_meta_scope_t Item23;

                public ecs_meta_scope_t Item24;

                public ecs_meta_scope_t Item25;

                public ecs_meta_scope_t Item26;

                public ecs_meta_scope_t Item27;

                public ecs_meta_scope_t Item28;

                public ecs_meta_scope_t Item29;

                public ecs_meta_scope_t Item30;

                public ecs_meta_scope_t Item31;
            }
        }

        public partial struct ecs_meta_scope_t
        {
            public ulong type;

            public ecs_meta_type_op_t* ops;

            public int op_count;

            public int op_cur;

            public int elem_cur;

            public int prev_depth;

            public void* ptr;

            public EcsComponent* comp;

            public EcsOpaque* opaque;

            public ecs_vec_t* vector;

            public ecs_hashmap_t* members;

            public byte is_collection;

            public byte is_inline_array;

            public byte is_empty_scope;
        }

        public partial struct ecs_meta_type_op_t
        {
            public ecs_meta_type_op_kind_t kind;

            public int offset;

            public int count;

            public byte* name;

            public int op_count;

            public int size;

            public ulong type;

            public int member_index;

            public ecs_hashmap_t* members;
        }

        public partial struct ecs_metric_desc_t
        {
            public int _canary;

            public ulong entity;

            public ulong member;

            public ulong id;

            public byte targets;

            public ulong kind;

            public byte* brief;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public partial struct ecs_metric_t
        {
            [System.Runtime.InteropServices.FieldOffset(0)]
            public ecs_gauge_t gauge;

            [System.Runtime.InteropServices.FieldOffset(0)]
            public ecs_counter_t counter;
        }

        public partial struct ecs_mixins_t
        {
        }

        public partial struct ecs_observable_t
        {
            public ecs_event_record_t on_add;

            public ecs_event_record_t on_remove;

            public ecs_event_record_t on_set;

            public ecs_event_record_t un_set;

            public ecs_event_record_t on_wildcard;

            public ecs_sparse_t events;
        }

        public partial struct ecs_observer_desc_t
        {
            public int _canary;

            public ulong entity;

            public ecs_filter_desc_t filter;

            public events_FixedBuffer events;

            public byte yield_existing;

            public System.IntPtr callback; // delegate* unmanaged<ecs_iter_t*, void>

            public System.IntPtr run; // delegate* unmanaged<ecs_iter_t*, void>

            public void* ctx;

            public void* binding_ctx;

            public System.IntPtr ctx_free; // delegate* unmanaged<void*, void>

            public System.IntPtr binding_ctx_free; // delegate* unmanaged<void*, void>

            public void* observable;

            public int* last_event_id;

            public int term_index;

            public partial struct events_FixedBuffer
            {
                public ulong Item0;

                public ulong Item1;

                public ulong Item2;

                public ulong Item3;

                public ulong Item4;

                public ulong Item5;

                public ulong Item6;

                public ulong Item7;
            }
        }

        public partial struct ecs_observer_t
        {
            public ecs_header_t hdr;

            public ecs_filter_t filter;

            public events_FixedBuffer events;

            public int event_count;

            public System.IntPtr callback; // delegate* unmanaged<ecs_iter_t*, void>

            public System.IntPtr run; // delegate* unmanaged<ecs_iter_t*, void>

            public void* ctx;

            public void* binding_ctx;

            public System.IntPtr ctx_free; // delegate* unmanaged<void*, void>

            public System.IntPtr binding_ctx_free; // delegate* unmanaged<void*, void>

            public ecs_observable_t* observable;

            public int* last_event_id;

            public int last_event_id_storage;

            public ulong register_id;

            public int term_index;

            public byte is_monitor;

            public byte is_multi;

            public System.IntPtr dtor; // delegate* unmanaged<void*, void>

            public partial struct events_FixedBuffer
            {
                public ulong Item0;

                public ulong Item1;

                public ulong Item2;

                public ulong Item3;

                public ulong Item4;

                public ulong Item5;

                public ulong Item6;

                public ulong Item7;
            }
        }

        public partial struct ecs_opaque_desc_t
        {
            public ulong entity;

            public EcsOpaque type;
        }

        public partial struct ecs_os_api_t
        {
            public System.IntPtr init_; // delegate* unmanaged<void>

            public System.IntPtr fini_; // delegate* unmanaged<void>

            public System.IntPtr malloc_; // delegate* unmanaged<int, void*>

            public System.IntPtr realloc_; // delegate* unmanaged<void*, int, void*>

            public System.IntPtr calloc_; // delegate* unmanaged<int, void*>

            public System.IntPtr free_; // delegate* unmanaged<void*, void>

            public System.IntPtr strdup_; // delegate* unmanaged<byte*, byte*>

            public System.IntPtr thread_new_; // delegate* unmanaged<System.IntPtr, void*, ulong>

            public System.IntPtr thread_join_; // delegate* unmanaged<ulong, void*>

            public System.IntPtr thread_self_; // delegate* unmanaged<ulong>

            public System.IntPtr task_new_; // delegate* unmanaged<System.IntPtr, void*, ulong>

            public System.IntPtr task_join_; // delegate* unmanaged<ulong, void*>

            public System.IntPtr ainc_; // delegate* unmanaged<int*, int>

            public System.IntPtr adec_; // delegate* unmanaged<int*, int>

            public System.IntPtr lainc_; // delegate* unmanaged<long*, long>

            public System.IntPtr ladec_; // delegate* unmanaged<long*, long>

            public System.IntPtr mutex_new_; // delegate* unmanaged<ulong>

            public System.IntPtr mutex_free_; // delegate* unmanaged<ulong, void>

            public System.IntPtr mutex_lock_; // delegate* unmanaged<ulong, void>

            public System.IntPtr mutex_unlock_; // delegate* unmanaged<ulong, void>

            public System.IntPtr cond_new_; // delegate* unmanaged<ulong>

            public System.IntPtr cond_free_; // delegate* unmanaged<ulong, void>

            public System.IntPtr cond_signal_; // delegate* unmanaged<ulong, void>

            public System.IntPtr cond_broadcast_; // delegate* unmanaged<ulong, void>

            public System.IntPtr cond_wait_; // delegate* unmanaged<ulong, ulong, void>

            public System.IntPtr sleep_; // delegate* unmanaged<int, int, void>

            public System.IntPtr now_; // delegate* unmanaged<ulong>

            public System.IntPtr get_time_; // delegate* unmanaged<ecs_time_t*, void>

            public System.IntPtr log_; // delegate* unmanaged<int, byte*, int, byte*, void>

            public System.IntPtr abort_; // delegate* unmanaged<void>

            public System.IntPtr dlopen_; // delegate* unmanaged<byte*, ulong>

            public System.IntPtr dlproc_; // delegate* unmanaged<ulong, byte*, System.IntPtr>

            public System.IntPtr dlclose_; // delegate* unmanaged<ulong, void>

            public System.IntPtr module_to_dl_; // delegate* unmanaged<byte*, byte*>

            public System.IntPtr module_to_etc_; // delegate* unmanaged<byte*, byte*>

            public int log_level_;

            public int log_indent_;

            public int log_last_error_;

            public long log_last_timestamp_;

            public uint flags_;
        }

        public partial struct ecs_page_iter_t
        {
            public int offset;

            public int limit;

            public int remaining;
        }

        public partial struct ecs_parse_expr_desc_t
        {
            public byte* name;

            public byte* expr;

            public System.IntPtr lookup_action; // delegate* unmanaged<ecs_world_t*, byte*, void*, ulong>

            public void* lookup_ctx;

            public ecs_vars_t* vars;
        }

        public partial struct ecs_pipeline_desc_t
        {
            public ulong entity;

            public ecs_query_desc_t query;
        }

        public partial struct ecs_pipeline_stats_t
        {
            public byte canary_;

            public ecs_vec_t systems;

            public ecs_map_t system_stats;

            public int t;

            public int system_count;

            public int active_system_count;

            public int rebuild_count;
        }

        public partial struct ecs_primitive_desc_t
        {
            public ulong entity;

            public ecs_primitive_kind_t kind;
        }

        public partial struct ecs_query_desc_t
        {
            public int _canary;

            public ecs_filter_desc_t filter;

            public ulong order_by_component;

            public System.IntPtr order_by; // delegate* unmanaged<ulong, void*, ulong, void*, int>

            public System.IntPtr sort_table; // delegate* unmanaged<ecs_world_t*, ecs_table_t*, ulong*, void*, int, int, int, System.IntPtr, void>

            public ulong group_by_id;

            public System.IntPtr group_by; // delegate* unmanaged<ecs_world_t*, ecs_table_t*, ulong, void*, ulong>

            public System.IntPtr on_group_create; // delegate* unmanaged<ecs_world_t*, ulong, void*, void*>

            public System.IntPtr on_group_delete; // delegate* unmanaged<ecs_world_t*, ulong, void*, void*, void>

            public void* group_by_ctx;

            public System.IntPtr group_by_ctx_free; // delegate* unmanaged<void*, void>

            public ecs_query_t* parent;
        }

        public partial struct ecs_query_group_info_t
        {
            public int match_count;

            public int table_count;

            public void* ctx;
        }

        public partial struct ecs_query_iter_t
        {
            public ecs_query_t* query;

            public ecs_query_table_match_t* node;

            public ecs_query_table_match_t* prev;

            public ecs_query_table_match_t* last;

            public int sparse_smallest;

            public int sparse_first;

            public int bitset_first;

            public int skip_count;
        }

        public partial struct ecs_query_stats_t
        {
            public long first_;

            public ecs_metric_t matched_table_count;

            public ecs_metric_t matched_empty_table_count;

            public ecs_metric_t matched_entity_count;

            public long last_;

            public int t;
        }

        public partial struct ecs_query_t
        {
        }

        public partial struct ecs_query_table_match_t
        {
        }

        public partial struct ecs_record_t
        {
            public ecs_id_record_t* idr;

            public ecs_table_t* table;

            public uint row;

            public int dense;
        }

        public partial struct ecs_ref_t
        {
            public ulong entity;

            public ulong id;

            public ecs_table_record_t* tr;

            public ecs_record_t* record;
        }

        public partial struct ecs_rule_iter_t
        {
            public ecs_rule_t* rule;

            public ecs_var_t* vars;

            public ecs_rule_var_t* rule_vars;

            public ecs_rule_op_t* ops;

            public ecs_rule_op_ctx_t* op_ctx;

            public ulong* written;

            public ecs_rule_op_profile_t* profile;

            public byte redo;

            public short op;

            public short sp;

            public partial struct ecs_rule_var_t
            {
            }

            public partial struct ecs_rule_op_t
            {
            }

            public partial struct ecs_rule_op_ctx_t
            {
            }
        }

        public partial struct ecs_rule_op_profile_t
        {
            public count_FixedBuffer count;

            public partial struct count_FixedBuffer
            {
                public int Item0;

                public int Item1;
            }
        }

        public partial struct ecs_rule_t
        {
        }

        public partial struct ecs_script_desc_t
        {
            public ulong entity;

            public byte* filename;

            public byte* str;
        }

        public partial struct ecs_serializer_t
        {
            public System.IntPtr value; // delegate* unmanaged<ecs_serializer_t*, ulong, void*, int>

            public System.IntPtr member; // delegate* unmanaged<ecs_serializer_t*, byte*, int>

            public ecs_world_t* world;

            public void* ctx;
        }

        public partial struct ecs_snapshot_iter_t
        {
            public ecs_filter_t filter;

            public ecs_vec_t tables;

            public int index;
        }

        public partial struct ecs_snapshot_t
        {
        }

        public partial struct ecs_sparse_t
        {
            public ecs_vec_t dense;

            public ecs_vec_t pages;

            public int size;

            public int count;

            public ulong max_id;

            public ecs_allocator_t* allocator;

            public ecs_block_allocator_t* page_allocator;

            public partial struct ecs_block_allocator_t
            {
                public ecs_block_allocator_chunk_header_t* head;

                public ecs_block_allocator_block_t* block_head;

                public ecs_block_allocator_block_t* block_tail;

                public int chunk_size;

                public int data_size;

                public int chunks_per_block;

                public int block_size;

                public int alloc_count;
            }
        }

        public partial struct ecs_stack_cursor_t
        {
            public ecs_stack_cursor_t* prev;

            public ecs_stack_page_t* page;

            public short sp;

            public byte is_free;

            public ecs_stack_t* owner;

            public partial struct ecs_stack_t
            {
            }
        }

        public partial struct ecs_stack_page_t
        {
        }

        public partial struct ecs_stage_t
        {
        }

        public partial struct ecs_strbuf_element
        {
            public byte buffer_embedded;

            public int pos;

            public byte* buf;

            public ecs_strbuf_element* next;
        }

        public partial struct ecs_strbuf_element_embedded
        {
            public ecs_strbuf_element super;

            public buf_FixedBuffer buf;

            public partial struct buf_FixedBuffer
            {
                public byte Item0;

                public byte Item1;

                public byte Item2;

                public byte Item3;

                public byte Item4;

                public byte Item5;

                public byte Item6;

                public byte Item7;

                public byte Item8;

                public byte Item9;

                public byte Item10;

                public byte Item11;

                public byte Item12;

                public byte Item13;

                public byte Item14;

                public byte Item15;

                public byte Item16;

                public byte Item17;

                public byte Item18;

                public byte Item19;

                public byte Item20;

                public byte Item21;

                public byte Item22;

                public byte Item23;

                public byte Item24;

                public byte Item25;

                public byte Item26;

                public byte Item27;

                public byte Item28;

                public byte Item29;

                public byte Item30;

                public byte Item31;

                public byte Item32;

                public byte Item33;

                public byte Item34;

                public byte Item35;

                public byte Item36;

                public byte Item37;

                public byte Item38;

                public byte Item39;

                public byte Item40;

                public byte Item41;

                public byte Item42;

                public byte Item43;

                public byte Item44;

                public byte Item45;

                public byte Item46;

                public byte Item47;

                public byte Item48;

                public byte Item49;

                public byte Item50;

                public byte Item51;

                public byte Item52;

                public byte Item53;

                public byte Item54;

                public byte Item55;

                public byte Item56;

                public byte Item57;

                public byte Item58;

                public byte Item59;

                public byte Item60;

                public byte Item61;

                public byte Item62;

                public byte Item63;

                public byte Item64;

                public byte Item65;

                public byte Item66;

                public byte Item67;

                public byte Item68;

                public byte Item69;

                public byte Item70;

                public byte Item71;

                public byte Item72;

                public byte Item73;

                public byte Item74;

                public byte Item75;

                public byte Item76;

                public byte Item77;

                public byte Item78;

                public byte Item79;

                public byte Item80;

                public byte Item81;

                public byte Item82;

                public byte Item83;

                public byte Item84;

                public byte Item85;

                public byte Item86;

                public byte Item87;

                public byte Item88;

                public byte Item89;

                public byte Item90;

                public byte Item91;

                public byte Item92;

                public byte Item93;

                public byte Item94;

                public byte Item95;

                public byte Item96;

                public byte Item97;

                public byte Item98;

                public byte Item99;

                public byte Item100;

                public byte Item101;

                public byte Item102;

                public byte Item103;

                public byte Item104;

                public byte Item105;

                public byte Item106;

                public byte Item107;

                public byte Item108;

                public byte Item109;

                public byte Item110;

                public byte Item111;

                public byte Item112;

                public byte Item113;

                public byte Item114;

                public byte Item115;

                public byte Item116;

                public byte Item117;

                public byte Item118;

                public byte Item119;

                public byte Item120;

                public byte Item121;

                public byte Item122;

                public byte Item123;

                public byte Item124;

                public byte Item125;

                public byte Item126;

                public byte Item127;

                public byte Item128;

                public byte Item129;

                public byte Item130;

                public byte Item131;

                public byte Item132;

                public byte Item133;

                public byte Item134;

                public byte Item135;

                public byte Item136;

                public byte Item137;

                public byte Item138;

                public byte Item139;

                public byte Item140;

                public byte Item141;

                public byte Item142;

                public byte Item143;

                public byte Item144;

                public byte Item145;

                public byte Item146;

                public byte Item147;

                public byte Item148;

                public byte Item149;

                public byte Item150;

                public byte Item151;

                public byte Item152;

                public byte Item153;

                public byte Item154;

                public byte Item155;

                public byte Item156;

                public byte Item157;

                public byte Item158;

                public byte Item159;

                public byte Item160;

                public byte Item161;

                public byte Item162;

                public byte Item163;

                public byte Item164;

                public byte Item165;

                public byte Item166;

                public byte Item167;

                public byte Item168;

                public byte Item169;

                public byte Item170;

                public byte Item171;

                public byte Item172;

                public byte Item173;

                public byte Item174;

                public byte Item175;

                public byte Item176;

                public byte Item177;

                public byte Item178;

                public byte Item179;

                public byte Item180;

                public byte Item181;

                public byte Item182;

                public byte Item183;

                public byte Item184;

                public byte Item185;

                public byte Item186;

                public byte Item187;

                public byte Item188;

                public byte Item189;

                public byte Item190;

                public byte Item191;

                public byte Item192;

                public byte Item193;

                public byte Item194;

                public byte Item195;

                public byte Item196;

                public byte Item197;

                public byte Item198;

                public byte Item199;

                public byte Item200;

                public byte Item201;

                public byte Item202;

                public byte Item203;

                public byte Item204;

                public byte Item205;

                public byte Item206;

                public byte Item207;

                public byte Item208;

                public byte Item209;

                public byte Item210;

                public byte Item211;

                public byte Item212;

                public byte Item213;

                public byte Item214;

                public byte Item215;

                public byte Item216;

                public byte Item217;

                public byte Item218;

                public byte Item219;

                public byte Item220;

                public byte Item221;

                public byte Item222;

                public byte Item223;

                public byte Item224;

                public byte Item225;

                public byte Item226;

                public byte Item227;

                public byte Item228;

                public byte Item229;

                public byte Item230;

                public byte Item231;

                public byte Item232;

                public byte Item233;

                public byte Item234;

                public byte Item235;

                public byte Item236;

                public byte Item237;

                public byte Item238;

                public byte Item239;

                public byte Item240;

                public byte Item241;

                public byte Item242;

                public byte Item243;

                public byte Item244;

                public byte Item245;

                public byte Item246;

                public byte Item247;

                public byte Item248;

                public byte Item249;

                public byte Item250;

                public byte Item251;

                public byte Item252;

                public byte Item253;

                public byte Item254;

                public byte Item255;

                public byte Item256;

                public byte Item257;

                public byte Item258;

                public byte Item259;

                public byte Item260;

                public byte Item261;

                public byte Item262;

                public byte Item263;

                public byte Item264;

                public byte Item265;

                public byte Item266;

                public byte Item267;

                public byte Item268;

                public byte Item269;

                public byte Item270;

                public byte Item271;

                public byte Item272;

                public byte Item273;

                public byte Item274;

                public byte Item275;

                public byte Item276;

                public byte Item277;

                public byte Item278;

                public byte Item279;

                public byte Item280;

                public byte Item281;

                public byte Item282;

                public byte Item283;

                public byte Item284;

                public byte Item285;

                public byte Item286;

                public byte Item287;

                public byte Item288;

                public byte Item289;

                public byte Item290;

                public byte Item291;

                public byte Item292;

                public byte Item293;

                public byte Item294;

                public byte Item295;

                public byte Item296;

                public byte Item297;

                public byte Item298;

                public byte Item299;

                public byte Item300;

                public byte Item301;

                public byte Item302;

                public byte Item303;

                public byte Item304;

                public byte Item305;

                public byte Item306;

                public byte Item307;

                public byte Item308;

                public byte Item309;

                public byte Item310;

                public byte Item311;

                public byte Item312;

                public byte Item313;

                public byte Item314;

                public byte Item315;

                public byte Item316;

                public byte Item317;

                public byte Item318;

                public byte Item319;

                public byte Item320;

                public byte Item321;

                public byte Item322;

                public byte Item323;

                public byte Item324;

                public byte Item325;

                public byte Item326;

                public byte Item327;

                public byte Item328;

                public byte Item329;

                public byte Item330;

                public byte Item331;

                public byte Item332;

                public byte Item333;

                public byte Item334;

                public byte Item335;

                public byte Item336;

                public byte Item337;

                public byte Item338;

                public byte Item339;

                public byte Item340;

                public byte Item341;

                public byte Item342;

                public byte Item343;

                public byte Item344;

                public byte Item345;

                public byte Item346;

                public byte Item347;

                public byte Item348;

                public byte Item349;

                public byte Item350;

                public byte Item351;

                public byte Item352;

                public byte Item353;

                public byte Item354;

                public byte Item355;

                public byte Item356;

                public byte Item357;

                public byte Item358;

                public byte Item359;

                public byte Item360;

                public byte Item361;

                public byte Item362;

                public byte Item363;

                public byte Item364;

                public byte Item365;

                public byte Item366;

                public byte Item367;

                public byte Item368;

                public byte Item369;

                public byte Item370;

                public byte Item371;

                public byte Item372;

                public byte Item373;

                public byte Item374;

                public byte Item375;

                public byte Item376;

                public byte Item377;

                public byte Item378;

                public byte Item379;

                public byte Item380;

                public byte Item381;

                public byte Item382;

                public byte Item383;

                public byte Item384;

                public byte Item385;

                public byte Item386;

                public byte Item387;

                public byte Item388;

                public byte Item389;

                public byte Item390;

                public byte Item391;

                public byte Item392;

                public byte Item393;

                public byte Item394;

                public byte Item395;

                public byte Item396;

                public byte Item397;

                public byte Item398;

                public byte Item399;

                public byte Item400;

                public byte Item401;

                public byte Item402;

                public byte Item403;

                public byte Item404;

                public byte Item405;

                public byte Item406;

                public byte Item407;

                public byte Item408;

                public byte Item409;

                public byte Item410;

                public byte Item411;

                public byte Item412;

                public byte Item413;

                public byte Item414;

                public byte Item415;

                public byte Item416;

                public byte Item417;

                public byte Item418;

                public byte Item419;

                public byte Item420;

                public byte Item421;

                public byte Item422;

                public byte Item423;

                public byte Item424;

                public byte Item425;

                public byte Item426;

                public byte Item427;

                public byte Item428;

                public byte Item429;

                public byte Item430;

                public byte Item431;

                public byte Item432;

                public byte Item433;

                public byte Item434;

                public byte Item435;

                public byte Item436;

                public byte Item437;

                public byte Item438;

                public byte Item439;

                public byte Item440;

                public byte Item441;

                public byte Item442;

                public byte Item443;

                public byte Item444;

                public byte Item445;

                public byte Item446;

                public byte Item447;

                public byte Item448;

                public byte Item449;

                public byte Item450;

                public byte Item451;

                public byte Item452;

                public byte Item453;

                public byte Item454;

                public byte Item455;

                public byte Item456;

                public byte Item457;

                public byte Item458;

                public byte Item459;

                public byte Item460;

                public byte Item461;

                public byte Item462;

                public byte Item463;

                public byte Item464;

                public byte Item465;

                public byte Item466;

                public byte Item467;

                public byte Item468;

                public byte Item469;

                public byte Item470;

                public byte Item471;

                public byte Item472;

                public byte Item473;

                public byte Item474;

                public byte Item475;

                public byte Item476;

                public byte Item477;

                public byte Item478;

                public byte Item479;

                public byte Item480;

                public byte Item481;

                public byte Item482;

                public byte Item483;

                public byte Item484;

                public byte Item485;

                public byte Item486;

                public byte Item487;

                public byte Item488;

                public byte Item489;

                public byte Item490;

                public byte Item491;

                public byte Item492;

                public byte Item493;

                public byte Item494;

                public byte Item495;

                public byte Item496;

                public byte Item497;

                public byte Item498;

                public byte Item499;

                public byte Item500;

                public byte Item501;

                public byte Item502;

                public byte Item503;

                public byte Item504;

                public byte Item505;

                public byte Item506;

                public byte Item507;

                public byte Item508;

                public byte Item509;

                public byte Item510;

                public byte Item511;
            }
        }

        public partial struct ecs_strbuf_element_str
        {
            public ecs_strbuf_element super;

            public byte* alloc_str;
        }

        public partial struct ecs_strbuf_list_elem
        {
            public int count;

            public byte* separator;
        }

        public partial struct ecs_strbuf_t
        {
            public byte* buf;

            public int max;

            public int size;

            public int elementCount;

            public ecs_strbuf_element_embedded firstElement;

            public ecs_strbuf_element* current;

            public list_stack_FixedBuffer list_stack;

            public int list_sp;

            public byte* content;

            public int length;

            public partial struct list_stack_FixedBuffer
            {
                public ecs_strbuf_list_elem Item0;

                public ecs_strbuf_list_elem Item1;

                public ecs_strbuf_list_elem Item2;

                public ecs_strbuf_list_elem Item3;

                public ecs_strbuf_list_elem Item4;

                public ecs_strbuf_list_elem Item5;

                public ecs_strbuf_list_elem Item6;

                public ecs_strbuf_list_elem Item7;

                public ecs_strbuf_list_elem Item8;

                public ecs_strbuf_list_elem Item9;

                public ecs_strbuf_list_elem Item10;

                public ecs_strbuf_list_elem Item11;

                public ecs_strbuf_list_elem Item12;

                public ecs_strbuf_list_elem Item13;

                public ecs_strbuf_list_elem Item14;

                public ecs_strbuf_list_elem Item15;

                public ecs_strbuf_list_elem Item16;

                public ecs_strbuf_list_elem Item17;

                public ecs_strbuf_list_elem Item18;

                public ecs_strbuf_list_elem Item19;

                public ecs_strbuf_list_elem Item20;

                public ecs_strbuf_list_elem Item21;

                public ecs_strbuf_list_elem Item22;

                public ecs_strbuf_list_elem Item23;

                public ecs_strbuf_list_elem Item24;

                public ecs_strbuf_list_elem Item25;

                public ecs_strbuf_list_elem Item26;

                public ecs_strbuf_list_elem Item27;

                public ecs_strbuf_list_elem Item28;

                public ecs_strbuf_list_elem Item29;

                public ecs_strbuf_list_elem Item30;

                public ecs_strbuf_list_elem Item31;
            }
        }

        public partial struct ecs_struct_desc_t
        {
            public ulong entity;

            public members_FixedBuffer members;

            public partial struct members_FixedBuffer
            {
                public ecs_member_t Item0;

                public ecs_member_t Item1;

                public ecs_member_t Item2;

                public ecs_member_t Item3;

                public ecs_member_t Item4;

                public ecs_member_t Item5;

                public ecs_member_t Item6;

                public ecs_member_t Item7;

                public ecs_member_t Item8;

                public ecs_member_t Item9;

                public ecs_member_t Item10;

                public ecs_member_t Item11;

                public ecs_member_t Item12;

                public ecs_member_t Item13;

                public ecs_member_t Item14;

                public ecs_member_t Item15;

                public ecs_member_t Item16;

                public ecs_member_t Item17;

                public ecs_member_t Item18;

                public ecs_member_t Item19;

                public ecs_member_t Item20;

                public ecs_member_t Item21;

                public ecs_member_t Item22;

                public ecs_member_t Item23;

                public ecs_member_t Item24;

                public ecs_member_t Item25;

                public ecs_member_t Item26;

                public ecs_member_t Item27;

                public ecs_member_t Item28;

                public ecs_member_t Item29;

                public ecs_member_t Item30;

                public ecs_member_t Item31;
            }
        }

        public partial struct ecs_switch_t
        {
        }

        public partial struct ecs_system_desc_t
        {
            public int _canary;

            public ulong entity;

            public ecs_query_desc_t query;

            public System.IntPtr run; // delegate* unmanaged<ecs_iter_t*, void>

            public System.IntPtr callback; // delegate* unmanaged<ecs_iter_t*, void>

            public void* ctx;

            public void* binding_ctx;

            public System.IntPtr ctx_free; // delegate* unmanaged<void*, void>

            public System.IntPtr binding_ctx_free; // delegate* unmanaged<void*, void>

            public float interval;

            public int rate;

            public ulong tick_source;

            public byte multi_threaded;

            public byte no_readonly;
        }

        public partial struct ecs_system_stats_t
        {
            public long first_;

            public ecs_metric_t time_spent;

            public ecs_metric_t invoke_count;

            public ecs_metric_t active;

            public ecs_metric_t enabled;

            public long last_;

            public byte task;

            public ecs_query_stats_t query;
        }

        public partial struct ecs_table_cache_iter_t
        {
            public ecs_table_cache_hdr_t* cur;

            public ecs_table_cache_hdr_t* next;

            public ecs_table_cache_hdr_t* next_list;

            public partial struct ecs_table_cache_hdr_t
            {
            }
        }

        public partial struct ecs_table_range_t
        {
            public ecs_table_t* table;

            public int offset;

            public int count;
        }

        public partial struct ecs_table_record_t
        {
        }

        public partial struct ecs_table_t
        {
        }

        public partial struct ecs_term_id_t
        {
            public ulong id;

            public byte* name;

            public ulong trav;

            public uint flags;
        }

        public partial struct ecs_term_iter_t
        {
            public ecs_term_t term;

            public ecs_id_record_t* self_index;

            public ecs_id_record_t* set_index;

            public ecs_id_record_t* cur;

            public ecs_table_cache_iter_t it;

            public int index;

            public int observed_table_count;

            public ecs_table_t* table;

            public int cur_match;

            public int match_count;

            public int last_column;

            public byte empty_tables;

            public ulong id;

            public int column;

            public ulong subject;

            public int size;

            public void* ptr;
        }

        public partial struct ecs_term_t
        {
            public ulong id;

            public ecs_term_id_t src;

            public ecs_term_id_t first;

            public ecs_term_id_t second;

            public ecs_inout_kind_t inout;

            public ecs_oper_kind_t oper;

            public ulong id_flags;

            public byte* name;

            public int field_index;

            public ecs_id_record_t* idr;

            public ushort flags;

            public byte move;
        }

        public partial struct ecs_time_t
        {
            public uint sec;

            public uint nanosec;
        }

        public partial struct ecs_type_hooks_t
        {
            public System.IntPtr ctor; // delegate* unmanaged<void*, int, ecs_type_info_t*, void>

            public System.IntPtr dtor; // delegate* unmanaged<void*, int, ecs_type_info_t*, void>

            public System.IntPtr copy; // delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>

            public System.IntPtr move; // delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>

            public System.IntPtr copy_ctor; // delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>

            public System.IntPtr move_ctor; // delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>

            public System.IntPtr ctor_move_dtor; // delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>

            public System.IntPtr move_dtor; // delegate* unmanaged<void*, void*, int, ecs_type_info_t*, void>

            public System.IntPtr on_add; // delegate* unmanaged<ecs_iter_t*, void>

            public System.IntPtr on_set; // delegate* unmanaged<ecs_iter_t*, void>

            public System.IntPtr on_remove; // delegate* unmanaged<ecs_iter_t*, void>

            public void* ctx;

            public void* binding_ctx;

            public System.IntPtr ctx_free; // delegate* unmanaged<void*, void>

            public System.IntPtr binding_ctx_free; // delegate* unmanaged<void*, void>
        }

        public partial struct ecs_type_info_t
        {
            public int size;

            public int alignment;

            public ecs_type_hooks_t hooks;

            public ulong component;

            public byte* name;
        }

        public partial struct ecs_type_t
        {
            public ulong* array;

            public int count;
        }

        public partial struct ecs_unit_desc_t
        {
            public ulong entity;

            public byte* symbol;

            public ulong quantity;

            public ulong @base;

            public ulong over;

            public ecs_unit_translation_t translation;

            public ulong prefix;
        }

        public partial struct ecs_unit_prefix_desc_t
        {
            public ulong entity;

            public byte* symbol;

            public ecs_unit_translation_t translation;
        }

        public partial struct ecs_unit_translation_t
        {
            public int factor;

            public int power;
        }

        public partial struct ecs_value_t
        {
            public ulong type;

            public void* ptr;
        }

        public partial struct ecs_var_t
        {
            public ecs_table_range_t range;

            public ulong entity;
        }

        public partial struct ecs_vars_t
        {
            public ecs_world_t* world;

            public ecs_expr_var_scope_t root;

            public ecs_expr_var_scope_t* cur;
        }

        public partial struct ecs_vec_t
        {
            public void* array;

            public int count;

            public int size;

            public int elem_size;
        }

        public partial struct ecs_vector_desc_t
        {
            public ulong entity;

            public ulong type;
        }

        public partial struct ecs_worker_iter_t
        {
            public int index;

            public int count;
        }

        public partial struct ecs_world_info_t
        {
            public ulong last_component_id;

            public ulong min_id;

            public ulong max_id;

            public float delta_time_raw;

            public float delta_time;

            public float time_scale;

            public float target_fps;

            public float frame_time_total;

            public float system_time_total;

            public float emit_time_total;

            public float merge_time_total;

            public float world_time_total;

            public float world_time_total_raw;

            public float rematch_time_total;

            public long frame_count_total;

            public long merge_count_total;

            public long rematch_count_total;

            public long id_create_total;

            public long id_delete_total;

            public long table_create_total;

            public long table_delete_total;

            public long pipeline_build_count_total;

            public long systems_ran_frame;

            public long observers_ran_frame;

            public int id_count;

            public int tag_id_count;

            public int component_id_count;

            public int pair_id_count;

            public int wildcard_id_count;

            public int table_count;

            public int tag_table_count;

            public int trivial_table_count;

            public int empty_table_count;

            public int table_record_count;

            public int table_storage_count;

            public cmd_AnonymousRecord cmd;

            public byte* name_prefix;

            public partial struct cmd_AnonymousRecord
            {
                public long add_count;

                public long remove_count;

                public long delete_count;

                public long clear_count;

                public long set_count;

                public long get_mut_count;

                public long modified_count;

                public long other_count;

                public long discard_count;

                public long batched_entity_count;

                public long batched_command_count;
            }
        }

        public partial struct ecs_world_stats_t
        {
            public long first_;

            public entities_AnonymousRecord entities;

            public ids_AnonymousRecord ids;

            public tables_AnonymousRecord tables;

            public queries_AnonymousRecord queries;

            public commands_AnonymousRecord commands;

            public frame_AnonymousRecord frame;

            public performance_AnonymousRecord performance;

            public memory_AnonymousRecord memory;

            public rest_AnonymousRecord rest;

            public http_AnonymousRecord http;

            public long last_;

            public int t;

            public partial struct entities_AnonymousRecord
            {
                public ecs_metric_t count;

                public ecs_metric_t not_alive_count;
            }

            public partial struct ids_AnonymousRecord
            {
                public ecs_metric_t count;

                public ecs_metric_t tag_count;

                public ecs_metric_t component_count;

                public ecs_metric_t pair_count;

                public ecs_metric_t wildcard_count;

                public ecs_metric_t type_count;

                public ecs_metric_t create_count;

                public ecs_metric_t delete_count;
            }

            public partial struct tables_AnonymousRecord
            {
                public ecs_metric_t count;

                public ecs_metric_t empty_count;

                public ecs_metric_t tag_only_count;

                public ecs_metric_t trivial_only_count;

                public ecs_metric_t record_count;

                public ecs_metric_t storage_count;

                public ecs_metric_t create_count;

                public ecs_metric_t delete_count;
            }

            public partial struct queries_AnonymousRecord
            {
                public ecs_metric_t query_count;

                public ecs_metric_t observer_count;

                public ecs_metric_t system_count;
            }

            public partial struct commands_AnonymousRecord
            {
                public ecs_metric_t add_count;

                public ecs_metric_t remove_count;

                public ecs_metric_t delete_count;

                public ecs_metric_t clear_count;

                public ecs_metric_t set_count;

                public ecs_metric_t get_mut_count;

                public ecs_metric_t modified_count;

                public ecs_metric_t other_count;

                public ecs_metric_t discard_count;

                public ecs_metric_t batched_entity_count;

                public ecs_metric_t batched_count;
            }

            public partial struct frame_AnonymousRecord
            {
                public ecs_metric_t frame_count;

                public ecs_metric_t merge_count;

                public ecs_metric_t rematch_count;

                public ecs_metric_t pipeline_build_count;

                public ecs_metric_t systems_ran;

                public ecs_metric_t observers_ran;

                public ecs_metric_t event_emit_count;
            }

            public partial struct performance_AnonymousRecord
            {
                public ecs_metric_t world_time_raw;

                public ecs_metric_t world_time;

                public ecs_metric_t frame_time;

                public ecs_metric_t system_time;

                public ecs_metric_t emit_time;

                public ecs_metric_t merge_time;

                public ecs_metric_t rematch_time;

                public ecs_metric_t fps;

                public ecs_metric_t delta_time;
            }

            public partial struct memory_AnonymousRecord
            {
                public ecs_metric_t alloc_count;

                public ecs_metric_t realloc_count;

                public ecs_metric_t free_count;

                public ecs_metric_t outstanding_alloc_count;

                public ecs_metric_t block_alloc_count;

                public ecs_metric_t block_free_count;

                public ecs_metric_t block_outstanding_alloc_count;

                public ecs_metric_t stack_alloc_count;

                public ecs_metric_t stack_free_count;

                public ecs_metric_t stack_outstanding_alloc_count;
            }

            public partial struct rest_AnonymousRecord
            {
                public ecs_metric_t request_count;

                public ecs_metric_t entity_count;

                public ecs_metric_t entity_error_count;

                public ecs_metric_t query_count;

                public ecs_metric_t query_error_count;

                public ecs_metric_t query_name_count;

                public ecs_metric_t query_name_error_count;

                public ecs_metric_t query_name_from_cache_count;

                public ecs_metric_t enable_count;

                public ecs_metric_t enable_error_count;

                public ecs_metric_t world_stats_count;

                public ecs_metric_t pipeline_stats_count;

                public ecs_metric_t stats_error_count;
            }

            public partial struct http_AnonymousRecord
            {
                public ecs_metric_t request_received_count;

                public ecs_metric_t request_invalid_count;

                public ecs_metric_t request_handled_ok_count;

                public ecs_metric_t request_handled_error_count;

                public ecs_metric_t request_not_handled_count;

                public ecs_metric_t request_preflight_count;

                public ecs_metric_t send_ok_count;

                public ecs_metric_t send_error_count;

                public ecs_metric_t busy_count;
            }
        }

        public partial struct ecs_world_t
        {
        }

        public partial struct ecs_world_to_json_desc_t
        {
            public byte serialize_builtin;

            public byte serialize_modules;
        }

        public partial struct EcsAlertInstance
        {
            public byte* message;
        }

        public partial struct EcsAlertsActive
        {
            public ecs_map_t alerts;
        }

        public partial struct EcsArray
        {
            public ulong type;

            public int count;
        }

        public partial struct EcsBitmask
        {
            public ecs_map_t constants;
        }

        public partial struct EcsComponent
        {
            public int size;

            public int alignment;
        }

        public partial struct EcsDocDescription
        {
            public byte* value;
        }

        public partial struct EcsEnum
        {
            public ecs_map_t constants;
        }

        public partial struct EcsIdentifier
        {
            public byte* value;

            public int length;

            public ulong hash;

            public ulong index_hash;

            public ecs_hashmap_t* index;
        }

        public partial struct EcsMember
        {
            public ulong type;

            public int count;

            public ulong unit;

            public int offset;
        }

        public partial struct EcsMemberRanges
        {
            public ecs_member_value_range_t value;

            public ecs_member_value_range_t warning;

            public ecs_member_value_range_t error;
        }

        public partial struct EcsMetaType
        {
            public ecs_type_kind_t kind;

            public byte existing;

            public byte partial;
        }

        public partial struct EcsMetaTypeSerialized
        {
            public ecs_vec_t ops;
        }

        public partial struct EcsMetricSource
        {
            public ulong entity;
        }

        public partial struct EcsMetricValue
        {
            public double value;
        }

        public partial struct EcsOpaque
        {
            public ulong as_type;

            public System.IntPtr serialize; // delegate* unmanaged<ecs_serializer_t*, void*, int>

            public System.IntPtr assign_bool; // delegate* unmanaged<void*, byte, void>

            public System.IntPtr assign_char; // delegate* unmanaged<void*, byte, void>

            public System.IntPtr assign_int; // delegate* unmanaged<void*, long, void>

            public System.IntPtr assign_uint; // delegate* unmanaged<void*, ulong, void>

            public System.IntPtr assign_float; // delegate* unmanaged<void*, double, void>

            public System.IntPtr assign_string; // delegate* unmanaged<void*, byte*, void>

            public System.IntPtr assign_entity; // delegate* unmanaged<void*, ecs_world_t*, ulong, void>

            public System.IntPtr assign_null; // delegate* unmanaged<void*, void>

            public System.IntPtr clear; // delegate* unmanaged<void*, void>

            public System.IntPtr ensure_element; // delegate* unmanaged<void*, ulong, void*>

            public System.IntPtr ensure_member; // delegate* unmanaged<void*, byte*, void*>

            public System.IntPtr count; // delegate* unmanaged<void*, ulong>

            public System.IntPtr resize; // delegate* unmanaged<void*, ulong, void>
        }

        public partial struct EcsPipelineStats
        {
            public EcsStatsHeader hdr;

            public ecs_pipeline_stats_t stats;
        }

        public partial struct EcsPoly
        {
            public void* poly;
        }

        public partial struct EcsPrimitive
        {
            public ecs_primitive_kind_t kind;
        }

        public partial struct EcsRateFilter
        {
            public ulong src;

            public int rate;

            public int tick_count;

            public float time_elapsed;
        }

        public partial struct EcsRest
        {
            public ushort port;

            public byte* ipaddr;

            public void* impl;
        }

        public partial struct EcsScript
        {
            public ecs_vec_t using_;

            public byte* script;

            public ecs_vec_t prop_defaults;

            public ecs_world_t* world;
        }

        public partial struct EcsStatsHeader
        {
            public float elapsed;

            public int reduce_count;
        }

        public partial struct EcsStruct
        {
            public ecs_vec_t members;
        }

        public partial struct EcsTarget
        {
            public int count;

            public ecs_record_t* target;
        }

        public partial struct EcsTickSource
        {
            public byte tick;

            public float time_elapsed;
        }

        public partial struct EcsTimer
        {
            public float timeout;

            public float time;

            public float overshoot;

            public int fired_count;

            public byte active;

            public byte single_shot;
        }

        public partial struct EcsUnit
        {
            public byte* symbol;

            public ulong prefix;

            public ulong @base;

            public ulong over;

            public ecs_unit_translation_t translation;
        }

        public partial struct EcsUnitPrefix
        {
            public byte* symbol;

            public ecs_unit_translation_t translation;
        }

        public partial struct EcsVector
        {
            public ulong type;
        }

        public partial struct EcsWorldStats
        {
            public EcsStatsHeader hdr;

            public ecs_world_stats_t stats;
        }

        public partial struct EcsWorldSummary
        {
            public double target_fps;

            public double frame_time_total;

            public double system_time_total;

            public double merge_time_total;

            public double frame_time_last;

            public double system_time_last;

            public double merge_time_last;
        }

        public partial struct flecs_hashmap_iter_t
        {
            public ecs_map_iter_t it;

            public ecs_hm_bucket_t* bucket;

            public int index;
        }

        public partial struct flecs_hashmap_result_t
        {
            public void* key;

            public void* value;

            public ulong hash;
        }

        public enum ecs_http_method_t : uint
        {
            EcsHttpGet = 0,
            EcsHttpPost = 1,
            EcsHttpPut = 2,
            EcsHttpDelete = 3,
            EcsHttpOptions = 4,
            EcsHttpMethodUnsupported = 5
        }

        public enum ecs_inout_kind_t : uint
        {
            EcsInOutDefault = 0,
            EcsInOutNone = 1,
            EcsInOut = 2,
            EcsIn = 3,
            EcsOut = 4
        }

        public enum ecs_iter_kind_t : uint
        {
            EcsIterEvalCondition = 0,
            EcsIterEvalTables = 1,
            EcsIterEvalChain = 2,
            EcsIterEvalNone = 3
        }

        public enum ecs_meta_type_op_kind_t : uint
        {
            EcsOpArray = 0,
            EcsOpVector = 1,
            EcsOpOpaque = 2,
            EcsOpPush = 3,
            EcsOpPop = 4,
            EcsOpScope = 5,
            EcsOpEnum = 6,
            EcsOpBitmask = 7,
            EcsOpPrimitive = 8,
            EcsOpBool = 9,
            EcsOpChar = 10,
            EcsOpByte = 11,
            EcsOpU8 = 12,
            EcsOpU16 = 13,
            EcsOpU32 = 14,
            EcsOpU64 = 15,
            EcsOpI8 = 16,
            EcsOpI16 = 17,
            EcsOpI32 = 18,
            EcsOpI64 = 19,
            EcsOpF32 = 20,
            EcsOpF64 = 21,
            EcsOpUPtr = 22,
            EcsOpIPtr = 23,
            EcsOpString = 24,
            EcsOpEntity = 25,
            EcsMetaTypeOpKindLast = 25
        }

        public enum ecs_oper_kind_t : uint
        {
            EcsAnd = 0,
            EcsOr = 1,
            EcsNot = 2,
            EcsOptional = 3,
            EcsAndFrom = 4,
            EcsOrFrom = 5,
            EcsNotFrom = 6
        }

        public enum ecs_primitive_kind_t : uint
        {
            EcsBool = 1,
            EcsChar = 2,
            EcsByte = 3,
            EcsU8 = 4,
            EcsU16 = 5,
            EcsU32 = 6,
            EcsU64 = 7,
            EcsI8 = 8,
            EcsI16 = 9,
            EcsI32 = 10,
            EcsI64 = 11,
            EcsF32 = 12,
            EcsF64 = 13,
            EcsUPtr = 14,
            EcsIPtr = 15,
            EcsString = 16,
            EcsEntity = 17,
            EcsPrimitiveKindLast = 17
        }

        public enum ecs_type_kind_t : uint
        {
            EcsPrimitiveType = 0,
            EcsBitmaskType = 1,
            EcsEnumType = 2,
            EcsStructType = 3,
            EcsArrayType = 4,
            EcsVectorType = 5,
            EcsOpaqueType = 6,
            EcsTypeKindLast = 6
        }

        public const ecs_http_method_t EcsHttpGet = ecs_http_method_t.EcsHttpGet;

        public const ecs_http_method_t EcsHttpPost = ecs_http_method_t.EcsHttpPost;

        public const ecs_http_method_t EcsHttpPut = ecs_http_method_t.EcsHttpPut;

        public const ecs_http_method_t EcsHttpDelete = ecs_http_method_t.EcsHttpDelete;

        public const ecs_http_method_t EcsHttpOptions = ecs_http_method_t.EcsHttpOptions;

        public const ecs_http_method_t EcsHttpMethodUnsupported = ecs_http_method_t.EcsHttpMethodUnsupported;

        public const ecs_inout_kind_t EcsInOutDefault = ecs_inout_kind_t.EcsInOutDefault;

        public const ecs_inout_kind_t EcsInOutNone = ecs_inout_kind_t.EcsInOutNone;

        public const ecs_inout_kind_t EcsInOut = ecs_inout_kind_t.EcsInOut;

        public const ecs_inout_kind_t EcsIn = ecs_inout_kind_t.EcsIn;

        public const ecs_inout_kind_t EcsOut = ecs_inout_kind_t.EcsOut;

        public const ecs_iter_kind_t EcsIterEvalCondition = ecs_iter_kind_t.EcsIterEvalCondition;

        public const ecs_iter_kind_t EcsIterEvalTables = ecs_iter_kind_t.EcsIterEvalTables;

        public const ecs_iter_kind_t EcsIterEvalChain = ecs_iter_kind_t.EcsIterEvalChain;

        public const ecs_iter_kind_t EcsIterEvalNone = ecs_iter_kind_t.EcsIterEvalNone;

        public const ecs_meta_type_op_kind_t EcsOpArray = ecs_meta_type_op_kind_t.EcsOpArray;

        public const ecs_meta_type_op_kind_t EcsOpVector = ecs_meta_type_op_kind_t.EcsOpVector;

        public const ecs_meta_type_op_kind_t EcsOpOpaque = ecs_meta_type_op_kind_t.EcsOpOpaque;

        public const ecs_meta_type_op_kind_t EcsOpPush = ecs_meta_type_op_kind_t.EcsOpPush;

        public const ecs_meta_type_op_kind_t EcsOpPop = ecs_meta_type_op_kind_t.EcsOpPop;

        public const ecs_meta_type_op_kind_t EcsOpScope = ecs_meta_type_op_kind_t.EcsOpScope;

        public const ecs_meta_type_op_kind_t EcsOpEnum = ecs_meta_type_op_kind_t.EcsOpEnum;

        public const ecs_meta_type_op_kind_t EcsOpBitmask = ecs_meta_type_op_kind_t.EcsOpBitmask;

        public const ecs_meta_type_op_kind_t EcsOpPrimitive = ecs_meta_type_op_kind_t.EcsOpPrimitive;

        public const ecs_meta_type_op_kind_t EcsOpBool = ecs_meta_type_op_kind_t.EcsOpBool;

        public const ecs_meta_type_op_kind_t EcsOpChar = ecs_meta_type_op_kind_t.EcsOpChar;

        public const ecs_meta_type_op_kind_t EcsOpByte = ecs_meta_type_op_kind_t.EcsOpByte;

        public const ecs_meta_type_op_kind_t EcsOpU8 = ecs_meta_type_op_kind_t.EcsOpU8;

        public const ecs_meta_type_op_kind_t EcsOpU16 = ecs_meta_type_op_kind_t.EcsOpU16;

        public const ecs_meta_type_op_kind_t EcsOpU32 = ecs_meta_type_op_kind_t.EcsOpU32;

        public const ecs_meta_type_op_kind_t EcsOpU64 = ecs_meta_type_op_kind_t.EcsOpU64;

        public const ecs_meta_type_op_kind_t EcsOpI8 = ecs_meta_type_op_kind_t.EcsOpI8;

        public const ecs_meta_type_op_kind_t EcsOpI16 = ecs_meta_type_op_kind_t.EcsOpI16;

        public const ecs_meta_type_op_kind_t EcsOpI32 = ecs_meta_type_op_kind_t.EcsOpI32;

        public const ecs_meta_type_op_kind_t EcsOpI64 = ecs_meta_type_op_kind_t.EcsOpI64;

        public const ecs_meta_type_op_kind_t EcsOpF32 = ecs_meta_type_op_kind_t.EcsOpF32;

        public const ecs_meta_type_op_kind_t EcsOpF64 = ecs_meta_type_op_kind_t.EcsOpF64;

        public const ecs_meta_type_op_kind_t EcsOpUPtr = ecs_meta_type_op_kind_t.EcsOpUPtr;

        public const ecs_meta_type_op_kind_t EcsOpIPtr = ecs_meta_type_op_kind_t.EcsOpIPtr;

        public const ecs_meta_type_op_kind_t EcsOpString = ecs_meta_type_op_kind_t.EcsOpString;

        public const ecs_meta_type_op_kind_t EcsOpEntity = ecs_meta_type_op_kind_t.EcsOpEntity;

        public const ecs_meta_type_op_kind_t EcsMetaTypeOpKindLast = ecs_meta_type_op_kind_t.EcsMetaTypeOpKindLast;

        public const ecs_oper_kind_t EcsAnd = ecs_oper_kind_t.EcsAnd;

        public const ecs_oper_kind_t EcsOr = ecs_oper_kind_t.EcsOr;

        public const ecs_oper_kind_t EcsNot = ecs_oper_kind_t.EcsNot;

        public const ecs_oper_kind_t EcsOptional = ecs_oper_kind_t.EcsOptional;

        public const ecs_oper_kind_t EcsAndFrom = ecs_oper_kind_t.EcsAndFrom;

        public const ecs_oper_kind_t EcsOrFrom = ecs_oper_kind_t.EcsOrFrom;

        public const ecs_oper_kind_t EcsNotFrom = ecs_oper_kind_t.EcsNotFrom;

        public const ecs_primitive_kind_t EcsBool = ecs_primitive_kind_t.EcsBool;

        public const ecs_primitive_kind_t EcsChar = ecs_primitive_kind_t.EcsChar;

        public const ecs_primitive_kind_t EcsByte = ecs_primitive_kind_t.EcsByte;

        public const ecs_primitive_kind_t EcsU8 = ecs_primitive_kind_t.EcsU8;

        public const ecs_primitive_kind_t EcsU16 = ecs_primitive_kind_t.EcsU16;

        public const ecs_primitive_kind_t EcsU32 = ecs_primitive_kind_t.EcsU32;

        public const ecs_primitive_kind_t EcsU64 = ecs_primitive_kind_t.EcsU64;

        public const ecs_primitive_kind_t EcsI8 = ecs_primitive_kind_t.EcsI8;

        public const ecs_primitive_kind_t EcsI16 = ecs_primitive_kind_t.EcsI16;

        public const ecs_primitive_kind_t EcsI32 = ecs_primitive_kind_t.EcsI32;

        public const ecs_primitive_kind_t EcsI64 = ecs_primitive_kind_t.EcsI64;

        public const ecs_primitive_kind_t EcsF32 = ecs_primitive_kind_t.EcsF32;

        public const ecs_primitive_kind_t EcsF64 = ecs_primitive_kind_t.EcsF64;

        public const ecs_primitive_kind_t EcsUPtr = ecs_primitive_kind_t.EcsUPtr;

        public const ecs_primitive_kind_t EcsIPtr = ecs_primitive_kind_t.EcsIPtr;

        public const ecs_primitive_kind_t EcsString = ecs_primitive_kind_t.EcsString;

        public const ecs_primitive_kind_t EcsEntity = ecs_primitive_kind_t.EcsEntity;

        public const ecs_primitive_kind_t EcsPrimitiveKindLast = ecs_primitive_kind_t.EcsPrimitiveKindLast;

        public const ecs_type_kind_t EcsPrimitiveType = ecs_type_kind_t.EcsPrimitiveType;

        public const ecs_type_kind_t EcsBitmaskType = ecs_type_kind_t.EcsBitmaskType;

        public const ecs_type_kind_t EcsEnumType = ecs_type_kind_t.EcsEnumType;

        public const ecs_type_kind_t EcsStructType = ecs_type_kind_t.EcsStructType;

        public const ecs_type_kind_t EcsArrayType = ecs_type_kind_t.EcsArrayType;

        public const ecs_type_kind_t EcsVectorType = ecs_type_kind_t.EcsVectorType;

        public const ecs_type_kind_t EcsOpaqueType = ecs_type_kind_t.EcsOpaqueType;

        public const ecs_type_kind_t EcsTypeKindLast = ecs_type_kind_t.EcsTypeKindLast;

        public const int ECS_ACCESS_VIOLATION = 40;

        public const int ECS_ALERT_MAX_SEVERITY_FILTERS = 4;

        public const int ECS_ALREADY_DEFINED = 8;

        public const int ECS_ALREADY_IN_USE = 30;

        public const string ECS_BLACK = "[1;30m";

        public const string ECS_BLUE = "[0;34m";

        public const string ECS_BOLD = "[1;49m";

        public const int ECS_CLANG_VERSION = 16;

        public const int ECS_COLUMN_INDEX_OUT_OF_RANGE = 41;

        public const int ECS_COLUMN_IS_NOT_SHARED = 42;

        public const int ECS_COLUMN_IS_SHARED = 43;

        public const int ECS_COLUMN_TYPE_MISMATCH = 45;

        public const ulong ECS_COMPONENT_MASK = 1152921504606846975;

        public const int ECS_COMPONENT_NOT_REGISTERED = 25;

        public const int ECS_CONSTRAINT_VIOLATED = 3;

        public const string ECS_CYAN = "[0;36m";

        public const int ECS_CYCLE_DETECTED = 13;

        public const int ECS_DOUBLE_FREE = 15;

        public const ulong ECS_ENTITY_MASK = 4294967295;

        public const int ecs_filter_t_magic = 1701016422;

        public const ulong ECS_GENERATION_MASK = 281470681743360;

        public const string ECS_GREEN = "[0;32m";

        public const string ECS_GREY = "[0;37m";

        public const int ECS_HTTP_HEADER_COUNT_MAX = 32;

        public const int ECS_HTTP_QUERY_PARAM_COUNT_MAX = 32;

        public const ulong ECS_ID_FLAG_BIT = 9223372036854775808;

        public const ulong ECS_ID_FLAGS_MASK = 17293822569102704640;

        public const int ECS_ID_IN_USE = 12;

        public const int ECS_INCONSISTENT_COMPONENT_ACTION = 27;

        public const int ECS_INCONSISTENT_COMPONENT_ID = 26;

        public const int ECS_INCONSISTENT_NAME = 20;

        public const int ECS_INTERNAL_ERROR = 7;

        public const int ECS_INVALID_COMPONENT_ALIGNMENT = 24;

        public const int ECS_INVALID_COMPONENT_SIZE = 23;

        public const int ECS_INVALID_CONVERSION = 11;

        public const int ECS_INVALID_FROM_WORKER = 72;

        public const int ECS_INVALID_OPERATION = 1;

        public const int ECS_INVALID_PARAMETER = 2;

        public const int ECS_INVALID_WHILE_READONLY = 70;

        public const int ECS_LEAK_DETECTED = 14;

        public const int ECS_LOCKED_STORAGE = 71;

        public const string ECS_MAGENTA = "[0;35m";

        public const uint ECS_MAX_COMPONENT_ID = 268435455;

        public const int ECS_MAX_RECURSION = 512;

        public const int ECS_MAX_TOKEN_SIZE = 256;

        public const int ECS_MEMBER_DESC_CACHE_SIZE = 32;

        public const int ECS_META_MAX_SCOPE_DEPTH = 32;

        public const int ECS_MISSING_OS_API = 9;

        public const int ECS_MISSING_SYMBOL = 29;

        public const int ECS_MODULE_UNDEFINED = 28;

        public const int ECS_NAME_IN_USE = 21;

        public const string ECS_NORMAL = "[0;49m";

        public const int ECS_NOT_A_COMPONENT = 22;

        public const int ecs_observer_t_magic = 1701016418;

        public const int ECS_OPERATION_FAILED = 10;

        public const int ECS_OUT_OF_MEMORY = 4;

        public const int ECS_OUT_OF_RANGE = 5;

        public const int ecs_query_t_magic = 1701016433;

        public const string ECS_RED = "[0;31m";

        public const int ECS_REST_DEFAULT_PORT = 27750;

        public const uint ECS_ROW_FLAGS_MASK = 4026531840;

        public const uint ECS_ROW_MASK = 268435455;

        public const int ecs_rule_t_magic = 1701016437;

        public const int ecs_stage_t_magic = 1701016435;

        public const int ECS_STAT_WINDOW = 60;

        public const int ECS_STRBUF_ELEMENT_SIZE = 511;

        public const int ECS_STRBUF_MAX_LIST_DEPTH = 32;

        public const int ecs_table_t_magic = 1701016436;

        public const int ecs_trigger_t_magic = 1701016434;

        public const int ECS_UNSUPPORTED = 6;

        public const string ECS_WHITE = "[1;37m";

        public const int ecs_world_t_magic = 1701016439;

        public const string ECS_YELLOW = "[0;33m";

        public const uint EcsAperiodicComponentMonitors = 4;

        public const uint EcsAperiodicEmptyQueries = 16;

        public const uint EcsAperiodicEmptyTables = 2;

        public const uint EcsCascade = 32;

        public const uint EcsDown = 8;

        public const uint EcsEntityIsId = 2147483648;

        public const uint EcsEntityIsTarget = 1073741824;

        public const uint EcsEntityIsTraversable = 536870912;

        public const uint EcsEventNoOnSet = 65536;

        public const uint EcsEventTableOnly = 16;

        public const uint EcsFilter = 1024;

        public const uint EcsFilterHasCondSet = 1024;

        public const uint EcsFilterHasPred = 4096;

        public const uint EcsFilterHasScopes = 8192;

        public const uint EcsFilterIsInstanced = 256;

        public const uint EcsFilterMatchAnything = 64;

        public const uint EcsFilterMatchDisabled = 16;

        public const uint EcsFilterMatchEmptyTables = 32;

        public const uint EcsFilterMatchOnlyThis = 4;

        public const uint EcsFilterMatchPrefab = 8;

        public const uint EcsFilterMatchThis = 2;

        public const uint EcsFilterNoData = 128;

        public const uint EcsFilterPopulate = 512;

        public const uint EcsFilterUnresolvedByName = 2048;

        public const int EcsFirstUserComponentId = 8;

        public const int EcsFirstUserEntityId = 384;

        public const uint EcsIdAlwaysOverride = 4096;

        public const uint EcsIdDontInherit = 128;

        public const uint EcsIdEventMask = 16711680;

        public const uint EcsIdExclusive = 64;

        public const uint EcsIdHasOnAdd = 65536;

        public const uint EcsIdHasOnRemove = 131072;

        public const uint EcsIdHasOnSet = 262144;

        public const uint EcsIdHasOnTableCreate = 4194304;

        public const uint EcsIdHasOnTableDelete = 8388608;

        public const uint EcsIdHasOnTableEmpty = 2097152;

        public const uint EcsIdHasOnTableFill = 1048576;

        public const uint EcsIdHasUnSet = 524288;

        public const uint EcsIdMarkedForDelete = 1073741824;

        public const uint EcsIdOnDeleteDelete = 2;

        public const uint EcsIdOnDeleteMask = 7;

        public const uint EcsIdOnDeleteObjectDelete = 16;

        public const uint EcsIdOnDeleteObjectMask = 56;

        public const uint EcsIdOnDeleteObjectPanic = 32;

        public const uint EcsIdOnDeleteObjectRemove = 8;

        public const uint EcsIdOnDeletePanic = 4;

        public const uint EcsIdOnDeleteRemove = 1;

        public const uint EcsIdTag = 512;

        public const uint EcsIdTraversable = 256;

        public const uint EcsIdUnion = 2048;

        public const uint EcsIdWith = 1024;

        public const uint EcsIsEntity = 256;

        public const uint EcsIsName = 512;

        public const uint EcsIsVariable = 128;

        public const uint EcsIterEntityOptional = 32;

        public const uint EcsIterHasCondSet = 1024;

        public const uint EcsIterHasShared = 8;

        public const uint EcsIterIgnoreThis = 128;

        public const uint EcsIterIsInstanced = 4;

        public const uint EcsIterIsValid = 1;

        public const uint EcsIterMatchVar = 256;

        public const int EcsIterNext = 1;

        public const int EcsIterNextYield = 0;

        public const uint EcsIterNoData = 2;

        public const uint EcsIterNoResults = 64;

        public const uint EcsIterProfile = 2048;

        public const uint EcsIterTableOnly = 16;

        public const int EcsIterYield = -1;

        public const uint EcsOsApiHighResolutionTimer = 1;

        public const uint EcsOsApiLogWithColors = 2;

        public const uint EcsOsApiLogWithTimeDelta = 8;

        public const uint EcsOsApiLogWithTimeStamp = 4;

        public const uint EcsParent = 64;

        public const uint EcsQueryHasMonitor = 64;

        public const uint EcsQueryHasNonThisOutTerms = 32;

        public const uint EcsQueryHasOutTerms = 16;

        public const uint EcsQueryHasRefs = 2;

        public const uint EcsQueryIsOrphaned = 8;

        public const uint EcsQueryIsSubquery = 4;

        public const uint EcsQueryTrivialIter = 128;

        public const uint EcsSelf = 2;

        public const uint EcsTableHasAddActions = 336392;

        public const uint EcsTableHasBuiltins = 2;

        public const uint EcsTableHasChildOf = 16;

        public const uint EcsTableHasCopy = 2048;

        public const uint EcsTableHasCtors = 512;

        public const uint EcsTableHasDtors = 1024;

        public const uint EcsTableHasIsA = 8;

        public const uint EcsTableHasLifecycle = 1536;

        public const uint EcsTableHasModule = 128;

        public const uint EcsTableHasMove = 4096;

        public const uint EcsTableHasName = 32;

        public const uint EcsTableHasOnAdd = 65536;

        public const uint EcsTableHasOnRemove = 131072;

        public const uint EcsTableHasOnSet = 262144;

        public const uint EcsTableHasOnTableCreate = 4194304;

        public const uint EcsTableHasOnTableDelete = 8388608;

        public const uint EcsTableHasOnTableEmpty = 2097152;

        public const uint EcsTableHasOnTableFill = 1048576;

        public const uint EcsTableHasOverrides = 32768;

        public const uint EcsTableHasPairs = 64;

        public const uint EcsTableHasRemoveActions = 656392;

        public const uint EcsTableHasTarget = 67108864;

        public const uint EcsTableHasToggle = 16384;

        public const uint EcsTableHasTraversable = 33554432;

        public const uint EcsTableHasUnion = 8192;

        public const uint EcsTableHasUnSet = 524288;

        public const uint EcsTableIsComplex = 26112;

        public const uint EcsTableIsDisabled = 256;

        public const uint EcsTableIsPrefab = 4;

        public const uint EcsTableMarkedForDelete = 1073741824;

        public const uint EcsTermIdInherited = 64;

        public const uint EcsTermMatchAny = 1;

        public const uint EcsTermMatchAnySrc = 2;

        public const uint EcsTermMatchDisabled = 128;

        public const uint EcsTermMatchPrefab = 256;

        public const uint EcsTermReflexive = 32;

        public const uint EcsTermSrcFirstEq = 4;

        public const uint EcsTermSrcSecondEq = 8;

        public const uint EcsTermTransitive = 16;

        public const uint EcsTraverseAll = 16;

        public const uint EcsTraverseFlags = 126;

        public const uint EcsUp = 4;

        public const uint EcsWorldFini = 16;

        public const uint EcsWorldInit = 4;

        public const uint EcsWorldMeasureFrameTime = 32;

        public const uint EcsWorldMeasureSystemTime = 64;

        public const uint EcsWorldMultiThreaded = 128;

        public const uint EcsWorldQuit = 8;

        public const uint EcsWorldQuitWorkers = 1;

        public const uint EcsWorldReadonly = 2;

        public const int FLECS_ENTITY_PAGE_BITS = 12;

        public const int FLECS_EVENT_DESC_MAX = 8;

        public const int FLECS_HI_COMPONENT_ID = 256;

        public const int FLECS_HI_ID_RECORD_ID = 1024;

        public const int FLECS_ID_DESC_MAX = 32;

        public const int FLECS_ID0ID_ = 0;

        public const int flecs_iter_cache_all = 255;

        public const uint flecs_iter_cache_columns = 2;

        public const uint flecs_iter_cache_ids = 1;

        public const uint flecs_iter_cache_match_indices = 16;

        public const uint flecs_iter_cache_ptrs = 8;

        public const uint flecs_iter_cache_sources = 4;

        public const uint flecs_iter_cache_variables = 32;

        public const int FLECS_QUERY_SCOPE_NESTING_MAX = 8;

        public const int FLECS_SPARSE_PAGE_BITS = 12;

        public const int FLECS_SPARSE_PAGE_SIZE = 4096;

        public const int FLECS_TERM_DESC_MAX = 16;

        public const int FLECS_VARIABLE_COUNT_MAX = 64;

        private static void* ECS_AND_Ptr;

        private static void* ecs_block_allocator_alloc_count_Ptr;

        private static void* ecs_block_allocator_free_count_Ptr;

        private static void* ECS_FILTER_INIT_Ptr;

        private static void* ecs_http_busy_count_Ptr;

        private static void* ecs_http_request_handled_error_count_Ptr;

        private static void* ecs_http_request_handled_ok_count_Ptr;

        private static void* ecs_http_request_invalid_count_Ptr;

        private static void* ecs_http_request_not_handled_count_Ptr;

        private static void* ecs_http_request_preflight_count_Ptr;

        private static void* ecs_http_request_received_count_Ptr;

        private static void* ecs_http_send_error_count_Ptr;

        private static void* ecs_http_send_ok_count_Ptr;

        private static void* ecs_os_api_Ptr;

        private static void* ecs_os_api_calloc_count_Ptr;

        private static void* ecs_os_api_free_count_Ptr;

        private static void* ecs_os_api_malloc_count_Ptr;

        private static void* ecs_os_api_realloc_count_Ptr;

        private static void* ECS_OVERRIDE_Ptr;

        private static void* ECS_PAIR_Ptr;

        private static void* ecs_rest_delete_count_Ptr;

        private static void* ecs_rest_delete_error_count_Ptr;

        private static void* ecs_rest_enable_count_Ptr;

        private static void* ecs_rest_enable_error_count_Ptr;

        private static void* ecs_rest_entity_count_Ptr;

        private static void* ecs_rest_entity_error_count_Ptr;

        private static void* ecs_rest_pipeline_stats_count_Ptr;

        private static void* ecs_rest_query_count_Ptr;

        private static void* ecs_rest_query_error_count_Ptr;

        private static void* ecs_rest_query_name_count_Ptr;

        private static void* ecs_rest_query_name_error_count_Ptr;

        private static void* ecs_rest_query_name_from_cache_count_Ptr;

        private static void* ecs_rest_request_count_Ptr;

        private static void* ecs_rest_stats_error_count_Ptr;

        private static void* ecs_rest_world_stats_count_Ptr;

        private static void* ecs_stack_allocator_alloc_count_Ptr;

        private static void* ecs_stack_allocator_free_count_Ptr;

        private static void* ECS_TOGGLE_Ptr;

        private static void* EcsAcceleration_Ptr;

        private static void* EcsAcyclic_Ptr;

        private static void* EcsAlertCritical_Ptr;

        private static void* EcsAlertError_Ptr;

        private static void* EcsAlertInfo_Ptr;

        private static void* EcsAlertWarning_Ptr;

        private static void* EcsAlias_Ptr;

        private static void* EcsAlwaysOverride_Ptr;

        private static void* EcsAmount_Ptr;

        private static void* EcsAmpere_Ptr;

        private static void* EcsAngle_Ptr;

        private static void* EcsAny_Ptr;

        private static void* EcsAtto_Ptr;

        private static void* EcsBar_Ptr;

        private static void* EcsBel_Ptr;

        private static void* EcsBits_Ptr;

        private static void* EcsBitsPerSecond_Ptr;

        private static void* EcsBytes_Ptr;

        private static void* EcsBytesPerSecond_Ptr;

        private static void* EcsCandela_Ptr;

        private static void* EcsCelsius_Ptr;

        private static void* EcsCenti_Ptr;

        private static void* EcsCentiMeters_Ptr;

        private static void* EcsChildOf_Ptr;

        private static void* EcsConstant_Ptr;

        private static void* EcsCounter_Ptr;

        private static void* EcsCounterId_Ptr;

        private static void* EcsCounterIncrement_Ptr;

        private static void* EcsData_Ptr;

        private static void* EcsDataRate_Ptr;

        private static void* EcsDate_Ptr;

        private static void* EcsDays_Ptr;

        private static void* EcsDeca_Ptr;

        private static void* EcsDeci_Ptr;

        private static void* EcsDeciBel_Ptr;

        private static void* EcsDefaultChildComponent_Ptr;

        private static void* EcsDegrees_Ptr;

        private static void* EcsDelete_Ptr;

        private static void* EcsDependsOn_Ptr;

        private static void* EcsDisabled_Ptr;

        private static void* EcsDocBrief_Ptr;

        private static void* EcsDocColor_Ptr;

        private static void* EcsDocDetail_Ptr;

        private static void* EcsDocLink_Ptr;

        private static void* EcsDontInherit_Ptr;

        private static void* EcsDuration_Ptr;

        private static void* EcsElectricCurrent_Ptr;

        private static void* EcsEmpty_Ptr;

        private static void* EcsExa_Ptr;

        private static void* EcsExbi_Ptr;

        private static void* EcsExclusive_Ptr;

        private static void* EcsFahrenheit_Ptr;

        private static void* EcsFemto_Ptr;

        private static void* EcsFinal_Ptr;

        private static void* EcsFlatten_Ptr;

        private static void* EcsFlecs_Ptr;

        private static void* EcsFlecsCore_Ptr;

        private static void* EcsForce_Ptr;

        private static void* EcsFrequency_Ptr;

        private static void* EcsGauge_Ptr;

        private static void* EcsGibi_Ptr;

        private static void* EcsGibiBytes_Ptr;

        private static void* EcsGiga_Ptr;

        private static void* EcsGigaBits_Ptr;

        private static void* EcsGigaBitsPerSecond_Ptr;

        private static void* EcsGigaBytes_Ptr;

        private static void* EcsGigaBytesPerSecond_Ptr;

        private static void* EcsGigaHertz_Ptr;

        private static void* EcsGrams_Ptr;

        private static void* EcsHecto_Ptr;

        private static void* EcsHertz_Ptr;

        private static void* EcsHours_Ptr;

        private static void* EcsIsA_Ptr;

        private static void* EcsKelvin_Ptr;

        private static void* EcsKibi_Ptr;

        private static void* EcsKibiBytes_Ptr;

        private static void* EcsKilo_Ptr;

        private static void* EcsKiloBits_Ptr;

        private static void* EcsKiloBitsPerSecond_Ptr;

        private static void* EcsKiloBytes_Ptr;

        private static void* EcsKiloBytesPerSecond_Ptr;

        private static void* EcsKiloGrams_Ptr;

        private static void* EcsKiloHertz_Ptr;

        private static void* EcsKiloMeters_Ptr;

        private static void* EcsKiloMetersPerHour_Ptr;

        private static void* EcsKiloMetersPerSecond_Ptr;

        private static void* EcsLength_Ptr;

        private static void* EcsLuminousIntensity_Ptr;

        private static void* EcsMass_Ptr;

        private static void* EcsMebi_Ptr;

        private static void* EcsMebiBytes_Ptr;

        private static void* EcsMega_Ptr;

        private static void* EcsMegaBits_Ptr;

        private static void* EcsMegaBitsPerSecond_Ptr;

        private static void* EcsMegaBytes_Ptr;

        private static void* EcsMegaBytesPerSecond_Ptr;

        private static void* EcsMegaHertz_Ptr;

        private static void* EcsMeters_Ptr;

        private static void* EcsMetersPerSecond_Ptr;

        private static void* EcsMetric_Ptr;

        private static void* EcsMetricInstance_Ptr;

        private static void* EcsMicro_Ptr;

        private static void* EcsMicroMeters_Ptr;

        private static void* EcsMicroSeconds_Ptr;

        private static void* EcsMiles_Ptr;

        private static void* EcsMilesPerHour_Ptr;

        private static void* EcsMilli_Ptr;

        private static void* EcsMilliMeters_Ptr;

        private static void* EcsMilliSeconds_Ptr;

        private static void* EcsMinutes_Ptr;

        private static void* EcsModule_Ptr;

        private static void* EcsMole_Ptr;

        private static void* EcsMonitor_Ptr;

        private static void* EcsName_Ptr;

        private static void* EcsNano_Ptr;

        private static void* EcsNanoMeters_Ptr;

        private static void* EcsNanoSeconds_Ptr;

        private static void* EcsNewton_Ptr;

        private static void* EcsObserver_Ptr;

        private static void* EcsOnAdd_Ptr;

        private static void* EcsOnDelete_Ptr;

        private static void* EcsOnDeleteTarget_Ptr;

        private static void* EcsOneOf_Ptr;

        private static void* EcsOnLoad_Ptr;

        private static void* EcsOnRemove_Ptr;

        private static void* EcsOnSet_Ptr;

        private static void* EcsOnStart_Ptr;

        private static void* EcsOnStore_Ptr;

        private static void* EcsOnTableCreate_Ptr;

        private static void* EcsOnTableDelete_Ptr;

        private static void* EcsOnTableEmpty_Ptr;

        private static void* EcsOnTableFill_Ptr;

        private static void* EcsOnUpdate_Ptr;

        private static void* EcsOnValidate_Ptr;

        private static void* EcsPanic_Ptr;

        private static void* EcsPascal_Ptr;

        private static void* EcsPebi_Ptr;

        private static void* EcsPercentage_Ptr;

        private static void* EcsPeriod1d_Ptr;

        private static void* EcsPeriod1h_Ptr;

        private static void* EcsPeriod1m_Ptr;

        private static void* EcsPeriod1s_Ptr;

        private static void* EcsPeriod1w_Ptr;

        private static void* EcsPeta_Ptr;

        private static void* EcsPhase_Ptr;

        private static void* EcsPico_Ptr;

        private static void* EcsPicoMeters_Ptr;

        private static void* EcsPicoSeconds_Ptr;

        private static void* EcsPixels_Ptr;

        private static void* EcsPostFrame_Ptr;

        private static void* EcsPostLoad_Ptr;

        private static void* EcsPostUpdate_Ptr;

        private static void* EcsPredEq_Ptr;

        private static void* EcsPredLookup_Ptr;

        private static void* EcsPredMatch_Ptr;

        private static void* EcsPrefab_Ptr;

        private static void* EcsPreFrame_Ptr;

        private static void* EcsPressure_Ptr;

        private static void* EcsPreStore_Ptr;

        private static void* EcsPreUpdate_Ptr;

        private static void* EcsPrivate_Ptr;

        private static void* EcsQuantity_Ptr;

        private static void* EcsQuery_Ptr;

        private static void* EcsRadians_Ptr;

        private static void* EcsReflexive_Ptr;

        private static void* EcsRemove_Ptr;

        private static void* EcsScopeClose_Ptr;

        private static void* EcsScopeOpen_Ptr;

        private static void* EcsSeconds_Ptr;

        private static void* EcsSlotOf_Ptr;

        private static void* EcsSpeed_Ptr;

        private static void* EcsSymbol_Ptr;

        private static void* EcsSymmetric_Ptr;

        private static void* EcsSystem_Ptr;

        private static void* EcsTag_Ptr;

        private static void* EcsTebi_Ptr;

        private static void* EcsTemperature_Ptr;

        private static void* EcsTera_Ptr;

        private static void* EcsThis_Ptr;

        private static void* EcsTime_Ptr;

        private static void* EcsTransitive_Ptr;

        private static void* EcsTraversable_Ptr;

        private static void* EcsUnion_Ptr;

        private static void* EcsUnitPrefixes_Ptr;

        private static void* EcsUnSet_Ptr;

        private static void* EcsUri_Ptr;

        private static void* EcsUriFile_Ptr;

        private static void* EcsUriHyperlink_Ptr;

        private static void* EcsUriImage_Ptr;

        private static void* EcsVariable_Ptr;

        private static void* EcsWildcard_Ptr;

        private static void* EcsWith_Ptr;

        private static void* EcsWorld_Ptr;

        private static void* EcsYobi_Ptr;

        private static void* EcsYocto_Ptr;

        private static void* EcsYotta_Ptr;

        private static void* EcsZebi_Ptr;

        private static void* EcsZepto_Ptr;

        private static void* EcsZetta_Ptr;

        private static void* FLECS_IDecs_bool_tID__Ptr;

        private static void* FLECS_IDecs_byte_tID__Ptr;

        private static void* FLECS_IDecs_char_tID__Ptr;

        private static void* FLECS_IDecs_entity_tID__Ptr;

        private static void* FLECS_IDecs_f32_tID__Ptr;

        private static void* FLECS_IDecs_f64_tID__Ptr;

        private static void* FLECS_IDecs_i16_tID__Ptr;

        private static void* FLECS_IDecs_i32_tID__Ptr;

        private static void* FLECS_IDecs_i64_tID__Ptr;

        private static void* FLECS_IDecs_i8_tID__Ptr;

        private static void* FLECS_IDecs_iptr_tID__Ptr;

        private static void* FLECS_IDecs_string_tID__Ptr;

        private static void* FLECS_IDecs_u16_tID__Ptr;

        private static void* FLECS_IDecs_u32_tID__Ptr;

        private static void* FLECS_IDecs_u64_tID__Ptr;

        private static void* FLECS_IDecs_u8_tID__Ptr;

        private static void* FLECS_IDecs_uptr_tID__Ptr;

        private static void* FLECS_IDEcsAccelerationID__Ptr;

        private static void* FLECS_IDEcsAlertCriticalID__Ptr;

        private static void* FLECS_IDEcsAlertErrorID__Ptr;

        private static void* FLECS_IDEcsAlertID__Ptr;

        private static void* FLECS_IDEcsAlertInfoID__Ptr;

        private static void* FLECS_IDEcsAlertInstanceID__Ptr;

        private static void* FLECS_IDEcsAlertsActiveID__Ptr;

        private static void* FLECS_IDEcsAlertTimeoutID__Ptr;

        private static void* FLECS_IDEcsAlertWarningID__Ptr;

        private static void* FLECS_IDEcsAmountID__Ptr;

        private static void* FLECS_IDEcsAmpereID__Ptr;

        private static void* FLECS_IDEcsAngleID__Ptr;

        private static void* FLECS_IDEcsArrayID__Ptr;

        private static void* FLECS_IDEcsAttoID__Ptr;

        private static void* FLECS_IDEcsBarID__Ptr;

        private static void* FLECS_IDEcsBelID__Ptr;

        private static void* FLECS_IDEcsBitmaskID__Ptr;

        private static void* FLECS_IDEcsBitsID__Ptr;

        private static void* FLECS_IDEcsBitsPerSecondID__Ptr;

        private static void* FLECS_IDEcsBytesID__Ptr;

        private static void* FLECS_IDEcsBytesPerSecondID__Ptr;

        private static void* FLECS_IDEcsCandelaID__Ptr;

        private static void* FLECS_IDEcsCelsiusID__Ptr;

        private static void* FLECS_IDEcsCentiID__Ptr;

        private static void* FLECS_IDEcsCentiMetersID__Ptr;

        private static void* FLECS_IDEcsComponentID__Ptr;

        private static void* FLECS_IDEcsCounterID__Ptr;

        private static void* FLECS_IDEcsCounterIdID__Ptr;

        private static void* FLECS_IDEcsCounterIncrementID__Ptr;

        private static void* FLECS_IDEcsDataID__Ptr;

        private static void* FLECS_IDEcsDataRateID__Ptr;

        private static void* FLECS_IDEcsDateID__Ptr;

        private static void* FLECS_IDEcsDaysID__Ptr;

        private static void* FLECS_IDEcsDecaID__Ptr;

        private static void* FLECS_IDEcsDeciBelID__Ptr;

        private static void* FLECS_IDEcsDeciID__Ptr;

        private static void* FLECS_IDEcsDegreesID__Ptr;

        private static void* FLECS_IDEcsDocDescriptionID__Ptr;

        private static void* FLECS_IDEcsDurationID__Ptr;

        private static void* FLECS_IDEcsElectricCurrentID__Ptr;

        private static void* FLECS_IDEcsEnumID__Ptr;

        private static void* FLECS_IDEcsExaID__Ptr;

        private static void* FLECS_IDEcsExbiID__Ptr;

        private static void* FLECS_IDEcsFahrenheitID__Ptr;

        private static void* FLECS_IDEcsFemtoID__Ptr;

        private static void* FLECS_IDEcsForceID__Ptr;

        private static void* FLECS_IDEcsFrequencyID__Ptr;

        private static void* FLECS_IDEcsGaugeID__Ptr;

        private static void* FLECS_IDEcsGibiBytesID__Ptr;

        private static void* FLECS_IDEcsGibiID__Ptr;

        private static void* FLECS_IDEcsGigaBitsID__Ptr;

        private static void* FLECS_IDEcsGigaBitsPerSecondID__Ptr;

        private static void* FLECS_IDEcsGigaBytesID__Ptr;

        private static void* FLECS_IDEcsGigaBytesPerSecondID__Ptr;

        private static void* FLECS_IDEcsGigaHertzID__Ptr;

        private static void* FLECS_IDEcsGigaID__Ptr;

        private static void* FLECS_IDEcsGramsID__Ptr;

        private static void* FLECS_IDEcsHectoID__Ptr;

        private static void* FLECS_IDEcsHertzID__Ptr;

        private static void* FLECS_IDEcsHoursID__Ptr;

        private static void* FLECS_IDEcsIdentifierID__Ptr;

        private static void* FLECS_IDEcsIterableID__Ptr;

        private static void* FLECS_IDEcsKelvinID__Ptr;

        private static void* FLECS_IDEcsKibiBytesID__Ptr;

        private static void* FLECS_IDEcsKibiID__Ptr;

        private static void* FLECS_IDEcsKiloBitsID__Ptr;

        private static void* FLECS_IDEcsKiloBitsPerSecondID__Ptr;

        private static void* FLECS_IDEcsKiloBytesID__Ptr;

        private static void* FLECS_IDEcsKiloBytesPerSecondID__Ptr;

        private static void* FLECS_IDEcsKiloGramsID__Ptr;

        private static void* FLECS_IDEcsKiloHertzID__Ptr;

        private static void* FLECS_IDEcsKiloID__Ptr;

        private static void* FLECS_IDEcsKiloMetersID__Ptr;

        private static void* FLECS_IDEcsKiloMetersPerHourID__Ptr;

        private static void* FLECS_IDEcsKiloMetersPerSecondID__Ptr;

        private static void* FLECS_IDEcsLengthID__Ptr;

        private static void* FLECS_IDEcsLuminousIntensityID__Ptr;

        private static void* FLECS_IDEcsMassID__Ptr;

        private static void* FLECS_IDEcsMebiBytesID__Ptr;

        private static void* FLECS_IDEcsMebiID__Ptr;

        private static void* FLECS_IDEcsMegaBitsID__Ptr;

        private static void* FLECS_IDEcsMegaBitsPerSecondID__Ptr;

        private static void* FLECS_IDEcsMegaBytesID__Ptr;

        private static void* FLECS_IDEcsMegaBytesPerSecondID__Ptr;

        private static void* FLECS_IDEcsMegaHertzID__Ptr;

        private static void* FLECS_IDEcsMegaID__Ptr;

        private static void* FLECS_IDEcsMemberID__Ptr;

        private static void* FLECS_IDEcsMemberRangesID__Ptr;

        private static void* FLECS_IDEcsMetaTypeID__Ptr;

        private static void* FLECS_IDEcsMetaTypeSerializedID__Ptr;

        private static void* FLECS_IDEcsMetersID__Ptr;

        private static void* FLECS_IDEcsMetersPerSecondID__Ptr;

        private static void* FLECS_IDEcsMetricID__Ptr;

        private static void* FLECS_IDEcsMetricInstanceID__Ptr;

        private static void* FLECS_IDEcsMetricSourceID__Ptr;

        private static void* FLECS_IDEcsMetricValueID__Ptr;

        private static void* FLECS_IDEcsMicroID__Ptr;

        private static void* FLECS_IDEcsMicroMetersID__Ptr;

        private static void* FLECS_IDEcsMicroSecondsID__Ptr;

        private static void* FLECS_IDEcsMilesID__Ptr;

        private static void* FLECS_IDEcsMilesPerHourID__Ptr;

        private static void* FLECS_IDEcsMilliID__Ptr;

        private static void* FLECS_IDEcsMilliMetersID__Ptr;

        private static void* FLECS_IDEcsMilliSecondsID__Ptr;

        private static void* FLECS_IDEcsMinutesID__Ptr;

        private static void* FLECS_IDEcsMoleID__Ptr;

        private static void* FLECS_IDEcsNanoID__Ptr;

        private static void* FLECS_IDEcsNanoMetersID__Ptr;

        private static void* FLECS_IDEcsNanoSecondsID__Ptr;

        private static void* FLECS_IDEcsNewtonID__Ptr;

        private static void* FLECS_IDEcsOpaqueID__Ptr;

        private static void* FLECS_IDEcsPascalID__Ptr;

        private static void* FLECS_IDEcsPebiID__Ptr;

        private static void* FLECS_IDEcsPercentageID__Ptr;

        private static void* FLECS_IDEcsPetaID__Ptr;

        private static void* FLECS_IDEcsPicoID__Ptr;

        private static void* FLECS_IDEcsPicoMetersID__Ptr;

        private static void* FLECS_IDEcsPicoSecondsID__Ptr;

        private static void* FLECS_IDEcsPipelineID__Ptr;

        private static void* FLECS_IDEcsPipelineQueryID__Ptr;

        private static void* FLECS_IDEcsPipelineStatsID__Ptr;

        private static void* FLECS_IDEcsPixelsID__Ptr;

        private static void* FLECS_IDEcsPolyID__Ptr;

        private static void* FLECS_IDEcsPressureID__Ptr;

        private static void* FLECS_IDEcsPrimitiveID__Ptr;

        private static void* FLECS_IDEcsRadiansID__Ptr;

        private static void* FLECS_IDEcsRateFilterID__Ptr;

        private static void* FLECS_IDEcsRestID__Ptr;

        private static void* FLECS_IDEcsScriptID__Ptr;

        private static void* FLECS_IDEcsSecondsID__Ptr;

        private static void* FLECS_IDEcsSpeedID__Ptr;

        private static void* FLECS_IDEcsStructID__Ptr;

        private static void* FLECS_IDEcsTargetID__Ptr;

        private static void* FLECS_IDEcsTebiID__Ptr;

        private static void* FLECS_IDEcsTemperatureID__Ptr;

        private static void* FLECS_IDEcsTeraID__Ptr;

        private static void* FLECS_IDEcsTickSourceID__Ptr;

        private static void* FLECS_IDEcsTimeID__Ptr;

        private static void* FLECS_IDEcsTimerID__Ptr;

        private static void* FLECS_IDEcsUnitID__Ptr;

        private static void* FLECS_IDEcsUnitPrefixesID__Ptr;

        private static void* FLECS_IDEcsUnitPrefixID__Ptr;

        private static void* FLECS_IDEcsUriFileID__Ptr;

        private static void* FLECS_IDEcsUriHyperlinkID__Ptr;

        private static void* FLECS_IDEcsUriID__Ptr;

        private static void* FLECS_IDEcsUriImageID__Ptr;

        private static void* FLECS_IDEcsVectorID__Ptr;

        private static void* FLECS_IDEcsWorldStatsID__Ptr;

        private static void* FLECS_IDEcsWorldSummaryID__Ptr;

        private static void* FLECS_IDEcsYobiID__Ptr;

        private static void* FLECS_IDEcsYoctoID__Ptr;

        private static void* FLECS_IDEcsYottaID__Ptr;

        private static void* FLECS_IDEcsZebiID__Ptr;

        private static void* FLECS_IDEcsZeptoID__Ptr;

        private static void* FLECS_IDEcsZettaID__Ptr;

        private static void* FLECS_IDFlecsAlertsID__Ptr;

        private static void* FLECS_IDFlecsMetricsID__Ptr;

        private static void* FLECS_IDFlecsMonitorID__Ptr;

        public static ref ulong ECS_AND
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ECS_AND_Ptr != null)
                    return ref *(ulong*)ECS_AND_Ptr;
                BindgenInternal.LoadDllSymbol("ECS_AND", out ECS_AND_Ptr);
                return ref *(ulong*)ECS_AND_Ptr;
            }
        }

        public static ref long ecs_block_allocator_alloc_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_block_allocator_alloc_count_Ptr != null)
                    return ref *(long*)ecs_block_allocator_alloc_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_block_allocator_alloc_count", out ecs_block_allocator_alloc_count_Ptr);
                return ref *(long*)ecs_block_allocator_alloc_count_Ptr;
            }
        }

        public static ref long ecs_block_allocator_free_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_block_allocator_free_count_Ptr != null)
                    return ref *(long*)ecs_block_allocator_free_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_block_allocator_free_count", out ecs_block_allocator_free_count_Ptr);
                return ref *(long*)ecs_block_allocator_free_count_Ptr;
            }
        }

        public static ref ecs_filter_t ECS_FILTER_INIT
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ECS_FILTER_INIT_Ptr != null)
                    return ref *(ecs_filter_t*)ECS_FILTER_INIT_Ptr;
                BindgenInternal.LoadDllSymbol("ECS_FILTER_INIT", out ECS_FILTER_INIT_Ptr);
                return ref *(ecs_filter_t*)ECS_FILTER_INIT_Ptr;
            }
        }

        public static ref long ecs_http_busy_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_busy_count_Ptr != null)
                    return ref *(long*)ecs_http_busy_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_busy_count", out ecs_http_busy_count_Ptr);
                return ref *(long*)ecs_http_busy_count_Ptr;
            }
        }

        public static ref long ecs_http_request_handled_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_request_handled_error_count_Ptr != null)
                    return ref *(long*)ecs_http_request_handled_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_request_handled_error_count", out ecs_http_request_handled_error_count_Ptr);
                return ref *(long*)ecs_http_request_handled_error_count_Ptr;
            }
        }

        public static ref long ecs_http_request_handled_ok_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_request_handled_ok_count_Ptr != null)
                    return ref *(long*)ecs_http_request_handled_ok_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_request_handled_ok_count", out ecs_http_request_handled_ok_count_Ptr);
                return ref *(long*)ecs_http_request_handled_ok_count_Ptr;
            }
        }

        public static ref long ecs_http_request_invalid_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_request_invalid_count_Ptr != null)
                    return ref *(long*)ecs_http_request_invalid_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_request_invalid_count", out ecs_http_request_invalid_count_Ptr);
                return ref *(long*)ecs_http_request_invalid_count_Ptr;
            }
        }

        public static ref long ecs_http_request_not_handled_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_request_not_handled_count_Ptr != null)
                    return ref *(long*)ecs_http_request_not_handled_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_request_not_handled_count", out ecs_http_request_not_handled_count_Ptr);
                return ref *(long*)ecs_http_request_not_handled_count_Ptr;
            }
        }

        public static ref long ecs_http_request_preflight_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_request_preflight_count_Ptr != null)
                    return ref *(long*)ecs_http_request_preflight_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_request_preflight_count", out ecs_http_request_preflight_count_Ptr);
                return ref *(long*)ecs_http_request_preflight_count_Ptr;
            }
        }

        public static ref long ecs_http_request_received_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_request_received_count_Ptr != null)
                    return ref *(long*)ecs_http_request_received_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_request_received_count", out ecs_http_request_received_count_Ptr);
                return ref *(long*)ecs_http_request_received_count_Ptr;
            }
        }

        public static ref long ecs_http_send_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_send_error_count_Ptr != null)
                    return ref *(long*)ecs_http_send_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_send_error_count", out ecs_http_send_error_count_Ptr);
                return ref *(long*)ecs_http_send_error_count_Ptr;
            }
        }

        public static ref long ecs_http_send_ok_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_http_send_ok_count_Ptr != null)
                    return ref *(long*)ecs_http_send_ok_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_http_send_ok_count", out ecs_http_send_ok_count_Ptr);
                return ref *(long*)ecs_http_send_ok_count_Ptr;
            }
        }

        public static ref ecs_os_api_t ecs_os_api
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_os_api_Ptr != null)
                    return ref *(ecs_os_api_t*)ecs_os_api_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_os_api", out ecs_os_api_Ptr);
                return ref *(ecs_os_api_t*)ecs_os_api_Ptr;
            }
        }

        public static ref long ecs_os_api_calloc_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_os_api_calloc_count_Ptr != null)
                    return ref *(long*)ecs_os_api_calloc_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_os_api_calloc_count", out ecs_os_api_calloc_count_Ptr);
                return ref *(long*)ecs_os_api_calloc_count_Ptr;
            }
        }

        public static ref long ecs_os_api_free_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_os_api_free_count_Ptr != null)
                    return ref *(long*)ecs_os_api_free_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_os_api_free_count", out ecs_os_api_free_count_Ptr);
                return ref *(long*)ecs_os_api_free_count_Ptr;
            }
        }

        public static ref long ecs_os_api_malloc_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_os_api_malloc_count_Ptr != null)
                    return ref *(long*)ecs_os_api_malloc_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_os_api_malloc_count", out ecs_os_api_malloc_count_Ptr);
                return ref *(long*)ecs_os_api_malloc_count_Ptr;
            }
        }

        public static ref long ecs_os_api_realloc_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_os_api_realloc_count_Ptr != null)
                    return ref *(long*)ecs_os_api_realloc_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_os_api_realloc_count", out ecs_os_api_realloc_count_Ptr);
                return ref *(long*)ecs_os_api_realloc_count_Ptr;
            }
        }

        public static ref ulong ECS_OVERRIDE
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ECS_OVERRIDE_Ptr != null)
                    return ref *(ulong*)ECS_OVERRIDE_Ptr;
                BindgenInternal.LoadDllSymbol("ECS_OVERRIDE", out ECS_OVERRIDE_Ptr);
                return ref *(ulong*)ECS_OVERRIDE_Ptr;
            }
        }

        public static ref ulong ECS_PAIR
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ECS_PAIR_Ptr != null)
                    return ref *(ulong*)ECS_PAIR_Ptr;
                BindgenInternal.LoadDllSymbol("ECS_PAIR", out ECS_PAIR_Ptr);
                return ref *(ulong*)ECS_PAIR_Ptr;
            }
        }

        public static ref long ecs_rest_delete_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_delete_count_Ptr != null)
                    return ref *(long*)ecs_rest_delete_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_delete_count", out ecs_rest_delete_count_Ptr);
                return ref *(long*)ecs_rest_delete_count_Ptr;
            }
        }

        public static ref long ecs_rest_delete_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_delete_error_count_Ptr != null)
                    return ref *(long*)ecs_rest_delete_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_delete_error_count", out ecs_rest_delete_error_count_Ptr);
                return ref *(long*)ecs_rest_delete_error_count_Ptr;
            }
        }

        public static ref long ecs_rest_enable_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_enable_count_Ptr != null)
                    return ref *(long*)ecs_rest_enable_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_enable_count", out ecs_rest_enable_count_Ptr);
                return ref *(long*)ecs_rest_enable_count_Ptr;
            }
        }

        public static ref long ecs_rest_enable_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_enable_error_count_Ptr != null)
                    return ref *(long*)ecs_rest_enable_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_enable_error_count", out ecs_rest_enable_error_count_Ptr);
                return ref *(long*)ecs_rest_enable_error_count_Ptr;
            }
        }

        public static ref long ecs_rest_entity_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_entity_count_Ptr != null)
                    return ref *(long*)ecs_rest_entity_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_entity_count", out ecs_rest_entity_count_Ptr);
                return ref *(long*)ecs_rest_entity_count_Ptr;
            }
        }

        public static ref long ecs_rest_entity_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_entity_error_count_Ptr != null)
                    return ref *(long*)ecs_rest_entity_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_entity_error_count", out ecs_rest_entity_error_count_Ptr);
                return ref *(long*)ecs_rest_entity_error_count_Ptr;
            }
        }

        public static ref long ecs_rest_pipeline_stats_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_pipeline_stats_count_Ptr != null)
                    return ref *(long*)ecs_rest_pipeline_stats_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_pipeline_stats_count", out ecs_rest_pipeline_stats_count_Ptr);
                return ref *(long*)ecs_rest_pipeline_stats_count_Ptr;
            }
        }

        public static ref long ecs_rest_query_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_query_count_Ptr != null)
                    return ref *(long*)ecs_rest_query_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_query_count", out ecs_rest_query_count_Ptr);
                return ref *(long*)ecs_rest_query_count_Ptr;
            }
        }

        public static ref long ecs_rest_query_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_query_error_count_Ptr != null)
                    return ref *(long*)ecs_rest_query_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_query_error_count", out ecs_rest_query_error_count_Ptr);
                return ref *(long*)ecs_rest_query_error_count_Ptr;
            }
        }

        public static ref long ecs_rest_query_name_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_query_name_count_Ptr != null)
                    return ref *(long*)ecs_rest_query_name_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_query_name_count", out ecs_rest_query_name_count_Ptr);
                return ref *(long*)ecs_rest_query_name_count_Ptr;
            }
        }

        public static ref long ecs_rest_query_name_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_query_name_error_count_Ptr != null)
                    return ref *(long*)ecs_rest_query_name_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_query_name_error_count", out ecs_rest_query_name_error_count_Ptr);
                return ref *(long*)ecs_rest_query_name_error_count_Ptr;
            }
        }

        public static ref long ecs_rest_query_name_from_cache_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_query_name_from_cache_count_Ptr != null)
                    return ref *(long*)ecs_rest_query_name_from_cache_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_query_name_from_cache_count", out ecs_rest_query_name_from_cache_count_Ptr);
                return ref *(long*)ecs_rest_query_name_from_cache_count_Ptr;
            }
        }

        public static ref long ecs_rest_request_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_request_count_Ptr != null)
                    return ref *(long*)ecs_rest_request_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_request_count", out ecs_rest_request_count_Ptr);
                return ref *(long*)ecs_rest_request_count_Ptr;
            }
        }

        public static ref long ecs_rest_stats_error_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_stats_error_count_Ptr != null)
                    return ref *(long*)ecs_rest_stats_error_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_stats_error_count", out ecs_rest_stats_error_count_Ptr);
                return ref *(long*)ecs_rest_stats_error_count_Ptr;
            }
        }

        public static ref long ecs_rest_world_stats_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_rest_world_stats_count_Ptr != null)
                    return ref *(long*)ecs_rest_world_stats_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_rest_world_stats_count", out ecs_rest_world_stats_count_Ptr);
                return ref *(long*)ecs_rest_world_stats_count_Ptr;
            }
        }

        public static ref long ecs_stack_allocator_alloc_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_stack_allocator_alloc_count_Ptr != null)
                    return ref *(long*)ecs_stack_allocator_alloc_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_stack_allocator_alloc_count", out ecs_stack_allocator_alloc_count_Ptr);
                return ref *(long*)ecs_stack_allocator_alloc_count_Ptr;
            }
        }

        public static ref long ecs_stack_allocator_free_count
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ecs_stack_allocator_free_count_Ptr != null)
                    return ref *(long*)ecs_stack_allocator_free_count_Ptr;
                BindgenInternal.LoadDllSymbol("ecs_stack_allocator_free_count", out ecs_stack_allocator_free_count_Ptr);
                return ref *(long*)ecs_stack_allocator_free_count_Ptr;
            }
        }

        public static ref ulong ECS_TOGGLE
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (ECS_TOGGLE_Ptr != null)
                    return ref *(ulong*)ECS_TOGGLE_Ptr;
                BindgenInternal.LoadDllSymbol("ECS_TOGGLE", out ECS_TOGGLE_Ptr);
                return ref *(ulong*)ECS_TOGGLE_Ptr;
            }
        }

        public static ref ulong EcsAcceleration
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAcceleration_Ptr != null)
                    return ref *(ulong*)EcsAcceleration_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAcceleration", out EcsAcceleration_Ptr);
                return ref *(ulong*)EcsAcceleration_Ptr;
            }
        }

        public static ref ulong EcsAcyclic
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAcyclic_Ptr != null)
                    return ref *(ulong*)EcsAcyclic_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAcyclic", out EcsAcyclic_Ptr);
                return ref *(ulong*)EcsAcyclic_Ptr;
            }
        }

        public static ref ulong EcsAlertCritical
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAlertCritical_Ptr != null)
                    return ref *(ulong*)EcsAlertCritical_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAlertCritical", out EcsAlertCritical_Ptr);
                return ref *(ulong*)EcsAlertCritical_Ptr;
            }
        }

        public static ref ulong EcsAlertError
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAlertError_Ptr != null)
                    return ref *(ulong*)EcsAlertError_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAlertError", out EcsAlertError_Ptr);
                return ref *(ulong*)EcsAlertError_Ptr;
            }
        }

        public static ref ulong EcsAlertInfo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAlertInfo_Ptr != null)
                    return ref *(ulong*)EcsAlertInfo_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAlertInfo", out EcsAlertInfo_Ptr);
                return ref *(ulong*)EcsAlertInfo_Ptr;
            }
        }

        public static ref ulong EcsAlertWarning
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAlertWarning_Ptr != null)
                    return ref *(ulong*)EcsAlertWarning_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAlertWarning", out EcsAlertWarning_Ptr);
                return ref *(ulong*)EcsAlertWarning_Ptr;
            }
        }

        public static ref ulong EcsAlias
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAlias_Ptr != null)
                    return ref *(ulong*)EcsAlias_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAlias", out EcsAlias_Ptr);
                return ref *(ulong*)EcsAlias_Ptr;
            }
        }

        public static ref ulong EcsAlwaysOverride
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAlwaysOverride_Ptr != null)
                    return ref *(ulong*)EcsAlwaysOverride_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAlwaysOverride", out EcsAlwaysOverride_Ptr);
                return ref *(ulong*)EcsAlwaysOverride_Ptr;
            }
        }

        public static ref ulong EcsAmount
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAmount_Ptr != null)
                    return ref *(ulong*)EcsAmount_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAmount", out EcsAmount_Ptr);
                return ref *(ulong*)EcsAmount_Ptr;
            }
        }

        public static ref ulong EcsAmpere
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAmpere_Ptr != null)
                    return ref *(ulong*)EcsAmpere_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAmpere", out EcsAmpere_Ptr);
                return ref *(ulong*)EcsAmpere_Ptr;
            }
        }

        public static ref ulong EcsAngle
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAngle_Ptr != null)
                    return ref *(ulong*)EcsAngle_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAngle", out EcsAngle_Ptr);
                return ref *(ulong*)EcsAngle_Ptr;
            }
        }

        public static ref ulong EcsAny
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAny_Ptr != null)
                    return ref *(ulong*)EcsAny_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAny", out EcsAny_Ptr);
                return ref *(ulong*)EcsAny_Ptr;
            }
        }

        public static ref ulong EcsAtto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsAtto_Ptr != null)
                    return ref *(ulong*)EcsAtto_Ptr;
                BindgenInternal.LoadDllSymbol("EcsAtto", out EcsAtto_Ptr);
                return ref *(ulong*)EcsAtto_Ptr;
            }
        }

        public static ref ulong EcsBar
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsBar_Ptr != null)
                    return ref *(ulong*)EcsBar_Ptr;
                BindgenInternal.LoadDllSymbol("EcsBar", out EcsBar_Ptr);
                return ref *(ulong*)EcsBar_Ptr;
            }
        }

        public static ref ulong EcsBel
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsBel_Ptr != null)
                    return ref *(ulong*)EcsBel_Ptr;
                BindgenInternal.LoadDllSymbol("EcsBel", out EcsBel_Ptr);
                return ref *(ulong*)EcsBel_Ptr;
            }
        }

        public static ref ulong EcsBits
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsBits_Ptr != null)
                    return ref *(ulong*)EcsBits_Ptr;
                BindgenInternal.LoadDllSymbol("EcsBits", out EcsBits_Ptr);
                return ref *(ulong*)EcsBits_Ptr;
            }
        }

        public static ref ulong EcsBitsPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsBitsPerSecond_Ptr != null)
                    return ref *(ulong*)EcsBitsPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsBitsPerSecond", out EcsBitsPerSecond_Ptr);
                return ref *(ulong*)EcsBitsPerSecond_Ptr;
            }
        }

        public static ref ulong EcsBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsBytes_Ptr != null)
                    return ref *(ulong*)EcsBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsBytes", out EcsBytes_Ptr);
                return ref *(ulong*)EcsBytes_Ptr;
            }
        }

        public static ref ulong EcsBytesPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsBytesPerSecond_Ptr != null)
                    return ref *(ulong*)EcsBytesPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsBytesPerSecond", out EcsBytesPerSecond_Ptr);
                return ref *(ulong*)EcsBytesPerSecond_Ptr;
            }
        }

        public static ref ulong EcsCandela
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCandela_Ptr != null)
                    return ref *(ulong*)EcsCandela_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCandela", out EcsCandela_Ptr);
                return ref *(ulong*)EcsCandela_Ptr;
            }
        }

        public static ref ulong EcsCelsius
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCelsius_Ptr != null)
                    return ref *(ulong*)EcsCelsius_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCelsius", out EcsCelsius_Ptr);
                return ref *(ulong*)EcsCelsius_Ptr;
            }
        }

        public static ref ulong EcsCenti
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCenti_Ptr != null)
                    return ref *(ulong*)EcsCenti_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCenti", out EcsCenti_Ptr);
                return ref *(ulong*)EcsCenti_Ptr;
            }
        }

        public static ref ulong EcsCentiMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCentiMeters_Ptr != null)
                    return ref *(ulong*)EcsCentiMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCentiMeters", out EcsCentiMeters_Ptr);
                return ref *(ulong*)EcsCentiMeters_Ptr;
            }
        }

        public static ref ulong EcsChildOf
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsChildOf_Ptr != null)
                    return ref *(ulong*)EcsChildOf_Ptr;
                BindgenInternal.LoadDllSymbol("EcsChildOf", out EcsChildOf_Ptr);
                return ref *(ulong*)EcsChildOf_Ptr;
            }
        }

        public static ref ulong EcsConstant
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsConstant_Ptr != null)
                    return ref *(ulong*)EcsConstant_Ptr;
                BindgenInternal.LoadDllSymbol("EcsConstant", out EcsConstant_Ptr);
                return ref *(ulong*)EcsConstant_Ptr;
            }
        }

        public static ref ulong EcsCounter
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCounter_Ptr != null)
                    return ref *(ulong*)EcsCounter_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCounter", out EcsCounter_Ptr);
                return ref *(ulong*)EcsCounter_Ptr;
            }
        }

        public static ref ulong EcsCounterId
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCounterId_Ptr != null)
                    return ref *(ulong*)EcsCounterId_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCounterId", out EcsCounterId_Ptr);
                return ref *(ulong*)EcsCounterId_Ptr;
            }
        }

        public static ref ulong EcsCounterIncrement
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsCounterIncrement_Ptr != null)
                    return ref *(ulong*)EcsCounterIncrement_Ptr;
                BindgenInternal.LoadDllSymbol("EcsCounterIncrement", out EcsCounterIncrement_Ptr);
                return ref *(ulong*)EcsCounterIncrement_Ptr;
            }
        }

        public static ref ulong EcsData
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsData_Ptr != null)
                    return ref *(ulong*)EcsData_Ptr;
                BindgenInternal.LoadDllSymbol("EcsData", out EcsData_Ptr);
                return ref *(ulong*)EcsData_Ptr;
            }
        }

        public static ref ulong EcsDataRate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDataRate_Ptr != null)
                    return ref *(ulong*)EcsDataRate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDataRate", out EcsDataRate_Ptr);
                return ref *(ulong*)EcsDataRate_Ptr;
            }
        }

        public static ref ulong EcsDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDate_Ptr != null)
                    return ref *(ulong*)EcsDate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDate", out EcsDate_Ptr);
                return ref *(ulong*)EcsDate_Ptr;
            }
        }

        public static ref ulong EcsDays
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDays_Ptr != null)
                    return ref *(ulong*)EcsDays_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDays", out EcsDays_Ptr);
                return ref *(ulong*)EcsDays_Ptr;
            }
        }

        public static ref ulong EcsDeca
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDeca_Ptr != null)
                    return ref *(ulong*)EcsDeca_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDeca", out EcsDeca_Ptr);
                return ref *(ulong*)EcsDeca_Ptr;
            }
        }

        public static ref ulong EcsDeci
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDeci_Ptr != null)
                    return ref *(ulong*)EcsDeci_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDeci", out EcsDeci_Ptr);
                return ref *(ulong*)EcsDeci_Ptr;
            }
        }

        public static ref ulong EcsDeciBel
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDeciBel_Ptr != null)
                    return ref *(ulong*)EcsDeciBel_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDeciBel", out EcsDeciBel_Ptr);
                return ref *(ulong*)EcsDeciBel_Ptr;
            }
        }

        public static ref ulong EcsDefaultChildComponent
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDefaultChildComponent_Ptr != null)
                    return ref *(ulong*)EcsDefaultChildComponent_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDefaultChildComponent", out EcsDefaultChildComponent_Ptr);
                return ref *(ulong*)EcsDefaultChildComponent_Ptr;
            }
        }

        public static ref ulong EcsDegrees
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDegrees_Ptr != null)
                    return ref *(ulong*)EcsDegrees_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDegrees", out EcsDegrees_Ptr);
                return ref *(ulong*)EcsDegrees_Ptr;
            }
        }

        public static ref ulong EcsDelete
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDelete_Ptr != null)
                    return ref *(ulong*)EcsDelete_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDelete", out EcsDelete_Ptr);
                return ref *(ulong*)EcsDelete_Ptr;
            }
        }

        public static ref ulong EcsDependsOn
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDependsOn_Ptr != null)
                    return ref *(ulong*)EcsDependsOn_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDependsOn", out EcsDependsOn_Ptr);
                return ref *(ulong*)EcsDependsOn_Ptr;
            }
        }

        public static ref ulong EcsDisabled
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDisabled_Ptr != null)
                    return ref *(ulong*)EcsDisabled_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDisabled", out EcsDisabled_Ptr);
                return ref *(ulong*)EcsDisabled_Ptr;
            }
        }

        public static ref ulong EcsDocBrief
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDocBrief_Ptr != null)
                    return ref *(ulong*)EcsDocBrief_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDocBrief", out EcsDocBrief_Ptr);
                return ref *(ulong*)EcsDocBrief_Ptr;
            }
        }

        public static ref ulong EcsDocColor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDocColor_Ptr != null)
                    return ref *(ulong*)EcsDocColor_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDocColor", out EcsDocColor_Ptr);
                return ref *(ulong*)EcsDocColor_Ptr;
            }
        }

        public static ref ulong EcsDocDetail
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDocDetail_Ptr != null)
                    return ref *(ulong*)EcsDocDetail_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDocDetail", out EcsDocDetail_Ptr);
                return ref *(ulong*)EcsDocDetail_Ptr;
            }
        }

        public static ref ulong EcsDocLink
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDocLink_Ptr != null)
                    return ref *(ulong*)EcsDocLink_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDocLink", out EcsDocLink_Ptr);
                return ref *(ulong*)EcsDocLink_Ptr;
            }
        }

        public static ref ulong EcsDontInherit
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDontInherit_Ptr != null)
                    return ref *(ulong*)EcsDontInherit_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDontInherit", out EcsDontInherit_Ptr);
                return ref *(ulong*)EcsDontInherit_Ptr;
            }
        }

        public static ref ulong EcsDuration
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsDuration_Ptr != null)
                    return ref *(ulong*)EcsDuration_Ptr;
                BindgenInternal.LoadDllSymbol("EcsDuration", out EcsDuration_Ptr);
                return ref *(ulong*)EcsDuration_Ptr;
            }
        }

        public static ref ulong EcsElectricCurrent
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsElectricCurrent_Ptr != null)
                    return ref *(ulong*)EcsElectricCurrent_Ptr;
                BindgenInternal.LoadDllSymbol("EcsElectricCurrent", out EcsElectricCurrent_Ptr);
                return ref *(ulong*)EcsElectricCurrent_Ptr;
            }
        }

        public static ref ulong EcsEmpty
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsEmpty_Ptr != null)
                    return ref *(ulong*)EcsEmpty_Ptr;
                BindgenInternal.LoadDllSymbol("EcsEmpty", out EcsEmpty_Ptr);
                return ref *(ulong*)EcsEmpty_Ptr;
            }
        }

        public static ref ulong EcsExa
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsExa_Ptr != null)
                    return ref *(ulong*)EcsExa_Ptr;
                BindgenInternal.LoadDllSymbol("EcsExa", out EcsExa_Ptr);
                return ref *(ulong*)EcsExa_Ptr;
            }
        }

        public static ref ulong EcsExbi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsExbi_Ptr != null)
                    return ref *(ulong*)EcsExbi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsExbi", out EcsExbi_Ptr);
                return ref *(ulong*)EcsExbi_Ptr;
            }
        }

        public static ref ulong EcsExclusive
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsExclusive_Ptr != null)
                    return ref *(ulong*)EcsExclusive_Ptr;
                BindgenInternal.LoadDllSymbol("EcsExclusive", out EcsExclusive_Ptr);
                return ref *(ulong*)EcsExclusive_Ptr;
            }
        }

        public static ref ulong EcsFahrenheit
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFahrenheit_Ptr != null)
                    return ref *(ulong*)EcsFahrenheit_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFahrenheit", out EcsFahrenheit_Ptr);
                return ref *(ulong*)EcsFahrenheit_Ptr;
            }
        }

        public static ref ulong EcsFemto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFemto_Ptr != null)
                    return ref *(ulong*)EcsFemto_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFemto", out EcsFemto_Ptr);
                return ref *(ulong*)EcsFemto_Ptr;
            }
        }

        public static ref ulong EcsFinal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFinal_Ptr != null)
                    return ref *(ulong*)EcsFinal_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFinal", out EcsFinal_Ptr);
                return ref *(ulong*)EcsFinal_Ptr;
            }
        }

        public static ref ulong EcsFlatten
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFlatten_Ptr != null)
                    return ref *(ulong*)EcsFlatten_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFlatten", out EcsFlatten_Ptr);
                return ref *(ulong*)EcsFlatten_Ptr;
            }
        }

        public static ref ulong EcsFlecs
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFlecs_Ptr != null)
                    return ref *(ulong*)EcsFlecs_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFlecs", out EcsFlecs_Ptr);
                return ref *(ulong*)EcsFlecs_Ptr;
            }
        }

        public static ref ulong EcsFlecsCore
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFlecsCore_Ptr != null)
                    return ref *(ulong*)EcsFlecsCore_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFlecsCore", out EcsFlecsCore_Ptr);
                return ref *(ulong*)EcsFlecsCore_Ptr;
            }
        }

        public static ref ulong EcsForce
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsForce_Ptr != null)
                    return ref *(ulong*)EcsForce_Ptr;
                BindgenInternal.LoadDllSymbol("EcsForce", out EcsForce_Ptr);
                return ref *(ulong*)EcsForce_Ptr;
            }
        }

        public static ref ulong EcsFrequency
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsFrequency_Ptr != null)
                    return ref *(ulong*)EcsFrequency_Ptr;
                BindgenInternal.LoadDllSymbol("EcsFrequency", out EcsFrequency_Ptr);
                return ref *(ulong*)EcsFrequency_Ptr;
            }
        }

        public static ref ulong EcsGauge
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGauge_Ptr != null)
                    return ref *(ulong*)EcsGauge_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGauge", out EcsGauge_Ptr);
                return ref *(ulong*)EcsGauge_Ptr;
            }
        }

        public static ref ulong EcsGibi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGibi_Ptr != null)
                    return ref *(ulong*)EcsGibi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGibi", out EcsGibi_Ptr);
                return ref *(ulong*)EcsGibi_Ptr;
            }
        }

        public static ref ulong EcsGibiBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGibiBytes_Ptr != null)
                    return ref *(ulong*)EcsGibiBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGibiBytes", out EcsGibiBytes_Ptr);
                return ref *(ulong*)EcsGibiBytes_Ptr;
            }
        }

        public static ref ulong EcsGiga
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGiga_Ptr != null)
                    return ref *(ulong*)EcsGiga_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGiga", out EcsGiga_Ptr);
                return ref *(ulong*)EcsGiga_Ptr;
            }
        }

        public static ref ulong EcsGigaBits
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGigaBits_Ptr != null)
                    return ref *(ulong*)EcsGigaBits_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGigaBits", out EcsGigaBits_Ptr);
                return ref *(ulong*)EcsGigaBits_Ptr;
            }
        }

        public static ref ulong EcsGigaBitsPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGigaBitsPerSecond_Ptr != null)
                    return ref *(ulong*)EcsGigaBitsPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGigaBitsPerSecond", out EcsGigaBitsPerSecond_Ptr);
                return ref *(ulong*)EcsGigaBitsPerSecond_Ptr;
            }
        }

        public static ref ulong EcsGigaBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGigaBytes_Ptr != null)
                    return ref *(ulong*)EcsGigaBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGigaBytes", out EcsGigaBytes_Ptr);
                return ref *(ulong*)EcsGigaBytes_Ptr;
            }
        }

        public static ref ulong EcsGigaBytesPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGigaBytesPerSecond_Ptr != null)
                    return ref *(ulong*)EcsGigaBytesPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGigaBytesPerSecond", out EcsGigaBytesPerSecond_Ptr);
                return ref *(ulong*)EcsGigaBytesPerSecond_Ptr;
            }
        }

        public static ref ulong EcsGigaHertz
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGigaHertz_Ptr != null)
                    return ref *(ulong*)EcsGigaHertz_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGigaHertz", out EcsGigaHertz_Ptr);
                return ref *(ulong*)EcsGigaHertz_Ptr;
            }
        }

        public static ref ulong EcsGrams
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsGrams_Ptr != null)
                    return ref *(ulong*)EcsGrams_Ptr;
                BindgenInternal.LoadDllSymbol("EcsGrams", out EcsGrams_Ptr);
                return ref *(ulong*)EcsGrams_Ptr;
            }
        }

        public static ref ulong EcsHecto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsHecto_Ptr != null)
                    return ref *(ulong*)EcsHecto_Ptr;
                BindgenInternal.LoadDllSymbol("EcsHecto", out EcsHecto_Ptr);
                return ref *(ulong*)EcsHecto_Ptr;
            }
        }

        public static ref ulong EcsHertz
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsHertz_Ptr != null)
                    return ref *(ulong*)EcsHertz_Ptr;
                BindgenInternal.LoadDllSymbol("EcsHertz", out EcsHertz_Ptr);
                return ref *(ulong*)EcsHertz_Ptr;
            }
        }

        public static ref ulong EcsHours
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsHours_Ptr != null)
                    return ref *(ulong*)EcsHours_Ptr;
                BindgenInternal.LoadDllSymbol("EcsHours", out EcsHours_Ptr);
                return ref *(ulong*)EcsHours_Ptr;
            }
        }

        public static ref ulong EcsIsA
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsIsA_Ptr != null)
                    return ref *(ulong*)EcsIsA_Ptr;
                BindgenInternal.LoadDllSymbol("EcsIsA", out EcsIsA_Ptr);
                return ref *(ulong*)EcsIsA_Ptr;
            }
        }

        public static ref ulong EcsKelvin
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKelvin_Ptr != null)
                    return ref *(ulong*)EcsKelvin_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKelvin", out EcsKelvin_Ptr);
                return ref *(ulong*)EcsKelvin_Ptr;
            }
        }

        public static ref ulong EcsKibi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKibi_Ptr != null)
                    return ref *(ulong*)EcsKibi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKibi", out EcsKibi_Ptr);
                return ref *(ulong*)EcsKibi_Ptr;
            }
        }

        public static ref ulong EcsKibiBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKibiBytes_Ptr != null)
                    return ref *(ulong*)EcsKibiBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKibiBytes", out EcsKibiBytes_Ptr);
                return ref *(ulong*)EcsKibiBytes_Ptr;
            }
        }

        public static ref ulong EcsKilo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKilo_Ptr != null)
                    return ref *(ulong*)EcsKilo_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKilo", out EcsKilo_Ptr);
                return ref *(ulong*)EcsKilo_Ptr;
            }
        }

        public static ref ulong EcsKiloBits
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloBits_Ptr != null)
                    return ref *(ulong*)EcsKiloBits_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloBits", out EcsKiloBits_Ptr);
                return ref *(ulong*)EcsKiloBits_Ptr;
            }
        }

        public static ref ulong EcsKiloBitsPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloBitsPerSecond_Ptr != null)
                    return ref *(ulong*)EcsKiloBitsPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloBitsPerSecond", out EcsKiloBitsPerSecond_Ptr);
                return ref *(ulong*)EcsKiloBitsPerSecond_Ptr;
            }
        }

        public static ref ulong EcsKiloBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloBytes_Ptr != null)
                    return ref *(ulong*)EcsKiloBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloBytes", out EcsKiloBytes_Ptr);
                return ref *(ulong*)EcsKiloBytes_Ptr;
            }
        }

        public static ref ulong EcsKiloBytesPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloBytesPerSecond_Ptr != null)
                    return ref *(ulong*)EcsKiloBytesPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloBytesPerSecond", out EcsKiloBytesPerSecond_Ptr);
                return ref *(ulong*)EcsKiloBytesPerSecond_Ptr;
            }
        }

        public static ref ulong EcsKiloGrams
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloGrams_Ptr != null)
                    return ref *(ulong*)EcsKiloGrams_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloGrams", out EcsKiloGrams_Ptr);
                return ref *(ulong*)EcsKiloGrams_Ptr;
            }
        }

        public static ref ulong EcsKiloHertz
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloHertz_Ptr != null)
                    return ref *(ulong*)EcsKiloHertz_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloHertz", out EcsKiloHertz_Ptr);
                return ref *(ulong*)EcsKiloHertz_Ptr;
            }
        }

        public static ref ulong EcsKiloMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloMeters_Ptr != null)
                    return ref *(ulong*)EcsKiloMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloMeters", out EcsKiloMeters_Ptr);
                return ref *(ulong*)EcsKiloMeters_Ptr;
            }
        }

        public static ref ulong EcsKiloMetersPerHour
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloMetersPerHour_Ptr != null)
                    return ref *(ulong*)EcsKiloMetersPerHour_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloMetersPerHour", out EcsKiloMetersPerHour_Ptr);
                return ref *(ulong*)EcsKiloMetersPerHour_Ptr;
            }
        }

        public static ref ulong EcsKiloMetersPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsKiloMetersPerSecond_Ptr != null)
                    return ref *(ulong*)EcsKiloMetersPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsKiloMetersPerSecond", out EcsKiloMetersPerSecond_Ptr);
                return ref *(ulong*)EcsKiloMetersPerSecond_Ptr;
            }
        }

        public static ref ulong EcsLength
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsLength_Ptr != null)
                    return ref *(ulong*)EcsLength_Ptr;
                BindgenInternal.LoadDllSymbol("EcsLength", out EcsLength_Ptr);
                return ref *(ulong*)EcsLength_Ptr;
            }
        }

        public static ref ulong EcsLuminousIntensity
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsLuminousIntensity_Ptr != null)
                    return ref *(ulong*)EcsLuminousIntensity_Ptr;
                BindgenInternal.LoadDllSymbol("EcsLuminousIntensity", out EcsLuminousIntensity_Ptr);
                return ref *(ulong*)EcsLuminousIntensity_Ptr;
            }
        }

        public static ref ulong EcsMass
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMass_Ptr != null)
                    return ref *(ulong*)EcsMass_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMass", out EcsMass_Ptr);
                return ref *(ulong*)EcsMass_Ptr;
            }
        }

        public static ref ulong EcsMebi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMebi_Ptr != null)
                    return ref *(ulong*)EcsMebi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMebi", out EcsMebi_Ptr);
                return ref *(ulong*)EcsMebi_Ptr;
            }
        }

        public static ref ulong EcsMebiBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMebiBytes_Ptr != null)
                    return ref *(ulong*)EcsMebiBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMebiBytes", out EcsMebiBytes_Ptr);
                return ref *(ulong*)EcsMebiBytes_Ptr;
            }
        }

        public static ref ulong EcsMega
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMega_Ptr != null)
                    return ref *(ulong*)EcsMega_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMega", out EcsMega_Ptr);
                return ref *(ulong*)EcsMega_Ptr;
            }
        }

        public static ref ulong EcsMegaBits
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMegaBits_Ptr != null)
                    return ref *(ulong*)EcsMegaBits_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMegaBits", out EcsMegaBits_Ptr);
                return ref *(ulong*)EcsMegaBits_Ptr;
            }
        }

        public static ref ulong EcsMegaBitsPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMegaBitsPerSecond_Ptr != null)
                    return ref *(ulong*)EcsMegaBitsPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMegaBitsPerSecond", out EcsMegaBitsPerSecond_Ptr);
                return ref *(ulong*)EcsMegaBitsPerSecond_Ptr;
            }
        }

        public static ref ulong EcsMegaBytes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMegaBytes_Ptr != null)
                    return ref *(ulong*)EcsMegaBytes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMegaBytes", out EcsMegaBytes_Ptr);
                return ref *(ulong*)EcsMegaBytes_Ptr;
            }
        }

        public static ref ulong EcsMegaBytesPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMegaBytesPerSecond_Ptr != null)
                    return ref *(ulong*)EcsMegaBytesPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMegaBytesPerSecond", out EcsMegaBytesPerSecond_Ptr);
                return ref *(ulong*)EcsMegaBytesPerSecond_Ptr;
            }
        }

        public static ref ulong EcsMegaHertz
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMegaHertz_Ptr != null)
                    return ref *(ulong*)EcsMegaHertz_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMegaHertz", out EcsMegaHertz_Ptr);
                return ref *(ulong*)EcsMegaHertz_Ptr;
            }
        }

        public static ref ulong EcsMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMeters_Ptr != null)
                    return ref *(ulong*)EcsMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMeters", out EcsMeters_Ptr);
                return ref *(ulong*)EcsMeters_Ptr;
            }
        }

        public static ref ulong EcsMetersPerSecond
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMetersPerSecond_Ptr != null)
                    return ref *(ulong*)EcsMetersPerSecond_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMetersPerSecond", out EcsMetersPerSecond_Ptr);
                return ref *(ulong*)EcsMetersPerSecond_Ptr;
            }
        }

        public static ref ulong EcsMetric
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMetric_Ptr != null)
                    return ref *(ulong*)EcsMetric_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMetric", out EcsMetric_Ptr);
                return ref *(ulong*)EcsMetric_Ptr;
            }
        }

        public static ref ulong EcsMetricInstance
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMetricInstance_Ptr != null)
                    return ref *(ulong*)EcsMetricInstance_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMetricInstance", out EcsMetricInstance_Ptr);
                return ref *(ulong*)EcsMetricInstance_Ptr;
            }
        }

        public static ref ulong EcsMicro
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMicro_Ptr != null)
                    return ref *(ulong*)EcsMicro_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMicro", out EcsMicro_Ptr);
                return ref *(ulong*)EcsMicro_Ptr;
            }
        }

        public static ref ulong EcsMicroMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMicroMeters_Ptr != null)
                    return ref *(ulong*)EcsMicroMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMicroMeters", out EcsMicroMeters_Ptr);
                return ref *(ulong*)EcsMicroMeters_Ptr;
            }
        }

        public static ref ulong EcsMicroSeconds
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMicroSeconds_Ptr != null)
                    return ref *(ulong*)EcsMicroSeconds_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMicroSeconds", out EcsMicroSeconds_Ptr);
                return ref *(ulong*)EcsMicroSeconds_Ptr;
            }
        }

        public static ref ulong EcsMiles
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMiles_Ptr != null)
                    return ref *(ulong*)EcsMiles_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMiles", out EcsMiles_Ptr);
                return ref *(ulong*)EcsMiles_Ptr;
            }
        }

        public static ref ulong EcsMilesPerHour
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMilesPerHour_Ptr != null)
                    return ref *(ulong*)EcsMilesPerHour_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMilesPerHour", out EcsMilesPerHour_Ptr);
                return ref *(ulong*)EcsMilesPerHour_Ptr;
            }
        }

        public static ref ulong EcsMilli
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMilli_Ptr != null)
                    return ref *(ulong*)EcsMilli_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMilli", out EcsMilli_Ptr);
                return ref *(ulong*)EcsMilli_Ptr;
            }
        }

        public static ref ulong EcsMilliMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMilliMeters_Ptr != null)
                    return ref *(ulong*)EcsMilliMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMilliMeters", out EcsMilliMeters_Ptr);
                return ref *(ulong*)EcsMilliMeters_Ptr;
            }
        }

        public static ref ulong EcsMilliSeconds
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMilliSeconds_Ptr != null)
                    return ref *(ulong*)EcsMilliSeconds_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMilliSeconds", out EcsMilliSeconds_Ptr);
                return ref *(ulong*)EcsMilliSeconds_Ptr;
            }
        }

        public static ref ulong EcsMinutes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMinutes_Ptr != null)
                    return ref *(ulong*)EcsMinutes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMinutes", out EcsMinutes_Ptr);
                return ref *(ulong*)EcsMinutes_Ptr;
            }
        }

        public static ref ulong EcsModule
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsModule_Ptr != null)
                    return ref *(ulong*)EcsModule_Ptr;
                BindgenInternal.LoadDllSymbol("EcsModule", out EcsModule_Ptr);
                return ref *(ulong*)EcsModule_Ptr;
            }
        }

        public static ref ulong EcsMole
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMole_Ptr != null)
                    return ref *(ulong*)EcsMole_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMole", out EcsMole_Ptr);
                return ref *(ulong*)EcsMole_Ptr;
            }
        }

        public static ref ulong EcsMonitor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsMonitor_Ptr != null)
                    return ref *(ulong*)EcsMonitor_Ptr;
                BindgenInternal.LoadDllSymbol("EcsMonitor", out EcsMonitor_Ptr);
                return ref *(ulong*)EcsMonitor_Ptr;
            }
        }

        public static ref ulong EcsName
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsName_Ptr != null)
                    return ref *(ulong*)EcsName_Ptr;
                BindgenInternal.LoadDllSymbol("EcsName", out EcsName_Ptr);
                return ref *(ulong*)EcsName_Ptr;
            }
        }

        public static ref ulong EcsNano
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsNano_Ptr != null)
                    return ref *(ulong*)EcsNano_Ptr;
                BindgenInternal.LoadDllSymbol("EcsNano", out EcsNano_Ptr);
                return ref *(ulong*)EcsNano_Ptr;
            }
        }

        public static ref ulong EcsNanoMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsNanoMeters_Ptr != null)
                    return ref *(ulong*)EcsNanoMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsNanoMeters", out EcsNanoMeters_Ptr);
                return ref *(ulong*)EcsNanoMeters_Ptr;
            }
        }

        public static ref ulong EcsNanoSeconds
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsNanoSeconds_Ptr != null)
                    return ref *(ulong*)EcsNanoSeconds_Ptr;
                BindgenInternal.LoadDllSymbol("EcsNanoSeconds", out EcsNanoSeconds_Ptr);
                return ref *(ulong*)EcsNanoSeconds_Ptr;
            }
        }

        public static ref ulong EcsNewton
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsNewton_Ptr != null)
                    return ref *(ulong*)EcsNewton_Ptr;
                BindgenInternal.LoadDllSymbol("EcsNewton", out EcsNewton_Ptr);
                return ref *(ulong*)EcsNewton_Ptr;
            }
        }

        public static ref ulong EcsObserver
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsObserver_Ptr != null)
                    return ref *(ulong*)EcsObserver_Ptr;
                BindgenInternal.LoadDllSymbol("EcsObserver", out EcsObserver_Ptr);
                return ref *(ulong*)EcsObserver_Ptr;
            }
        }

        public static ref ulong EcsOnAdd
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnAdd_Ptr != null)
                    return ref *(ulong*)EcsOnAdd_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnAdd", out EcsOnAdd_Ptr);
                return ref *(ulong*)EcsOnAdd_Ptr;
            }
        }

        public static ref ulong EcsOnDelete
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnDelete_Ptr != null)
                    return ref *(ulong*)EcsOnDelete_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnDelete", out EcsOnDelete_Ptr);
                return ref *(ulong*)EcsOnDelete_Ptr;
            }
        }

        public static ref ulong EcsOnDeleteTarget
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnDeleteTarget_Ptr != null)
                    return ref *(ulong*)EcsOnDeleteTarget_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnDeleteTarget", out EcsOnDeleteTarget_Ptr);
                return ref *(ulong*)EcsOnDeleteTarget_Ptr;
            }
        }

        public static ref ulong EcsOneOf
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOneOf_Ptr != null)
                    return ref *(ulong*)EcsOneOf_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOneOf", out EcsOneOf_Ptr);
                return ref *(ulong*)EcsOneOf_Ptr;
            }
        }

        public static ref ulong EcsOnLoad
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnLoad_Ptr != null)
                    return ref *(ulong*)EcsOnLoad_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnLoad", out EcsOnLoad_Ptr);
                return ref *(ulong*)EcsOnLoad_Ptr;
            }
        }

        public static ref ulong EcsOnRemove
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnRemove_Ptr != null)
                    return ref *(ulong*)EcsOnRemove_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnRemove", out EcsOnRemove_Ptr);
                return ref *(ulong*)EcsOnRemove_Ptr;
            }
        }

        public static ref ulong EcsOnSet
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnSet_Ptr != null)
                    return ref *(ulong*)EcsOnSet_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnSet", out EcsOnSet_Ptr);
                return ref *(ulong*)EcsOnSet_Ptr;
            }
        }

        public static ref ulong EcsOnStart
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnStart_Ptr != null)
                    return ref *(ulong*)EcsOnStart_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnStart", out EcsOnStart_Ptr);
                return ref *(ulong*)EcsOnStart_Ptr;
            }
        }

        public static ref ulong EcsOnStore
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnStore_Ptr != null)
                    return ref *(ulong*)EcsOnStore_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnStore", out EcsOnStore_Ptr);
                return ref *(ulong*)EcsOnStore_Ptr;
            }
        }

        public static ref ulong EcsOnTableCreate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnTableCreate_Ptr != null)
                    return ref *(ulong*)EcsOnTableCreate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnTableCreate", out EcsOnTableCreate_Ptr);
                return ref *(ulong*)EcsOnTableCreate_Ptr;
            }
        }

        public static ref ulong EcsOnTableDelete
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnTableDelete_Ptr != null)
                    return ref *(ulong*)EcsOnTableDelete_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnTableDelete", out EcsOnTableDelete_Ptr);
                return ref *(ulong*)EcsOnTableDelete_Ptr;
            }
        }

        public static ref ulong EcsOnTableEmpty
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnTableEmpty_Ptr != null)
                    return ref *(ulong*)EcsOnTableEmpty_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnTableEmpty", out EcsOnTableEmpty_Ptr);
                return ref *(ulong*)EcsOnTableEmpty_Ptr;
            }
        }

        public static ref ulong EcsOnTableFill
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnTableFill_Ptr != null)
                    return ref *(ulong*)EcsOnTableFill_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnTableFill", out EcsOnTableFill_Ptr);
                return ref *(ulong*)EcsOnTableFill_Ptr;
            }
        }

        public static ref ulong EcsOnUpdate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnUpdate_Ptr != null)
                    return ref *(ulong*)EcsOnUpdate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnUpdate", out EcsOnUpdate_Ptr);
                return ref *(ulong*)EcsOnUpdate_Ptr;
            }
        }

        public static ref ulong EcsOnValidate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsOnValidate_Ptr != null)
                    return ref *(ulong*)EcsOnValidate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsOnValidate", out EcsOnValidate_Ptr);
                return ref *(ulong*)EcsOnValidate_Ptr;
            }
        }

        public static ref ulong EcsPanic
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPanic_Ptr != null)
                    return ref *(ulong*)EcsPanic_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPanic", out EcsPanic_Ptr);
                return ref *(ulong*)EcsPanic_Ptr;
            }
        }

        public static ref ulong EcsPascal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPascal_Ptr != null)
                    return ref *(ulong*)EcsPascal_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPascal", out EcsPascal_Ptr);
                return ref *(ulong*)EcsPascal_Ptr;
            }
        }

        public static ref ulong EcsPebi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPebi_Ptr != null)
                    return ref *(ulong*)EcsPebi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPebi", out EcsPebi_Ptr);
                return ref *(ulong*)EcsPebi_Ptr;
            }
        }

        public static ref ulong EcsPercentage
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPercentage_Ptr != null)
                    return ref *(ulong*)EcsPercentage_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPercentage", out EcsPercentage_Ptr);
                return ref *(ulong*)EcsPercentage_Ptr;
            }
        }

        public static ref ulong EcsPeriod1d
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPeriod1d_Ptr != null)
                    return ref *(ulong*)EcsPeriod1d_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPeriod1d", out EcsPeriod1d_Ptr);
                return ref *(ulong*)EcsPeriod1d_Ptr;
            }
        }

        public static ref ulong EcsPeriod1h
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPeriod1h_Ptr != null)
                    return ref *(ulong*)EcsPeriod1h_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPeriod1h", out EcsPeriod1h_Ptr);
                return ref *(ulong*)EcsPeriod1h_Ptr;
            }
        }

        public static ref ulong EcsPeriod1m
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPeriod1m_Ptr != null)
                    return ref *(ulong*)EcsPeriod1m_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPeriod1m", out EcsPeriod1m_Ptr);
                return ref *(ulong*)EcsPeriod1m_Ptr;
            }
        }

        public static ref ulong EcsPeriod1s
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPeriod1s_Ptr != null)
                    return ref *(ulong*)EcsPeriod1s_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPeriod1s", out EcsPeriod1s_Ptr);
                return ref *(ulong*)EcsPeriod1s_Ptr;
            }
        }

        public static ref ulong EcsPeriod1w
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPeriod1w_Ptr != null)
                    return ref *(ulong*)EcsPeriod1w_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPeriod1w", out EcsPeriod1w_Ptr);
                return ref *(ulong*)EcsPeriod1w_Ptr;
            }
        }

        public static ref ulong EcsPeta
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPeta_Ptr != null)
                    return ref *(ulong*)EcsPeta_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPeta", out EcsPeta_Ptr);
                return ref *(ulong*)EcsPeta_Ptr;
            }
        }

        public static ref ulong EcsPhase
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPhase_Ptr != null)
                    return ref *(ulong*)EcsPhase_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPhase", out EcsPhase_Ptr);
                return ref *(ulong*)EcsPhase_Ptr;
            }
        }

        public static ref ulong EcsPico
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPico_Ptr != null)
                    return ref *(ulong*)EcsPico_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPico", out EcsPico_Ptr);
                return ref *(ulong*)EcsPico_Ptr;
            }
        }

        public static ref ulong EcsPicoMeters
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPicoMeters_Ptr != null)
                    return ref *(ulong*)EcsPicoMeters_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPicoMeters", out EcsPicoMeters_Ptr);
                return ref *(ulong*)EcsPicoMeters_Ptr;
            }
        }

        public static ref ulong EcsPicoSeconds
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPicoSeconds_Ptr != null)
                    return ref *(ulong*)EcsPicoSeconds_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPicoSeconds", out EcsPicoSeconds_Ptr);
                return ref *(ulong*)EcsPicoSeconds_Ptr;
            }
        }

        public static ref ulong EcsPixels
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPixels_Ptr != null)
                    return ref *(ulong*)EcsPixels_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPixels", out EcsPixels_Ptr);
                return ref *(ulong*)EcsPixels_Ptr;
            }
        }

        public static ref ulong EcsPostFrame
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPostFrame_Ptr != null)
                    return ref *(ulong*)EcsPostFrame_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPostFrame", out EcsPostFrame_Ptr);
                return ref *(ulong*)EcsPostFrame_Ptr;
            }
        }

        public static ref ulong EcsPostLoad
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPostLoad_Ptr != null)
                    return ref *(ulong*)EcsPostLoad_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPostLoad", out EcsPostLoad_Ptr);
                return ref *(ulong*)EcsPostLoad_Ptr;
            }
        }

        public static ref ulong EcsPostUpdate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPostUpdate_Ptr != null)
                    return ref *(ulong*)EcsPostUpdate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPostUpdate", out EcsPostUpdate_Ptr);
                return ref *(ulong*)EcsPostUpdate_Ptr;
            }
        }

        public static ref ulong EcsPredEq
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPredEq_Ptr != null)
                    return ref *(ulong*)EcsPredEq_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPredEq", out EcsPredEq_Ptr);
                return ref *(ulong*)EcsPredEq_Ptr;
            }
        }

        public static ref ulong EcsPredLookup
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPredLookup_Ptr != null)
                    return ref *(ulong*)EcsPredLookup_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPredLookup", out EcsPredLookup_Ptr);
                return ref *(ulong*)EcsPredLookup_Ptr;
            }
        }

        public static ref ulong EcsPredMatch
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPredMatch_Ptr != null)
                    return ref *(ulong*)EcsPredMatch_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPredMatch", out EcsPredMatch_Ptr);
                return ref *(ulong*)EcsPredMatch_Ptr;
            }
        }

        public static ref ulong EcsPrefab
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPrefab_Ptr != null)
                    return ref *(ulong*)EcsPrefab_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPrefab", out EcsPrefab_Ptr);
                return ref *(ulong*)EcsPrefab_Ptr;
            }
        }

        public static ref ulong EcsPreFrame
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPreFrame_Ptr != null)
                    return ref *(ulong*)EcsPreFrame_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPreFrame", out EcsPreFrame_Ptr);
                return ref *(ulong*)EcsPreFrame_Ptr;
            }
        }

        public static ref ulong EcsPressure
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPressure_Ptr != null)
                    return ref *(ulong*)EcsPressure_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPressure", out EcsPressure_Ptr);
                return ref *(ulong*)EcsPressure_Ptr;
            }
        }

        public static ref ulong EcsPreStore
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPreStore_Ptr != null)
                    return ref *(ulong*)EcsPreStore_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPreStore", out EcsPreStore_Ptr);
                return ref *(ulong*)EcsPreStore_Ptr;
            }
        }

        public static ref ulong EcsPreUpdate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPreUpdate_Ptr != null)
                    return ref *(ulong*)EcsPreUpdate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPreUpdate", out EcsPreUpdate_Ptr);
                return ref *(ulong*)EcsPreUpdate_Ptr;
            }
        }

        public static ref ulong EcsPrivate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsPrivate_Ptr != null)
                    return ref *(ulong*)EcsPrivate_Ptr;
                BindgenInternal.LoadDllSymbol("EcsPrivate", out EcsPrivate_Ptr);
                return ref *(ulong*)EcsPrivate_Ptr;
            }
        }

        public static ref ulong EcsQuantity
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsQuantity_Ptr != null)
                    return ref *(ulong*)EcsQuantity_Ptr;
                BindgenInternal.LoadDllSymbol("EcsQuantity", out EcsQuantity_Ptr);
                return ref *(ulong*)EcsQuantity_Ptr;
            }
        }

        public static ref ulong EcsQuery
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsQuery_Ptr != null)
                    return ref *(ulong*)EcsQuery_Ptr;
                BindgenInternal.LoadDllSymbol("EcsQuery", out EcsQuery_Ptr);
                return ref *(ulong*)EcsQuery_Ptr;
            }
        }

        public static ref ulong EcsRadians
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsRadians_Ptr != null)
                    return ref *(ulong*)EcsRadians_Ptr;
                BindgenInternal.LoadDllSymbol("EcsRadians", out EcsRadians_Ptr);
                return ref *(ulong*)EcsRadians_Ptr;
            }
        }

        public static ref ulong EcsReflexive
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsReflexive_Ptr != null)
                    return ref *(ulong*)EcsReflexive_Ptr;
                BindgenInternal.LoadDllSymbol("EcsReflexive", out EcsReflexive_Ptr);
                return ref *(ulong*)EcsReflexive_Ptr;
            }
        }

        public static ref ulong EcsRemove
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsRemove_Ptr != null)
                    return ref *(ulong*)EcsRemove_Ptr;
                BindgenInternal.LoadDllSymbol("EcsRemove", out EcsRemove_Ptr);
                return ref *(ulong*)EcsRemove_Ptr;
            }
        }

        public static ref ulong EcsScopeClose
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsScopeClose_Ptr != null)
                    return ref *(ulong*)EcsScopeClose_Ptr;
                BindgenInternal.LoadDllSymbol("EcsScopeClose", out EcsScopeClose_Ptr);
                return ref *(ulong*)EcsScopeClose_Ptr;
            }
        }

        public static ref ulong EcsScopeOpen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsScopeOpen_Ptr != null)
                    return ref *(ulong*)EcsScopeOpen_Ptr;
                BindgenInternal.LoadDllSymbol("EcsScopeOpen", out EcsScopeOpen_Ptr);
                return ref *(ulong*)EcsScopeOpen_Ptr;
            }
        }

        public static ref ulong EcsSeconds
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsSeconds_Ptr != null)
                    return ref *(ulong*)EcsSeconds_Ptr;
                BindgenInternal.LoadDllSymbol("EcsSeconds", out EcsSeconds_Ptr);
                return ref *(ulong*)EcsSeconds_Ptr;
            }
        }

        public static ref ulong EcsSlotOf
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsSlotOf_Ptr != null)
                    return ref *(ulong*)EcsSlotOf_Ptr;
                BindgenInternal.LoadDllSymbol("EcsSlotOf", out EcsSlotOf_Ptr);
                return ref *(ulong*)EcsSlotOf_Ptr;
            }
        }

        public static ref ulong EcsSpeed
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsSpeed_Ptr != null)
                    return ref *(ulong*)EcsSpeed_Ptr;
                BindgenInternal.LoadDllSymbol("EcsSpeed", out EcsSpeed_Ptr);
                return ref *(ulong*)EcsSpeed_Ptr;
            }
        }

        public static ref ulong EcsSymbol
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsSymbol_Ptr != null)
                    return ref *(ulong*)EcsSymbol_Ptr;
                BindgenInternal.LoadDllSymbol("EcsSymbol", out EcsSymbol_Ptr);
                return ref *(ulong*)EcsSymbol_Ptr;
            }
        }

        public static ref ulong EcsSymmetric
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsSymmetric_Ptr != null)
                    return ref *(ulong*)EcsSymmetric_Ptr;
                BindgenInternal.LoadDllSymbol("EcsSymmetric", out EcsSymmetric_Ptr);
                return ref *(ulong*)EcsSymmetric_Ptr;
            }
        }

        public static ref ulong EcsSystem
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsSystem_Ptr != null)
                    return ref *(ulong*)EcsSystem_Ptr;
                BindgenInternal.LoadDllSymbol("EcsSystem", out EcsSystem_Ptr);
                return ref *(ulong*)EcsSystem_Ptr;
            }
        }

        public static ref ulong EcsTag
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTag_Ptr != null)
                    return ref *(ulong*)EcsTag_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTag", out EcsTag_Ptr);
                return ref *(ulong*)EcsTag_Ptr;
            }
        }

        public static ref ulong EcsTebi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTebi_Ptr != null)
                    return ref *(ulong*)EcsTebi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTebi", out EcsTebi_Ptr);
                return ref *(ulong*)EcsTebi_Ptr;
            }
        }

        public static ref ulong EcsTemperature
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTemperature_Ptr != null)
                    return ref *(ulong*)EcsTemperature_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTemperature", out EcsTemperature_Ptr);
                return ref *(ulong*)EcsTemperature_Ptr;
            }
        }

        public static ref ulong EcsTera
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTera_Ptr != null)
                    return ref *(ulong*)EcsTera_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTera", out EcsTera_Ptr);
                return ref *(ulong*)EcsTera_Ptr;
            }
        }

        public static ref ulong EcsThis
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsThis_Ptr != null)
                    return ref *(ulong*)EcsThis_Ptr;
                BindgenInternal.LoadDllSymbol("EcsThis", out EcsThis_Ptr);
                return ref *(ulong*)EcsThis_Ptr;
            }
        }

        public static ref ulong EcsTime
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTime_Ptr != null)
                    return ref *(ulong*)EcsTime_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTime", out EcsTime_Ptr);
                return ref *(ulong*)EcsTime_Ptr;
            }
        }

        public static ref ulong EcsTransitive
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTransitive_Ptr != null)
                    return ref *(ulong*)EcsTransitive_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTransitive", out EcsTransitive_Ptr);
                return ref *(ulong*)EcsTransitive_Ptr;
            }
        }

        public static ref ulong EcsTraversable
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsTraversable_Ptr != null)
                    return ref *(ulong*)EcsTraversable_Ptr;
                BindgenInternal.LoadDllSymbol("EcsTraversable", out EcsTraversable_Ptr);
                return ref *(ulong*)EcsTraversable_Ptr;
            }
        }

        public static ref ulong EcsUnion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUnion_Ptr != null)
                    return ref *(ulong*)EcsUnion_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUnion", out EcsUnion_Ptr);
                return ref *(ulong*)EcsUnion_Ptr;
            }
        }

        public static ref ulong EcsUnitPrefixes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUnitPrefixes_Ptr != null)
                    return ref *(ulong*)EcsUnitPrefixes_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUnitPrefixes", out EcsUnitPrefixes_Ptr);
                return ref *(ulong*)EcsUnitPrefixes_Ptr;
            }
        }

        public static ref ulong EcsUnSet
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUnSet_Ptr != null)
                    return ref *(ulong*)EcsUnSet_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUnSet", out EcsUnSet_Ptr);
                return ref *(ulong*)EcsUnSet_Ptr;
            }
        }

        public static ref ulong EcsUri
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUri_Ptr != null)
                    return ref *(ulong*)EcsUri_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUri", out EcsUri_Ptr);
                return ref *(ulong*)EcsUri_Ptr;
            }
        }

        public static ref ulong EcsUriFile
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUriFile_Ptr != null)
                    return ref *(ulong*)EcsUriFile_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUriFile", out EcsUriFile_Ptr);
                return ref *(ulong*)EcsUriFile_Ptr;
            }
        }

        public static ref ulong EcsUriHyperlink
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUriHyperlink_Ptr != null)
                    return ref *(ulong*)EcsUriHyperlink_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUriHyperlink", out EcsUriHyperlink_Ptr);
                return ref *(ulong*)EcsUriHyperlink_Ptr;
            }
        }

        public static ref ulong EcsUriImage
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsUriImage_Ptr != null)
                    return ref *(ulong*)EcsUriImage_Ptr;
                BindgenInternal.LoadDllSymbol("EcsUriImage", out EcsUriImage_Ptr);
                return ref *(ulong*)EcsUriImage_Ptr;
            }
        }

        public static ref ulong EcsVariable
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsVariable_Ptr != null)
                    return ref *(ulong*)EcsVariable_Ptr;
                BindgenInternal.LoadDllSymbol("EcsVariable", out EcsVariable_Ptr);
                return ref *(ulong*)EcsVariable_Ptr;
            }
        }

        public static ref ulong EcsWildcard
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsWildcard_Ptr != null)
                    return ref *(ulong*)EcsWildcard_Ptr;
                BindgenInternal.LoadDllSymbol("EcsWildcard", out EcsWildcard_Ptr);
                return ref *(ulong*)EcsWildcard_Ptr;
            }
        }

        public static ref ulong EcsWith
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsWith_Ptr != null)
                    return ref *(ulong*)EcsWith_Ptr;
                BindgenInternal.LoadDllSymbol("EcsWith", out EcsWith_Ptr);
                return ref *(ulong*)EcsWith_Ptr;
            }
        }

        public static ref ulong EcsWorld
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsWorld_Ptr != null)
                    return ref *(ulong*)EcsWorld_Ptr;
                BindgenInternal.LoadDllSymbol("EcsWorld", out EcsWorld_Ptr);
                return ref *(ulong*)EcsWorld_Ptr;
            }
        }

        public static ref ulong EcsYobi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsYobi_Ptr != null)
                    return ref *(ulong*)EcsYobi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsYobi", out EcsYobi_Ptr);
                return ref *(ulong*)EcsYobi_Ptr;
            }
        }

        public static ref ulong EcsYocto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsYocto_Ptr != null)
                    return ref *(ulong*)EcsYocto_Ptr;
                BindgenInternal.LoadDllSymbol("EcsYocto", out EcsYocto_Ptr);
                return ref *(ulong*)EcsYocto_Ptr;
            }
        }

        public static ref ulong EcsYotta
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsYotta_Ptr != null)
                    return ref *(ulong*)EcsYotta_Ptr;
                BindgenInternal.LoadDllSymbol("EcsYotta", out EcsYotta_Ptr);
                return ref *(ulong*)EcsYotta_Ptr;
            }
        }

        public static ref ulong EcsZebi
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsZebi_Ptr != null)
                    return ref *(ulong*)EcsZebi_Ptr;
                BindgenInternal.LoadDllSymbol("EcsZebi", out EcsZebi_Ptr);
                return ref *(ulong*)EcsZebi_Ptr;
            }
        }

        public static ref ulong EcsZepto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsZepto_Ptr != null)
                    return ref *(ulong*)EcsZepto_Ptr;
                BindgenInternal.LoadDllSymbol("EcsZepto", out EcsZepto_Ptr);
                return ref *(ulong*)EcsZepto_Ptr;
            }
        }

        public static ref ulong EcsZetta
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (EcsZetta_Ptr != null)
                    return ref *(ulong*)EcsZetta_Ptr;
                BindgenInternal.LoadDllSymbol("EcsZetta", out EcsZetta_Ptr);
                return ref *(ulong*)EcsZetta_Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_bool_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_bool_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_bool_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_bool_tID_", out FLECS_IDecs_bool_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_bool_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_byte_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_byte_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_byte_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_byte_tID_", out FLECS_IDecs_byte_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_byte_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_char_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_char_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_char_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_char_tID_", out FLECS_IDecs_char_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_char_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_entity_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_entity_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_entity_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_entity_tID_", out FLECS_IDecs_entity_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_entity_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_f32_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_f32_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_f32_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_f32_tID_", out FLECS_IDecs_f32_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_f32_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_f64_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_f64_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_f64_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_f64_tID_", out FLECS_IDecs_f64_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_f64_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_i16_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_i16_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_i16_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_i16_tID_", out FLECS_IDecs_i16_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_i16_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_i32_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_i32_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_i32_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_i32_tID_", out FLECS_IDecs_i32_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_i32_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_i64_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_i64_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_i64_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_i64_tID_", out FLECS_IDecs_i64_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_i64_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_i8_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_i8_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_i8_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_i8_tID_", out FLECS_IDecs_i8_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_i8_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_iptr_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_iptr_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_iptr_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_iptr_tID_", out FLECS_IDecs_iptr_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_iptr_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_string_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_string_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_string_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_string_tID_", out FLECS_IDecs_string_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_string_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_u16_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_u16_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_u16_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_u16_tID_", out FLECS_IDecs_u16_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_u16_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_u32_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_u32_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_u32_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_u32_tID_", out FLECS_IDecs_u32_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_u32_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_u64_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_u64_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_u64_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_u64_tID_", out FLECS_IDecs_u64_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_u64_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_u8_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_u8_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_u8_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_u8_tID_", out FLECS_IDecs_u8_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_u8_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDecs_uptr_tID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDecs_uptr_tID__Ptr != null)
                    return ref *(ulong*)FLECS_IDecs_uptr_tID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDecs_uptr_tID_", out FLECS_IDecs_uptr_tID__Ptr);
                return ref *(ulong*)FLECS_IDecs_uptr_tID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAccelerationID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAccelerationID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAccelerationID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAccelerationID_", out FLECS_IDEcsAccelerationID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAccelerationID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertCriticalID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertCriticalID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertCriticalID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertCriticalID_", out FLECS_IDEcsAlertCriticalID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertCriticalID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertErrorID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertErrorID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertErrorID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertErrorID_", out FLECS_IDEcsAlertErrorID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertErrorID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertID_", out FLECS_IDEcsAlertID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertInfoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertInfoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertInfoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertInfoID_", out FLECS_IDEcsAlertInfoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertInfoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertInstanceID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertInstanceID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertInstanceID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertInstanceID_", out FLECS_IDEcsAlertInstanceID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertInstanceID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertsActiveID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertsActiveID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertsActiveID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertsActiveID_", out FLECS_IDEcsAlertsActiveID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertsActiveID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertTimeoutID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertTimeoutID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertTimeoutID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertTimeoutID_", out FLECS_IDEcsAlertTimeoutID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertTimeoutID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAlertWarningID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAlertWarningID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAlertWarningID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAlertWarningID_", out FLECS_IDEcsAlertWarningID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAlertWarningID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAmountID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAmountID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAmountID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAmountID_", out FLECS_IDEcsAmountID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAmountID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAmpereID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAmpereID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAmpereID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAmpereID_", out FLECS_IDEcsAmpereID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAmpereID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAngleID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAngleID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAngleID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAngleID_", out FLECS_IDEcsAngleID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAngleID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsArrayID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsArrayID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsArrayID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsArrayID_", out FLECS_IDEcsArrayID__Ptr);
                return ref *(ulong*)FLECS_IDEcsArrayID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsAttoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsAttoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsAttoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsAttoID_", out FLECS_IDEcsAttoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsAttoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBarID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBarID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBarID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBarID_", out FLECS_IDEcsBarID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBarID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBelID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBelID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBelID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBelID_", out FLECS_IDEcsBelID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBelID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBitmaskID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBitmaskID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBitmaskID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBitmaskID_", out FLECS_IDEcsBitmaskID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBitmaskID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBitsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBitsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBitsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBitsID_", out FLECS_IDEcsBitsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBitsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBitsPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBitsPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBitsPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBitsPerSecondID_", out FLECS_IDEcsBitsPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBitsPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBytesID_", out FLECS_IDEcsBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsBytesPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsBytesPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsBytesPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsBytesPerSecondID_", out FLECS_IDEcsBytesPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsBytesPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCandelaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCandelaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCandelaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCandelaID_", out FLECS_IDEcsCandelaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCandelaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCelsiusID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCelsiusID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCelsiusID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCelsiusID_", out FLECS_IDEcsCelsiusID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCelsiusID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCentiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCentiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCentiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCentiID_", out FLECS_IDEcsCentiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCentiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCentiMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCentiMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCentiMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCentiMetersID_", out FLECS_IDEcsCentiMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCentiMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsComponentID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsComponentID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsComponentID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsComponentID_", out FLECS_IDEcsComponentID__Ptr);
                return ref *(ulong*)FLECS_IDEcsComponentID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCounterID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCounterID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCounterID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCounterID_", out FLECS_IDEcsCounterID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCounterID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCounterIdID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCounterIdID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCounterIdID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCounterIdID_", out FLECS_IDEcsCounterIdID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCounterIdID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsCounterIncrementID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsCounterIncrementID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsCounterIncrementID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsCounterIncrementID_", out FLECS_IDEcsCounterIncrementID__Ptr);
                return ref *(ulong*)FLECS_IDEcsCounterIncrementID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDataID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDataID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDataID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDataID_", out FLECS_IDEcsDataID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDataID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDataRateID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDataRateID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDataRateID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDataRateID_", out FLECS_IDEcsDataRateID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDataRateID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDateID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDateID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDateID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDateID_", out FLECS_IDEcsDateID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDateID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDaysID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDaysID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDaysID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDaysID_", out FLECS_IDEcsDaysID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDaysID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDecaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDecaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDecaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDecaID_", out FLECS_IDEcsDecaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDecaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDeciBelID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDeciBelID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDeciBelID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDeciBelID_", out FLECS_IDEcsDeciBelID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDeciBelID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDeciID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDeciID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDeciID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDeciID_", out FLECS_IDEcsDeciID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDeciID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDegreesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDegreesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDegreesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDegreesID_", out FLECS_IDEcsDegreesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDegreesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDocDescriptionID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDocDescriptionID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDocDescriptionID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDocDescriptionID_", out FLECS_IDEcsDocDescriptionID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDocDescriptionID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsDurationID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsDurationID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsDurationID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsDurationID_", out FLECS_IDEcsDurationID__Ptr);
                return ref *(ulong*)FLECS_IDEcsDurationID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsElectricCurrentID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsElectricCurrentID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsElectricCurrentID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsElectricCurrentID_", out FLECS_IDEcsElectricCurrentID__Ptr);
                return ref *(ulong*)FLECS_IDEcsElectricCurrentID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsEnumID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsEnumID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsEnumID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsEnumID_", out FLECS_IDEcsEnumID__Ptr);
                return ref *(ulong*)FLECS_IDEcsEnumID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsExaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsExaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsExaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsExaID_", out FLECS_IDEcsExaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsExaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsExbiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsExbiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsExbiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsExbiID_", out FLECS_IDEcsExbiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsExbiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsFahrenheitID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsFahrenheitID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsFahrenheitID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsFahrenheitID_", out FLECS_IDEcsFahrenheitID__Ptr);
                return ref *(ulong*)FLECS_IDEcsFahrenheitID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsFemtoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsFemtoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsFemtoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsFemtoID_", out FLECS_IDEcsFemtoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsFemtoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsForceID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsForceID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsForceID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsForceID_", out FLECS_IDEcsForceID__Ptr);
                return ref *(ulong*)FLECS_IDEcsForceID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsFrequencyID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsFrequencyID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsFrequencyID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsFrequencyID_", out FLECS_IDEcsFrequencyID__Ptr);
                return ref *(ulong*)FLECS_IDEcsFrequencyID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGaugeID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGaugeID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGaugeID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGaugeID_", out FLECS_IDEcsGaugeID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGaugeID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGibiBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGibiBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGibiBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGibiBytesID_", out FLECS_IDEcsGibiBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGibiBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGibiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGibiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGibiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGibiID_", out FLECS_IDEcsGibiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGibiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGigaBitsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGigaBitsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGigaBitsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGigaBitsID_", out FLECS_IDEcsGigaBitsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGigaBitsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGigaBitsPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGigaBitsPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGigaBitsPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGigaBitsPerSecondID_", out FLECS_IDEcsGigaBitsPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGigaBitsPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGigaBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGigaBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGigaBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGigaBytesID_", out FLECS_IDEcsGigaBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGigaBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGigaBytesPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGigaBytesPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGigaBytesPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGigaBytesPerSecondID_", out FLECS_IDEcsGigaBytesPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGigaBytesPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGigaHertzID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGigaHertzID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGigaHertzID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGigaHertzID_", out FLECS_IDEcsGigaHertzID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGigaHertzID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGigaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGigaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGigaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGigaID_", out FLECS_IDEcsGigaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGigaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsGramsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsGramsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsGramsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsGramsID_", out FLECS_IDEcsGramsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsGramsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsHectoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsHectoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsHectoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsHectoID_", out FLECS_IDEcsHectoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsHectoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsHertzID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsHertzID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsHertzID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsHertzID_", out FLECS_IDEcsHertzID__Ptr);
                return ref *(ulong*)FLECS_IDEcsHertzID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsHoursID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsHoursID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsHoursID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsHoursID_", out FLECS_IDEcsHoursID__Ptr);
                return ref *(ulong*)FLECS_IDEcsHoursID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsIdentifierID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsIdentifierID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsIdentifierID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsIdentifierID_", out FLECS_IDEcsIdentifierID__Ptr);
                return ref *(ulong*)FLECS_IDEcsIdentifierID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsIterableID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsIterableID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsIterableID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsIterableID_", out FLECS_IDEcsIterableID__Ptr);
                return ref *(ulong*)FLECS_IDEcsIterableID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKelvinID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKelvinID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKelvinID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKelvinID_", out FLECS_IDEcsKelvinID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKelvinID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKibiBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKibiBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKibiBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKibiBytesID_", out FLECS_IDEcsKibiBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKibiBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKibiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKibiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKibiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKibiID_", out FLECS_IDEcsKibiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKibiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloBitsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloBitsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloBitsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloBitsID_", out FLECS_IDEcsKiloBitsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloBitsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloBitsPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloBitsPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloBitsPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloBitsPerSecondID_", out FLECS_IDEcsKiloBitsPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloBitsPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloBytesID_", out FLECS_IDEcsKiloBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloBytesPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloBytesPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloBytesPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloBytesPerSecondID_", out FLECS_IDEcsKiloBytesPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloBytesPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloGramsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloGramsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloGramsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloGramsID_", out FLECS_IDEcsKiloGramsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloGramsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloHertzID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloHertzID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloHertzID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloHertzID_", out FLECS_IDEcsKiloHertzID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloHertzID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloID_", out FLECS_IDEcsKiloID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloMetersID_", out FLECS_IDEcsKiloMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloMetersPerHourID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloMetersPerHourID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloMetersPerHourID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloMetersPerHourID_", out FLECS_IDEcsKiloMetersPerHourID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloMetersPerHourID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsKiloMetersPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsKiloMetersPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsKiloMetersPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsKiloMetersPerSecondID_", out FLECS_IDEcsKiloMetersPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsKiloMetersPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsLengthID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsLengthID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsLengthID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsLengthID_", out FLECS_IDEcsLengthID__Ptr);
                return ref *(ulong*)FLECS_IDEcsLengthID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsLuminousIntensityID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsLuminousIntensityID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsLuminousIntensityID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsLuminousIntensityID_", out FLECS_IDEcsLuminousIntensityID__Ptr);
                return ref *(ulong*)FLECS_IDEcsLuminousIntensityID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMassID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMassID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMassID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMassID_", out FLECS_IDEcsMassID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMassID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMebiBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMebiBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMebiBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMebiBytesID_", out FLECS_IDEcsMebiBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMebiBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMebiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMebiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMebiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMebiID_", out FLECS_IDEcsMebiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMebiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMegaBitsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMegaBitsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMegaBitsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMegaBitsID_", out FLECS_IDEcsMegaBitsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMegaBitsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMegaBitsPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMegaBitsPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMegaBitsPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMegaBitsPerSecondID_", out FLECS_IDEcsMegaBitsPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMegaBitsPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMegaBytesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMegaBytesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMegaBytesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMegaBytesID_", out FLECS_IDEcsMegaBytesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMegaBytesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMegaBytesPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMegaBytesPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMegaBytesPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMegaBytesPerSecondID_", out FLECS_IDEcsMegaBytesPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMegaBytesPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMegaHertzID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMegaHertzID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMegaHertzID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMegaHertzID_", out FLECS_IDEcsMegaHertzID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMegaHertzID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMegaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMegaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMegaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMegaID_", out FLECS_IDEcsMegaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMegaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMemberID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMemberID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMemberID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMemberID_", out FLECS_IDEcsMemberID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMemberID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMemberRangesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMemberRangesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMemberRangesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMemberRangesID_", out FLECS_IDEcsMemberRangesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMemberRangesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetaTypeID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetaTypeID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetaTypeID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetaTypeID_", out FLECS_IDEcsMetaTypeID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetaTypeID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetaTypeSerializedID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetaTypeSerializedID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetaTypeSerializedID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetaTypeSerializedID_", out FLECS_IDEcsMetaTypeSerializedID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetaTypeSerializedID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetersID_", out FLECS_IDEcsMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetersPerSecondID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetersPerSecondID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetersPerSecondID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetersPerSecondID_", out FLECS_IDEcsMetersPerSecondID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetersPerSecondID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetricID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetricID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetricID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetricID_", out FLECS_IDEcsMetricID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetricID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetricInstanceID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetricInstanceID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetricInstanceID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetricInstanceID_", out FLECS_IDEcsMetricInstanceID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetricInstanceID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetricSourceID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetricSourceID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetricSourceID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetricSourceID_", out FLECS_IDEcsMetricSourceID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetricSourceID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMetricValueID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMetricValueID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMetricValueID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMetricValueID_", out FLECS_IDEcsMetricValueID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMetricValueID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMicroID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMicroID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMicroID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMicroID_", out FLECS_IDEcsMicroID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMicroID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMicroMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMicroMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMicroMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMicroMetersID_", out FLECS_IDEcsMicroMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMicroMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMicroSecondsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMicroSecondsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMicroSecondsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMicroSecondsID_", out FLECS_IDEcsMicroSecondsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMicroSecondsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMilesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMilesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMilesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMilesID_", out FLECS_IDEcsMilesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMilesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMilesPerHourID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMilesPerHourID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMilesPerHourID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMilesPerHourID_", out FLECS_IDEcsMilesPerHourID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMilesPerHourID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMilliID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMilliID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMilliID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMilliID_", out FLECS_IDEcsMilliID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMilliID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMilliMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMilliMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMilliMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMilliMetersID_", out FLECS_IDEcsMilliMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMilliMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMilliSecondsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMilliSecondsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMilliSecondsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMilliSecondsID_", out FLECS_IDEcsMilliSecondsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMilliSecondsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMinutesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMinutesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMinutesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMinutesID_", out FLECS_IDEcsMinutesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMinutesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsMoleID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsMoleID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsMoleID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsMoleID_", out FLECS_IDEcsMoleID__Ptr);
                return ref *(ulong*)FLECS_IDEcsMoleID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsNanoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsNanoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsNanoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsNanoID_", out FLECS_IDEcsNanoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsNanoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsNanoMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsNanoMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsNanoMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsNanoMetersID_", out FLECS_IDEcsNanoMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsNanoMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsNanoSecondsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsNanoSecondsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsNanoSecondsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsNanoSecondsID_", out FLECS_IDEcsNanoSecondsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsNanoSecondsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsNewtonID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsNewtonID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsNewtonID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsNewtonID_", out FLECS_IDEcsNewtonID__Ptr);
                return ref *(ulong*)FLECS_IDEcsNewtonID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsOpaqueID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsOpaqueID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsOpaqueID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsOpaqueID_", out FLECS_IDEcsOpaqueID__Ptr);
                return ref *(ulong*)FLECS_IDEcsOpaqueID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPascalID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPascalID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPascalID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPascalID_", out FLECS_IDEcsPascalID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPascalID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPebiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPebiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPebiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPebiID_", out FLECS_IDEcsPebiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPebiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPercentageID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPercentageID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPercentageID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPercentageID_", out FLECS_IDEcsPercentageID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPercentageID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPetaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPetaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPetaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPetaID_", out FLECS_IDEcsPetaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPetaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPicoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPicoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPicoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPicoID_", out FLECS_IDEcsPicoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPicoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPicoMetersID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPicoMetersID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPicoMetersID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPicoMetersID_", out FLECS_IDEcsPicoMetersID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPicoMetersID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPicoSecondsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPicoSecondsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPicoSecondsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPicoSecondsID_", out FLECS_IDEcsPicoSecondsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPicoSecondsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPipelineID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPipelineID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPipelineID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPipelineID_", out FLECS_IDEcsPipelineID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPipelineID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPipelineQueryID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPipelineQueryID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPipelineQueryID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPipelineQueryID_", out FLECS_IDEcsPipelineQueryID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPipelineQueryID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPipelineStatsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPipelineStatsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPipelineStatsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPipelineStatsID_", out FLECS_IDEcsPipelineStatsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPipelineStatsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPixelsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPixelsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPixelsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPixelsID_", out FLECS_IDEcsPixelsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPixelsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPolyID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPolyID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPolyID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPolyID_", out FLECS_IDEcsPolyID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPolyID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPressureID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPressureID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPressureID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPressureID_", out FLECS_IDEcsPressureID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPressureID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsPrimitiveID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsPrimitiveID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsPrimitiveID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsPrimitiveID_", out FLECS_IDEcsPrimitiveID__Ptr);
                return ref *(ulong*)FLECS_IDEcsPrimitiveID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsRadiansID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsRadiansID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsRadiansID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsRadiansID_", out FLECS_IDEcsRadiansID__Ptr);
                return ref *(ulong*)FLECS_IDEcsRadiansID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsRateFilterID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsRateFilterID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsRateFilterID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsRateFilterID_", out FLECS_IDEcsRateFilterID__Ptr);
                return ref *(ulong*)FLECS_IDEcsRateFilterID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsRestID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsRestID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsRestID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsRestID_", out FLECS_IDEcsRestID__Ptr);
                return ref *(ulong*)FLECS_IDEcsRestID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsScriptID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsScriptID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsScriptID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsScriptID_", out FLECS_IDEcsScriptID__Ptr);
                return ref *(ulong*)FLECS_IDEcsScriptID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsSecondsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsSecondsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsSecondsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsSecondsID_", out FLECS_IDEcsSecondsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsSecondsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsSpeedID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsSpeedID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsSpeedID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsSpeedID_", out FLECS_IDEcsSpeedID__Ptr);
                return ref *(ulong*)FLECS_IDEcsSpeedID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsStructID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsStructID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsStructID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsStructID_", out FLECS_IDEcsStructID__Ptr);
                return ref *(ulong*)FLECS_IDEcsStructID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTargetID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTargetID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTargetID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTargetID_", out FLECS_IDEcsTargetID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTargetID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTebiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTebiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTebiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTebiID_", out FLECS_IDEcsTebiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTebiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTemperatureID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTemperatureID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTemperatureID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTemperatureID_", out FLECS_IDEcsTemperatureID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTemperatureID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTeraID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTeraID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTeraID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTeraID_", out FLECS_IDEcsTeraID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTeraID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTickSourceID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTickSourceID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTickSourceID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTickSourceID_", out FLECS_IDEcsTickSourceID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTickSourceID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTimeID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTimeID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTimeID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTimeID_", out FLECS_IDEcsTimeID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTimeID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsTimerID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsTimerID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsTimerID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsTimerID_", out FLECS_IDEcsTimerID__Ptr);
                return ref *(ulong*)FLECS_IDEcsTimerID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUnitID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUnitID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUnitID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUnitID_", out FLECS_IDEcsUnitID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUnitID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUnitPrefixesID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUnitPrefixesID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUnitPrefixesID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUnitPrefixesID_", out FLECS_IDEcsUnitPrefixesID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUnitPrefixesID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUnitPrefixID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUnitPrefixID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUnitPrefixID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUnitPrefixID_", out FLECS_IDEcsUnitPrefixID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUnitPrefixID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUriFileID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUriFileID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUriFileID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUriFileID_", out FLECS_IDEcsUriFileID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUriFileID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUriHyperlinkID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUriHyperlinkID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUriHyperlinkID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUriHyperlinkID_", out FLECS_IDEcsUriHyperlinkID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUriHyperlinkID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUriID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUriID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUriID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUriID_", out FLECS_IDEcsUriID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUriID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsUriImageID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsUriImageID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsUriImageID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsUriImageID_", out FLECS_IDEcsUriImageID__Ptr);
                return ref *(ulong*)FLECS_IDEcsUriImageID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsVectorID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsVectorID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsVectorID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsVectorID_", out FLECS_IDEcsVectorID__Ptr);
                return ref *(ulong*)FLECS_IDEcsVectorID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsWorldStatsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsWorldStatsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsWorldStatsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsWorldStatsID_", out FLECS_IDEcsWorldStatsID__Ptr);
                return ref *(ulong*)FLECS_IDEcsWorldStatsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsWorldSummaryID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsWorldSummaryID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsWorldSummaryID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsWorldSummaryID_", out FLECS_IDEcsWorldSummaryID__Ptr);
                return ref *(ulong*)FLECS_IDEcsWorldSummaryID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsYobiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsYobiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsYobiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsYobiID_", out FLECS_IDEcsYobiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsYobiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsYoctoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsYoctoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsYoctoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsYoctoID_", out FLECS_IDEcsYoctoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsYoctoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsYottaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsYottaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsYottaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsYottaID_", out FLECS_IDEcsYottaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsYottaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsZebiID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsZebiID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsZebiID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsZebiID_", out FLECS_IDEcsZebiID__Ptr);
                return ref *(ulong*)FLECS_IDEcsZebiID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsZeptoID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsZeptoID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsZeptoID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsZeptoID_", out FLECS_IDEcsZeptoID__Ptr);
                return ref *(ulong*)FLECS_IDEcsZeptoID__Ptr;
            }
        }

        public static ref ulong FLECS_IDEcsZettaID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDEcsZettaID__Ptr != null)
                    return ref *(ulong*)FLECS_IDEcsZettaID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDEcsZettaID_", out FLECS_IDEcsZettaID__Ptr);
                return ref *(ulong*)FLECS_IDEcsZettaID__Ptr;
            }
        }

        public static ref ulong FLECS_IDFlecsAlertsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDFlecsAlertsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDFlecsAlertsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDFlecsAlertsID_", out FLECS_IDFlecsAlertsID__Ptr);
                return ref *(ulong*)FLECS_IDFlecsAlertsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDFlecsMetricsID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDFlecsMetricsID__Ptr != null)
                    return ref *(ulong*)FLECS_IDFlecsMetricsID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDFlecsMetricsID_", out FLECS_IDFlecsMetricsID__Ptr);
                return ref *(ulong*)FLECS_IDFlecsMetricsID__Ptr;
            }
        }

        public static ref ulong FLECS_IDFlecsMonitorID_
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            get
            {
                if (FLECS_IDFlecsMonitorID__Ptr != null)
                    return ref *(ulong*)FLECS_IDFlecsMonitorID__Ptr;
                BindgenInternal.LoadDllSymbol("FLECS_IDFlecsMonitorID_", out FLECS_IDFlecsMonitorID__Ptr);
                return ref *(ulong*)FLECS_IDFlecsMonitorID__Ptr;
            }
        }

        private partial class BindgenInternal
        {
            public const string DllImportPath = "libflecs";

            static BindgenInternal()
            {
                DllFilePaths = new string[]
                {
                    "libflecs",
                    "runtimes/linux-x64/native/libflecs",
                    "runtimes/linux-arm64/native/libflecs",
                    "runtimes/osx-x64/native/libflecs",
                    "runtimes/osx-arm64/native/libflecs",
                    "runtimes/win-x64/native/libflecs",
                    "runtimes/win-arm64/native/libflecs"
                };
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "SYSLIB1054")]
        private partial class BindgenInternal
        {
            private static readonly string[] DllFilePaths;

            private static System.IntPtr _libraryHandle = System.IntPtr.Zero;

            private static bool IsLinux => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);

            private static bool IsOsx => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX);

            private static bool IsWindows => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

            [System.Runtime.InteropServices.DllImport("libc", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "dlopen")]
            private static extern System.IntPtr LoadLibraryLinux(string? path, int flags);

            [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "dlopen")]
            private static extern System.IntPtr LoadLibraryOsx(string? path, int flags);

            [System.Runtime.InteropServices.DllImport("kernel32", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "LoadLibrary")]
            private static extern System.IntPtr LoadLibraryWindows(string path);

            [System.Runtime.InteropServices.DllImport("kernel32", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "GetModuleHandle")]
            private static extern System.IntPtr GetModuleHandle(string? name);

            [System.Runtime.InteropServices.DllImport("libc", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "dlsym")]
            private static extern System.IntPtr GetExportLinux(System.IntPtr handle, string name);

            [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "dlsym")]
            private static extern System.IntPtr GetExportOsx(System.IntPtr handle, string name);

            [System.Runtime.InteropServices.DllImport("kernel32", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "GetProcAddress")]
            private static extern System.IntPtr GetExportWindows(System.IntPtr handle, string name);

            [System.Runtime.InteropServices.DllImport("libc", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "dlerror")]
            private static extern byte* GetLastErrorLinux();

            [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "dlerror")]
            private static extern byte* GetLastErrorOsx();

            [System.Runtime.InteropServices.DllImport("kernel32", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, CharSet = System.Runtime.InteropServices.CharSet.Ansi, EntryPoint = "GetLastError")]
            private static extern int GetLastErrorWindows();

            private static bool TryLoad(string path, out System.IntPtr handle)
            {
#if NET5_0_OR_GREATER
            return System.Runtime.InteropServices.NativeLibrary.TryLoad(path, out handle);
#else
                handle = System.IntPtr.Zero;
                if (IsLinux)
                    handle = LoadLibraryLinux(path, 0x101);
                else if (IsOsx)
                    handle = LoadLibraryOsx(path, 0x101);
                else if (IsWindows)
                    handle = LoadLibraryWindows(path);
                return handle != System.IntPtr.Zero;
#endif
            }

            private static System.IntPtr GetExport(string symbol)
            {
#if NET5_0_OR_GREATER
            return System.Runtime.InteropServices.NativeLibrary.GetExport(_libraryHandle, symbol);
#else
                if (IsLinux)
                {
                    GetLastErrorLinux();
                    System.IntPtr handle = GetExportLinux(_libraryHandle, symbol);
                    if (handle != System.IntPtr.Zero)
                        return handle;
                    byte* errorResult = GetLastErrorLinux();
                    if (errorResult == null)
                        return handle;
                    string errorMessage = System.Runtime.InteropServices.Marshal.PtrToStringAnsi((System.IntPtr)errorResult)!;
                    throw new System.EntryPointNotFoundException(errorMessage);
                }

                if (IsOsx)
                {
                    GetLastErrorOsx();
                    System.IntPtr handle = GetExportOsx(_libraryHandle, symbol);
                    if (handle != System.IntPtr.Zero)
                        return handle;
                    byte* errorResult = GetLastErrorOsx();
                    if (errorResult == null)
                        return handle;
                    string errorMessage = System.Runtime.InteropServices.Marshal.PtrToStringAnsi((System.IntPtr)errorResult)!;
                    throw new System.EntryPointNotFoundException(errorMessage);
                }

                if (IsWindows)
                {
                    System.IntPtr handle = GetExportWindows(_libraryHandle, symbol);
                    if (handle != System.IntPtr.Zero)
                        return handle;
                    int errorCode = GetLastErrorWindows();
                    string errorMessage = new System.ComponentModel.Win32Exception(errorCode).Message;
                    throw new System.EntryPointNotFoundException($"{errorMessage} \"{symbol}\" not found.");
                }

                throw new System.InvalidOperationException($"Failed to export symbol \"{symbol}\" from dll. Platform is not linux, mac, or windows.");
#endif
            }

            private static void ResolveLibrary()
            {
                string fileExtension;
                if (IsLinux)
                    fileExtension = ".so";
                else if (IsOsx)
                    fileExtension = ".dylib";
                else if (IsWindows)
                    fileExtension = ".dll";
                else
                    throw new System.InvalidOperationException("Can't determine native library file extension for the current system.");
                foreach (string dllFilePath in DllFilePaths)
                {
                    string fileName = System.IO.Path.GetFileName(dllFilePath);
                    string parentDir = $"{dllFilePath}/..";
                    string searchDir = System.IO.Path.IsPathRooted(dllFilePath) ? System.IO.Path.GetFullPath(parentDir) + "/" : System.IO.Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + parentDir) + "/";
                    if (TryLoad($"{searchDir}{fileName}", out _libraryHandle))
                        return;
                    if (TryLoad($"{searchDir}{fileName}{fileExtension}", out _libraryHandle))
                        return;
                    if (TryLoad($"{searchDir}lib{fileName}", out _libraryHandle))
                        return;
                    if (TryLoad($"{searchDir}lib{fileName}{fileExtension}", out _libraryHandle))
                        return;
                    if (!fileName.StartsWith("lib") || fileName == "lib")
                        continue;
                    string unprefixed = fileName.Substring(4);
                    if (TryLoad($"{searchDir}{unprefixed}", out _libraryHandle))
                        return;
                    if (TryLoad($"{searchDir}{unprefixed}{fileExtension}", out _libraryHandle))
                        return;
                }

#if NET7_0_OR_GREATER
                _libraryHandle = System.Runtime.InteropServices.NativeLibrary.GetMainProgramHandle();
#else
                if (IsLinux)
                    _libraryHandle = LoadLibraryLinux(null, 0x101);
                else if (IsOsx)
                    _libraryHandle = LoadLibraryOsx(null, 0x101);
                else if (IsWindows)
                    _libraryHandle = GetModuleHandle(null);
#endif
            }

            public static void LoadDllSymbol(string variableSymbol, out void* field)
            {
                if (_libraryHandle == System.IntPtr.Zero)
                    ResolveLibrary();
                field = (void*)GetExport(variableSymbol);
            }
        }
    }
}