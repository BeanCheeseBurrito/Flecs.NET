#ifdef _WIN32
    #define BINDGEN_API __declspec(dllexport)
#else
    #define BINDGEN_API __attribute__((visibility("default")))
#endif
extern void* ECS_AUTO_OVERRIDE;
extern void* ecs_block_allocator_alloc_count;
extern void* ecs_block_allocator_free_count;
extern void* ecs_http_busy_count;
extern void* ecs_http_request_handled_error_count;
extern void* ecs_http_request_handled_ok_count;
extern void* ecs_http_request_invalid_count;
extern void* ecs_http_request_not_handled_count;
extern void* ecs_http_request_preflight_count;
extern void* ecs_http_request_received_count;
extern void* ecs_http_send_error_count;
extern void* ecs_http_send_ok_count;
extern void* ecs_os_api;
extern void* ecs_os_api_calloc_count;
extern void* ecs_os_api_free_count;
extern void* ecs_os_api_malloc_count;
extern void* ecs_os_api_realloc_count;
extern void* ECS_PAIR;
extern void* ecs_stack_allocator_alloc_count;
extern void* ecs_stack_allocator_free_count;
extern void* ECS_TOGGLE;
extern void* EcsAcceleration;
extern void* EcsAcyclic;
extern void* EcsAlertCritical;
extern void* EcsAlertError;
extern void* EcsAlertInfo;
extern void* EcsAlertWarning;
extern void* EcsAlias;
extern void* EcsAmount;
extern void* EcsAmpere;
extern void* EcsAngle;
extern void* EcsAny;
extern void* EcsAtto;
extern void* EcsBar;
extern void* EcsBel;
extern void* EcsBits;
extern void* EcsBitsPerSecond;
extern void* EcsBytes;
extern void* EcsBytesPerSecond;
extern void* EcsCandela;
extern void* EcsCanToggle;
extern void* EcsCelsius;
extern void* EcsCenti;
extern void* EcsCentiMeters;
extern void* EcsChildOf;
extern void* EcsColor;
extern void* EcsColorCss;
extern void* EcsColorHsl;
extern void* EcsColorRgb;
extern void* EcsConstant;
extern void* EcsCounter;
extern void* EcsCounterId;
extern void* EcsCounterIncrement;
extern void* EcsData;
extern void* EcsDataRate;
extern void* EcsDate;
extern void* EcsDays;
extern void* EcsDeca;
extern void* EcsDeci;
extern void* EcsDeciBel;
extern void* EcsDegrees;
extern void* EcsDelete;
extern void* EcsDependsOn;
extern void* EcsDisabled;
extern void* EcsDocBrief;
extern void* EcsDocColor;
extern void* EcsDocDetail;
extern void* EcsDocLink;
extern void* EcsDocUuid;
extern void* EcsDontInherit;
extern void* EcsDuration;
extern void* EcsElectricCurrent;
extern void* EcsEmpty;
extern void* EcsExa;
extern void* EcsExbi;
extern void* EcsExclusive;
extern void* EcsFahrenheit;
extern void* EcsFemto;
extern void* EcsFinal;
extern void* EcsFlecs;
extern void* EcsFlecsCore;
extern void* EcsForce;
extern void* EcsFrequency;
extern void* EcsGauge;
extern void* EcsGibi;
extern void* EcsGibiBytes;
extern void* EcsGiga;
extern void* EcsGigaBits;
extern void* EcsGigaBitsPerSecond;
extern void* EcsGigaBytes;
extern void* EcsGigaBytesPerSecond;
extern void* EcsGigaHertz;
extern void* EcsGrams;
extern void* EcsHecto;
extern void* EcsHertz;
extern void* EcsHours;
extern void* EcsInherit;
extern void* EcsInheritable;
extern void* EcsIsA;
extern void* EcsKelvin;
extern void* EcsKibi;
extern void* EcsKibiBytes;
extern void* EcsKilo;
extern void* EcsKiloBits;
extern void* EcsKiloBitsPerSecond;
extern void* EcsKiloBytes;
extern void* EcsKiloBytesPerSecond;
extern void* EcsKiloGrams;
extern void* EcsKiloHertz;
extern void* EcsKiloMeters;
extern void* EcsKiloMetersPerHour;
extern void* EcsKiloMetersPerSecond;
extern void* EcsLength;
extern void* EcsLuminousIntensity;
extern void* EcsMass;
extern void* EcsMebi;
extern void* EcsMebiBytes;
extern void* EcsMega;
extern void* EcsMegaBits;
extern void* EcsMegaBitsPerSecond;
extern void* EcsMegaBytes;
extern void* EcsMegaBytesPerSecond;
extern void* EcsMegaHertz;
extern void* EcsMeters;
extern void* EcsMetersPerSecond;
extern void* EcsMetric;
extern void* EcsMetricInstance;
extern void* EcsMicro;
extern void* EcsMicroMeters;
extern void* EcsMicroSeconds;
extern void* EcsMiles;
extern void* EcsMilesPerHour;
extern void* EcsMilli;
extern void* EcsMilliMeters;
extern void* EcsMilliSeconds;
extern void* EcsMinutes;
extern void* EcsModule;
extern void* EcsMole;
extern void* EcsMonitor;
extern void* EcsName;
extern void* EcsNano;
extern void* EcsNanoMeters;
extern void* EcsNanoSeconds;
extern void* EcsNewton;
extern void* EcsNotQueryable;
extern void* EcsObserver;
extern void* EcsOnAdd;
extern void* EcsOnDelete;
extern void* EcsOnDeleteTarget;
extern void* EcsOneOf;
extern void* EcsOnInstantiate;
extern void* EcsOnLoad;
extern void* EcsOnRemove;
extern void* EcsOnSet;
extern void* EcsOnStart;
extern void* EcsOnStore;
extern void* EcsOnTableCreate;
extern void* EcsOnTableDelete;
extern void* EcsOnUpdate;
extern void* EcsOnValidate;
extern void* EcsOverride;
extern void* EcsPairIsTag;
extern void* EcsPanic;
extern void* EcsPascal;
extern void* EcsPebi;
extern void* EcsPercentage;
extern void* EcsPeriod1d;
extern void* EcsPeriod1h;
extern void* EcsPeriod1m;
extern void* EcsPeriod1s;
extern void* EcsPeriod1w;
extern void* EcsPeta;
extern void* EcsPhase;
extern void* EcsPico;
extern void* EcsPicoMeters;
extern void* EcsPicoSeconds;
extern void* EcsPixels;
extern void* EcsPostFrame;
extern void* EcsPostLoad;
extern void* EcsPostUpdate;
extern void* EcsPredEq;
extern void* EcsPredLookup;
extern void* EcsPredMatch;
extern void* EcsPrefab;
extern void* EcsPreFrame;
extern void* EcsPressure;
extern void* EcsPreStore;
extern void* EcsPreUpdate;
extern void* EcsPrivate;
extern void* EcsQuantity;
extern void* EcsQuery;
extern void* EcsRadians;
extern void* EcsReflexive;
extern void* EcsRelationship;
extern void* EcsRemove;
extern void* EcsScopeClose;
extern void* EcsScopeOpen;
extern void* EcsScriptTemplate;
extern void* EcsSeconds;
extern void* EcsSlotOf;
extern void* EcsSparse;
extern void* EcsSpeed;
extern void* EcsSymbol;
extern void* EcsSymmetric;
extern void* EcsSystem;
extern void* EcsTarget;
extern void* EcsTebi;
extern void* EcsTemperature;
extern void* EcsTera;
extern void* EcsThis;
extern void* EcsTime;
extern void* EcsTrait;
extern void* EcsTransitive;
extern void* EcsTraversable;
extern void* EcsUnion;
extern void* EcsUnitPrefixes;
extern void* EcsUri;
extern void* EcsUriFile;
extern void* EcsUriHyperlink;
extern void* EcsUriImage;
extern void* EcsVariable;
extern void* EcsWildcard;
extern void* EcsWith;
extern void* EcsWorld;
extern void* EcsYobi;
extern void* EcsYocto;
extern void* EcsYotta;
extern void* EcsZebi;
extern void* EcsZepto;
extern void* EcsZetta;
extern void* FLECS_IDecs_bool_tID_;
extern void* FLECS_IDecs_byte_tID_;
extern void* FLECS_IDecs_char_tID_;
extern void* FLECS_IDecs_entity_tID_;
extern void* FLECS_IDecs_f32_tID_;
extern void* FLECS_IDecs_f64_tID_;
extern void* FLECS_IDecs_i16_tID_;
extern void* FLECS_IDecs_i32_tID_;
extern void* FLECS_IDecs_i64_tID_;
extern void* FLECS_IDecs_i8_tID_;
extern void* FLECS_IDecs_id_tID_;
extern void* FLECS_IDecs_iptr_tID_;
extern void* FLECS_IDecs_string_tID_;
extern void* FLECS_IDecs_u16_tID_;
extern void* FLECS_IDecs_u32_tID_;
extern void* FLECS_IDecs_u64_tID_;
extern void* FLECS_IDecs_u8_tID_;
extern void* FLECS_IDecs_uptr_tID_;
extern void* FLECS_IDEcsAlertCriticalID_;
extern void* FLECS_IDEcsAlertErrorID_;
extern void* FLECS_IDEcsAlertID_;
extern void* FLECS_IDEcsAlertInfoID_;
extern void* FLECS_IDEcsAlertInstanceID_;
extern void* FLECS_IDEcsAlertsActiveID_;
extern void* FLECS_IDEcsAlertTimeoutID_;
extern void* FLECS_IDEcsAlertWarningID_;
extern void* FLECS_IDEcsArrayID_;
extern void* FLECS_IDEcsBitmaskID_;
extern void* FLECS_IDEcsComponentID_;
extern void* FLECS_IDEcsCounterID_;
extern void* FLECS_IDEcsCounterIdID_;
extern void* FLECS_IDEcsCounterIncrementID_;
extern void* FLECS_IDEcsDefaultChildComponentID_;
extern void* FLECS_IDEcsDocDescriptionID_;
extern void* FLECS_IDEcsEnumID_;
extern void* FLECS_IDEcsGaugeID_;
extern void* FLECS_IDEcsIdentifierID_;
extern void* FLECS_IDEcsMemberID_;
extern void* FLECS_IDEcsMemberRangesID_;
extern void* FLECS_IDEcsMetricID_;
extern void* FLECS_IDEcsMetricInstanceID_;
extern void* FLECS_IDEcsMetricSourceID_;
extern void* FLECS_IDEcsMetricValueID_;
extern void* FLECS_IDEcsOpaqueID_;
extern void* FLECS_IDEcsPipelineID_;
extern void* FLECS_IDEcsPipelineStatsID_;
extern void* FLECS_IDEcsPolyID_;
extern void* FLECS_IDEcsPrimitiveID_;
extern void* FLECS_IDEcsRateFilterID_;
extern void* FLECS_IDEcsRestID_;
extern void* FLECS_IDEcsScriptConstVarID_;
extern void* FLECS_IDEcsScriptFunctionID_;
extern void* FLECS_IDEcsScriptID_;
extern void* FLECS_IDEcsScriptMethodID_;
extern void* FLECS_IDEcsScriptTemplateID_;
extern void* FLECS_IDEcsStructID_;
extern void* FLECS_IDEcsSystemStatsID_;
extern void* FLECS_IDEcsTickSourceID_;
extern void* FLECS_IDEcsTimerID_;
extern void* FLECS_IDEcsTypeID_;
extern void* FLECS_IDEcsTypeSerializerID_;
extern void* FLECS_IDEcsUnitID_;
extern void* FLECS_IDEcsUnitPrefixID_;
extern void* FLECS_IDEcsVectorID_;
extern void* FLECS_IDEcsWorldStatsID_;
extern void* FLECS_IDEcsWorldSummaryID_;
extern void* FLECS_IDFlecsAlertsID_;
extern void* FLECS_IDFlecsMetricsID_;
extern void* FLECS_IDFlecsStatsID_;
BINDGEN_API void* ECS_AUTO_OVERRIDE_BindgenGetExtern() {
    return &ECS_AUTO_OVERRIDE;
}
BINDGEN_API void* ecs_block_allocator_alloc_count_BindgenGetExtern() {
    return &ecs_block_allocator_alloc_count;
}
BINDGEN_API void* ecs_block_allocator_free_count_BindgenGetExtern() {
    return &ecs_block_allocator_free_count;
}
BINDGEN_API void* ecs_http_busy_count_BindgenGetExtern() {
    return &ecs_http_busy_count;
}
BINDGEN_API void* ecs_http_request_handled_error_count_BindgenGetExtern() {
    return &ecs_http_request_handled_error_count;
}
BINDGEN_API void* ecs_http_request_handled_ok_count_BindgenGetExtern() {
    return &ecs_http_request_handled_ok_count;
}
BINDGEN_API void* ecs_http_request_invalid_count_BindgenGetExtern() {
    return &ecs_http_request_invalid_count;
}
BINDGEN_API void* ecs_http_request_not_handled_count_BindgenGetExtern() {
    return &ecs_http_request_not_handled_count;
}
BINDGEN_API void* ecs_http_request_preflight_count_BindgenGetExtern() {
    return &ecs_http_request_preflight_count;
}
BINDGEN_API void* ecs_http_request_received_count_BindgenGetExtern() {
    return &ecs_http_request_received_count;
}
BINDGEN_API void* ecs_http_send_error_count_BindgenGetExtern() {
    return &ecs_http_send_error_count;
}
BINDGEN_API void* ecs_http_send_ok_count_BindgenGetExtern() {
    return &ecs_http_send_ok_count;
}
BINDGEN_API void* ecs_os_api_BindgenGetExtern() {
    return &ecs_os_api;
}
BINDGEN_API void* ecs_os_api_calloc_count_BindgenGetExtern() {
    return &ecs_os_api_calloc_count;
}
BINDGEN_API void* ecs_os_api_free_count_BindgenGetExtern() {
    return &ecs_os_api_free_count;
}
BINDGEN_API void* ecs_os_api_malloc_count_BindgenGetExtern() {
    return &ecs_os_api_malloc_count;
}
BINDGEN_API void* ecs_os_api_realloc_count_BindgenGetExtern() {
    return &ecs_os_api_realloc_count;
}
BINDGEN_API void* ECS_PAIR_BindgenGetExtern() {
    return &ECS_PAIR;
}
BINDGEN_API void* ecs_stack_allocator_alloc_count_BindgenGetExtern() {
    return &ecs_stack_allocator_alloc_count;
}
BINDGEN_API void* ecs_stack_allocator_free_count_BindgenGetExtern() {
    return &ecs_stack_allocator_free_count;
}
BINDGEN_API void* ECS_TOGGLE_BindgenGetExtern() {
    return &ECS_TOGGLE;
}
BINDGEN_API void* EcsAcceleration_BindgenGetExtern() {
    return &EcsAcceleration;
}
BINDGEN_API void* EcsAcyclic_BindgenGetExtern() {
    return &EcsAcyclic;
}
BINDGEN_API void* EcsAlertCritical_BindgenGetExtern() {
    return &EcsAlertCritical;
}
BINDGEN_API void* EcsAlertError_BindgenGetExtern() {
    return &EcsAlertError;
}
BINDGEN_API void* EcsAlertInfo_BindgenGetExtern() {
    return &EcsAlertInfo;
}
BINDGEN_API void* EcsAlertWarning_BindgenGetExtern() {
    return &EcsAlertWarning;
}
BINDGEN_API void* EcsAlias_BindgenGetExtern() {
    return &EcsAlias;
}
BINDGEN_API void* EcsAmount_BindgenGetExtern() {
    return &EcsAmount;
}
BINDGEN_API void* EcsAmpere_BindgenGetExtern() {
    return &EcsAmpere;
}
BINDGEN_API void* EcsAngle_BindgenGetExtern() {
    return &EcsAngle;
}
BINDGEN_API void* EcsAny_BindgenGetExtern() {
    return &EcsAny;
}
BINDGEN_API void* EcsAtto_BindgenGetExtern() {
    return &EcsAtto;
}
BINDGEN_API void* EcsBar_BindgenGetExtern() {
    return &EcsBar;
}
BINDGEN_API void* EcsBel_BindgenGetExtern() {
    return &EcsBel;
}
BINDGEN_API void* EcsBits_BindgenGetExtern() {
    return &EcsBits;
}
BINDGEN_API void* EcsBitsPerSecond_BindgenGetExtern() {
    return &EcsBitsPerSecond;
}
BINDGEN_API void* EcsBytes_BindgenGetExtern() {
    return &EcsBytes;
}
BINDGEN_API void* EcsBytesPerSecond_BindgenGetExtern() {
    return &EcsBytesPerSecond;
}
BINDGEN_API void* EcsCandela_BindgenGetExtern() {
    return &EcsCandela;
}
BINDGEN_API void* EcsCanToggle_BindgenGetExtern() {
    return &EcsCanToggle;
}
BINDGEN_API void* EcsCelsius_BindgenGetExtern() {
    return &EcsCelsius;
}
BINDGEN_API void* EcsCenti_BindgenGetExtern() {
    return &EcsCenti;
}
BINDGEN_API void* EcsCentiMeters_BindgenGetExtern() {
    return &EcsCentiMeters;
}
BINDGEN_API void* EcsChildOf_BindgenGetExtern() {
    return &EcsChildOf;
}
BINDGEN_API void* EcsColor_BindgenGetExtern() {
    return &EcsColor;
}
BINDGEN_API void* EcsColorCss_BindgenGetExtern() {
    return &EcsColorCss;
}
BINDGEN_API void* EcsColorHsl_BindgenGetExtern() {
    return &EcsColorHsl;
}
BINDGEN_API void* EcsColorRgb_BindgenGetExtern() {
    return &EcsColorRgb;
}
BINDGEN_API void* EcsConstant_BindgenGetExtern() {
    return &EcsConstant;
}
BINDGEN_API void* EcsCounter_BindgenGetExtern() {
    return &EcsCounter;
}
BINDGEN_API void* EcsCounterId_BindgenGetExtern() {
    return &EcsCounterId;
}
BINDGEN_API void* EcsCounterIncrement_BindgenGetExtern() {
    return &EcsCounterIncrement;
}
BINDGEN_API void* EcsData_BindgenGetExtern() {
    return &EcsData;
}
BINDGEN_API void* EcsDataRate_BindgenGetExtern() {
    return &EcsDataRate;
}
BINDGEN_API void* EcsDate_BindgenGetExtern() {
    return &EcsDate;
}
BINDGEN_API void* EcsDays_BindgenGetExtern() {
    return &EcsDays;
}
BINDGEN_API void* EcsDeca_BindgenGetExtern() {
    return &EcsDeca;
}
BINDGEN_API void* EcsDeci_BindgenGetExtern() {
    return &EcsDeci;
}
BINDGEN_API void* EcsDeciBel_BindgenGetExtern() {
    return &EcsDeciBel;
}
BINDGEN_API void* EcsDegrees_BindgenGetExtern() {
    return &EcsDegrees;
}
BINDGEN_API void* EcsDelete_BindgenGetExtern() {
    return &EcsDelete;
}
BINDGEN_API void* EcsDependsOn_BindgenGetExtern() {
    return &EcsDependsOn;
}
BINDGEN_API void* EcsDisabled_BindgenGetExtern() {
    return &EcsDisabled;
}
BINDGEN_API void* EcsDocBrief_BindgenGetExtern() {
    return &EcsDocBrief;
}
BINDGEN_API void* EcsDocColor_BindgenGetExtern() {
    return &EcsDocColor;
}
BINDGEN_API void* EcsDocDetail_BindgenGetExtern() {
    return &EcsDocDetail;
}
BINDGEN_API void* EcsDocLink_BindgenGetExtern() {
    return &EcsDocLink;
}
BINDGEN_API void* EcsDocUuid_BindgenGetExtern() {
    return &EcsDocUuid;
}
BINDGEN_API void* EcsDontInherit_BindgenGetExtern() {
    return &EcsDontInherit;
}
BINDGEN_API void* EcsDuration_BindgenGetExtern() {
    return &EcsDuration;
}
BINDGEN_API void* EcsElectricCurrent_BindgenGetExtern() {
    return &EcsElectricCurrent;
}
BINDGEN_API void* EcsEmpty_BindgenGetExtern() {
    return &EcsEmpty;
}
BINDGEN_API void* EcsExa_BindgenGetExtern() {
    return &EcsExa;
}
BINDGEN_API void* EcsExbi_BindgenGetExtern() {
    return &EcsExbi;
}
BINDGEN_API void* EcsExclusive_BindgenGetExtern() {
    return &EcsExclusive;
}
BINDGEN_API void* EcsFahrenheit_BindgenGetExtern() {
    return &EcsFahrenheit;
}
BINDGEN_API void* EcsFemto_BindgenGetExtern() {
    return &EcsFemto;
}
BINDGEN_API void* EcsFinal_BindgenGetExtern() {
    return &EcsFinal;
}
BINDGEN_API void* EcsFlecs_BindgenGetExtern() {
    return &EcsFlecs;
}
BINDGEN_API void* EcsFlecsCore_BindgenGetExtern() {
    return &EcsFlecsCore;
}
BINDGEN_API void* EcsForce_BindgenGetExtern() {
    return &EcsForce;
}
BINDGEN_API void* EcsFrequency_BindgenGetExtern() {
    return &EcsFrequency;
}
BINDGEN_API void* EcsGauge_BindgenGetExtern() {
    return &EcsGauge;
}
BINDGEN_API void* EcsGibi_BindgenGetExtern() {
    return &EcsGibi;
}
BINDGEN_API void* EcsGibiBytes_BindgenGetExtern() {
    return &EcsGibiBytes;
}
BINDGEN_API void* EcsGiga_BindgenGetExtern() {
    return &EcsGiga;
}
BINDGEN_API void* EcsGigaBits_BindgenGetExtern() {
    return &EcsGigaBits;
}
BINDGEN_API void* EcsGigaBitsPerSecond_BindgenGetExtern() {
    return &EcsGigaBitsPerSecond;
}
BINDGEN_API void* EcsGigaBytes_BindgenGetExtern() {
    return &EcsGigaBytes;
}
BINDGEN_API void* EcsGigaBytesPerSecond_BindgenGetExtern() {
    return &EcsGigaBytesPerSecond;
}
BINDGEN_API void* EcsGigaHertz_BindgenGetExtern() {
    return &EcsGigaHertz;
}
BINDGEN_API void* EcsGrams_BindgenGetExtern() {
    return &EcsGrams;
}
BINDGEN_API void* EcsHecto_BindgenGetExtern() {
    return &EcsHecto;
}
BINDGEN_API void* EcsHertz_BindgenGetExtern() {
    return &EcsHertz;
}
BINDGEN_API void* EcsHours_BindgenGetExtern() {
    return &EcsHours;
}
BINDGEN_API void* EcsInherit_BindgenGetExtern() {
    return &EcsInherit;
}
BINDGEN_API void* EcsInheritable_BindgenGetExtern() {
    return &EcsInheritable;
}
BINDGEN_API void* EcsIsA_BindgenGetExtern() {
    return &EcsIsA;
}
BINDGEN_API void* EcsKelvin_BindgenGetExtern() {
    return &EcsKelvin;
}
BINDGEN_API void* EcsKibi_BindgenGetExtern() {
    return &EcsKibi;
}
BINDGEN_API void* EcsKibiBytes_BindgenGetExtern() {
    return &EcsKibiBytes;
}
BINDGEN_API void* EcsKilo_BindgenGetExtern() {
    return &EcsKilo;
}
BINDGEN_API void* EcsKiloBits_BindgenGetExtern() {
    return &EcsKiloBits;
}
BINDGEN_API void* EcsKiloBitsPerSecond_BindgenGetExtern() {
    return &EcsKiloBitsPerSecond;
}
BINDGEN_API void* EcsKiloBytes_BindgenGetExtern() {
    return &EcsKiloBytes;
}
BINDGEN_API void* EcsKiloBytesPerSecond_BindgenGetExtern() {
    return &EcsKiloBytesPerSecond;
}
BINDGEN_API void* EcsKiloGrams_BindgenGetExtern() {
    return &EcsKiloGrams;
}
BINDGEN_API void* EcsKiloHertz_BindgenGetExtern() {
    return &EcsKiloHertz;
}
BINDGEN_API void* EcsKiloMeters_BindgenGetExtern() {
    return &EcsKiloMeters;
}
BINDGEN_API void* EcsKiloMetersPerHour_BindgenGetExtern() {
    return &EcsKiloMetersPerHour;
}
BINDGEN_API void* EcsKiloMetersPerSecond_BindgenGetExtern() {
    return &EcsKiloMetersPerSecond;
}
BINDGEN_API void* EcsLength_BindgenGetExtern() {
    return &EcsLength;
}
BINDGEN_API void* EcsLuminousIntensity_BindgenGetExtern() {
    return &EcsLuminousIntensity;
}
BINDGEN_API void* EcsMass_BindgenGetExtern() {
    return &EcsMass;
}
BINDGEN_API void* EcsMebi_BindgenGetExtern() {
    return &EcsMebi;
}
BINDGEN_API void* EcsMebiBytes_BindgenGetExtern() {
    return &EcsMebiBytes;
}
BINDGEN_API void* EcsMega_BindgenGetExtern() {
    return &EcsMega;
}
BINDGEN_API void* EcsMegaBits_BindgenGetExtern() {
    return &EcsMegaBits;
}
BINDGEN_API void* EcsMegaBitsPerSecond_BindgenGetExtern() {
    return &EcsMegaBitsPerSecond;
}
BINDGEN_API void* EcsMegaBytes_BindgenGetExtern() {
    return &EcsMegaBytes;
}
BINDGEN_API void* EcsMegaBytesPerSecond_BindgenGetExtern() {
    return &EcsMegaBytesPerSecond;
}
BINDGEN_API void* EcsMegaHertz_BindgenGetExtern() {
    return &EcsMegaHertz;
}
BINDGEN_API void* EcsMeters_BindgenGetExtern() {
    return &EcsMeters;
}
BINDGEN_API void* EcsMetersPerSecond_BindgenGetExtern() {
    return &EcsMetersPerSecond;
}
BINDGEN_API void* EcsMetric_BindgenGetExtern() {
    return &EcsMetric;
}
BINDGEN_API void* EcsMetricInstance_BindgenGetExtern() {
    return &EcsMetricInstance;
}
BINDGEN_API void* EcsMicro_BindgenGetExtern() {
    return &EcsMicro;
}
BINDGEN_API void* EcsMicroMeters_BindgenGetExtern() {
    return &EcsMicroMeters;
}
BINDGEN_API void* EcsMicroSeconds_BindgenGetExtern() {
    return &EcsMicroSeconds;
}
BINDGEN_API void* EcsMiles_BindgenGetExtern() {
    return &EcsMiles;
}
BINDGEN_API void* EcsMilesPerHour_BindgenGetExtern() {
    return &EcsMilesPerHour;
}
BINDGEN_API void* EcsMilli_BindgenGetExtern() {
    return &EcsMilli;
}
BINDGEN_API void* EcsMilliMeters_BindgenGetExtern() {
    return &EcsMilliMeters;
}
BINDGEN_API void* EcsMilliSeconds_BindgenGetExtern() {
    return &EcsMilliSeconds;
}
BINDGEN_API void* EcsMinutes_BindgenGetExtern() {
    return &EcsMinutes;
}
BINDGEN_API void* EcsModule_BindgenGetExtern() {
    return &EcsModule;
}
BINDGEN_API void* EcsMole_BindgenGetExtern() {
    return &EcsMole;
}
BINDGEN_API void* EcsMonitor_BindgenGetExtern() {
    return &EcsMonitor;
}
BINDGEN_API void* EcsName_BindgenGetExtern() {
    return &EcsName;
}
BINDGEN_API void* EcsNano_BindgenGetExtern() {
    return &EcsNano;
}
BINDGEN_API void* EcsNanoMeters_BindgenGetExtern() {
    return &EcsNanoMeters;
}
BINDGEN_API void* EcsNanoSeconds_BindgenGetExtern() {
    return &EcsNanoSeconds;
}
BINDGEN_API void* EcsNewton_BindgenGetExtern() {
    return &EcsNewton;
}
BINDGEN_API void* EcsNotQueryable_BindgenGetExtern() {
    return &EcsNotQueryable;
}
BINDGEN_API void* EcsObserver_BindgenGetExtern() {
    return &EcsObserver;
}
BINDGEN_API void* EcsOnAdd_BindgenGetExtern() {
    return &EcsOnAdd;
}
BINDGEN_API void* EcsOnDelete_BindgenGetExtern() {
    return &EcsOnDelete;
}
BINDGEN_API void* EcsOnDeleteTarget_BindgenGetExtern() {
    return &EcsOnDeleteTarget;
}
BINDGEN_API void* EcsOneOf_BindgenGetExtern() {
    return &EcsOneOf;
}
BINDGEN_API void* EcsOnInstantiate_BindgenGetExtern() {
    return &EcsOnInstantiate;
}
BINDGEN_API void* EcsOnLoad_BindgenGetExtern() {
    return &EcsOnLoad;
}
BINDGEN_API void* EcsOnRemove_BindgenGetExtern() {
    return &EcsOnRemove;
}
BINDGEN_API void* EcsOnSet_BindgenGetExtern() {
    return &EcsOnSet;
}
BINDGEN_API void* EcsOnStart_BindgenGetExtern() {
    return &EcsOnStart;
}
BINDGEN_API void* EcsOnStore_BindgenGetExtern() {
    return &EcsOnStore;
}
BINDGEN_API void* EcsOnTableCreate_BindgenGetExtern() {
    return &EcsOnTableCreate;
}
BINDGEN_API void* EcsOnTableDelete_BindgenGetExtern() {
    return &EcsOnTableDelete;
}
BINDGEN_API void* EcsOnUpdate_BindgenGetExtern() {
    return &EcsOnUpdate;
}
BINDGEN_API void* EcsOnValidate_BindgenGetExtern() {
    return &EcsOnValidate;
}
BINDGEN_API void* EcsOverride_BindgenGetExtern() {
    return &EcsOverride;
}
BINDGEN_API void* EcsPairIsTag_BindgenGetExtern() {
    return &EcsPairIsTag;
}
BINDGEN_API void* EcsPanic_BindgenGetExtern() {
    return &EcsPanic;
}
BINDGEN_API void* EcsPascal_BindgenGetExtern() {
    return &EcsPascal;
}
BINDGEN_API void* EcsPebi_BindgenGetExtern() {
    return &EcsPebi;
}
BINDGEN_API void* EcsPercentage_BindgenGetExtern() {
    return &EcsPercentage;
}
BINDGEN_API void* EcsPeriod1d_BindgenGetExtern() {
    return &EcsPeriod1d;
}
BINDGEN_API void* EcsPeriod1h_BindgenGetExtern() {
    return &EcsPeriod1h;
}
BINDGEN_API void* EcsPeriod1m_BindgenGetExtern() {
    return &EcsPeriod1m;
}
BINDGEN_API void* EcsPeriod1s_BindgenGetExtern() {
    return &EcsPeriod1s;
}
BINDGEN_API void* EcsPeriod1w_BindgenGetExtern() {
    return &EcsPeriod1w;
}
BINDGEN_API void* EcsPeta_BindgenGetExtern() {
    return &EcsPeta;
}
BINDGEN_API void* EcsPhase_BindgenGetExtern() {
    return &EcsPhase;
}
BINDGEN_API void* EcsPico_BindgenGetExtern() {
    return &EcsPico;
}
BINDGEN_API void* EcsPicoMeters_BindgenGetExtern() {
    return &EcsPicoMeters;
}
BINDGEN_API void* EcsPicoSeconds_BindgenGetExtern() {
    return &EcsPicoSeconds;
}
BINDGEN_API void* EcsPixels_BindgenGetExtern() {
    return &EcsPixels;
}
BINDGEN_API void* EcsPostFrame_BindgenGetExtern() {
    return &EcsPostFrame;
}
BINDGEN_API void* EcsPostLoad_BindgenGetExtern() {
    return &EcsPostLoad;
}
BINDGEN_API void* EcsPostUpdate_BindgenGetExtern() {
    return &EcsPostUpdate;
}
BINDGEN_API void* EcsPredEq_BindgenGetExtern() {
    return &EcsPredEq;
}
BINDGEN_API void* EcsPredLookup_BindgenGetExtern() {
    return &EcsPredLookup;
}
BINDGEN_API void* EcsPredMatch_BindgenGetExtern() {
    return &EcsPredMatch;
}
BINDGEN_API void* EcsPrefab_BindgenGetExtern() {
    return &EcsPrefab;
}
BINDGEN_API void* EcsPreFrame_BindgenGetExtern() {
    return &EcsPreFrame;
}
BINDGEN_API void* EcsPressure_BindgenGetExtern() {
    return &EcsPressure;
}
BINDGEN_API void* EcsPreStore_BindgenGetExtern() {
    return &EcsPreStore;
}
BINDGEN_API void* EcsPreUpdate_BindgenGetExtern() {
    return &EcsPreUpdate;
}
BINDGEN_API void* EcsPrivate_BindgenGetExtern() {
    return &EcsPrivate;
}
BINDGEN_API void* EcsQuantity_BindgenGetExtern() {
    return &EcsQuantity;
}
BINDGEN_API void* EcsQuery_BindgenGetExtern() {
    return &EcsQuery;
}
BINDGEN_API void* EcsRadians_BindgenGetExtern() {
    return &EcsRadians;
}
BINDGEN_API void* EcsReflexive_BindgenGetExtern() {
    return &EcsReflexive;
}
BINDGEN_API void* EcsRelationship_BindgenGetExtern() {
    return &EcsRelationship;
}
BINDGEN_API void* EcsRemove_BindgenGetExtern() {
    return &EcsRemove;
}
BINDGEN_API void* EcsScopeClose_BindgenGetExtern() {
    return &EcsScopeClose;
}
BINDGEN_API void* EcsScopeOpen_BindgenGetExtern() {
    return &EcsScopeOpen;
}
BINDGEN_API void* EcsScriptTemplate_BindgenGetExtern() {
    return &EcsScriptTemplate;
}
BINDGEN_API void* EcsSeconds_BindgenGetExtern() {
    return &EcsSeconds;
}
BINDGEN_API void* EcsSlotOf_BindgenGetExtern() {
    return &EcsSlotOf;
}
BINDGEN_API void* EcsSparse_BindgenGetExtern() {
    return &EcsSparse;
}
BINDGEN_API void* EcsSpeed_BindgenGetExtern() {
    return &EcsSpeed;
}
BINDGEN_API void* EcsSymbol_BindgenGetExtern() {
    return &EcsSymbol;
}
BINDGEN_API void* EcsSymmetric_BindgenGetExtern() {
    return &EcsSymmetric;
}
BINDGEN_API void* EcsSystem_BindgenGetExtern() {
    return &EcsSystem;
}
BINDGEN_API void* EcsTarget_BindgenGetExtern() {
    return &EcsTarget;
}
BINDGEN_API void* EcsTebi_BindgenGetExtern() {
    return &EcsTebi;
}
BINDGEN_API void* EcsTemperature_BindgenGetExtern() {
    return &EcsTemperature;
}
BINDGEN_API void* EcsTera_BindgenGetExtern() {
    return &EcsTera;
}
BINDGEN_API void* EcsThis_BindgenGetExtern() {
    return &EcsThis;
}
BINDGEN_API void* EcsTime_BindgenGetExtern() {
    return &EcsTime;
}
BINDGEN_API void* EcsTrait_BindgenGetExtern() {
    return &EcsTrait;
}
BINDGEN_API void* EcsTransitive_BindgenGetExtern() {
    return &EcsTransitive;
}
BINDGEN_API void* EcsTraversable_BindgenGetExtern() {
    return &EcsTraversable;
}
BINDGEN_API void* EcsUnion_BindgenGetExtern() {
    return &EcsUnion;
}
BINDGEN_API void* EcsUnitPrefixes_BindgenGetExtern() {
    return &EcsUnitPrefixes;
}
BINDGEN_API void* EcsUri_BindgenGetExtern() {
    return &EcsUri;
}
BINDGEN_API void* EcsUriFile_BindgenGetExtern() {
    return &EcsUriFile;
}
BINDGEN_API void* EcsUriHyperlink_BindgenGetExtern() {
    return &EcsUriHyperlink;
}
BINDGEN_API void* EcsUriImage_BindgenGetExtern() {
    return &EcsUriImage;
}
BINDGEN_API void* EcsVariable_BindgenGetExtern() {
    return &EcsVariable;
}
BINDGEN_API void* EcsWildcard_BindgenGetExtern() {
    return &EcsWildcard;
}
BINDGEN_API void* EcsWith_BindgenGetExtern() {
    return &EcsWith;
}
BINDGEN_API void* EcsWorld_BindgenGetExtern() {
    return &EcsWorld;
}
BINDGEN_API void* EcsYobi_BindgenGetExtern() {
    return &EcsYobi;
}
BINDGEN_API void* EcsYocto_BindgenGetExtern() {
    return &EcsYocto;
}
BINDGEN_API void* EcsYotta_BindgenGetExtern() {
    return &EcsYotta;
}
BINDGEN_API void* EcsZebi_BindgenGetExtern() {
    return &EcsZebi;
}
BINDGEN_API void* EcsZepto_BindgenGetExtern() {
    return &EcsZepto;
}
BINDGEN_API void* EcsZetta_BindgenGetExtern() {
    return &EcsZetta;
}
BINDGEN_API void* FLECS_IDecs_bool_tID__BindgenGetExtern() {
    return &FLECS_IDecs_bool_tID_;
}
BINDGEN_API void* FLECS_IDecs_byte_tID__BindgenGetExtern() {
    return &FLECS_IDecs_byte_tID_;
}
BINDGEN_API void* FLECS_IDecs_char_tID__BindgenGetExtern() {
    return &FLECS_IDecs_char_tID_;
}
BINDGEN_API void* FLECS_IDecs_entity_tID__BindgenGetExtern() {
    return &FLECS_IDecs_entity_tID_;
}
BINDGEN_API void* FLECS_IDecs_f32_tID__BindgenGetExtern() {
    return &FLECS_IDecs_f32_tID_;
}
BINDGEN_API void* FLECS_IDecs_f64_tID__BindgenGetExtern() {
    return &FLECS_IDecs_f64_tID_;
}
BINDGEN_API void* FLECS_IDecs_i16_tID__BindgenGetExtern() {
    return &FLECS_IDecs_i16_tID_;
}
BINDGEN_API void* FLECS_IDecs_i32_tID__BindgenGetExtern() {
    return &FLECS_IDecs_i32_tID_;
}
BINDGEN_API void* FLECS_IDecs_i64_tID__BindgenGetExtern() {
    return &FLECS_IDecs_i64_tID_;
}
BINDGEN_API void* FLECS_IDecs_i8_tID__BindgenGetExtern() {
    return &FLECS_IDecs_i8_tID_;
}
BINDGEN_API void* FLECS_IDecs_id_tID__BindgenGetExtern() {
    return &FLECS_IDecs_id_tID_;
}
BINDGEN_API void* FLECS_IDecs_iptr_tID__BindgenGetExtern() {
    return &FLECS_IDecs_iptr_tID_;
}
BINDGEN_API void* FLECS_IDecs_string_tID__BindgenGetExtern() {
    return &FLECS_IDecs_string_tID_;
}
BINDGEN_API void* FLECS_IDecs_u16_tID__BindgenGetExtern() {
    return &FLECS_IDecs_u16_tID_;
}
BINDGEN_API void* FLECS_IDecs_u32_tID__BindgenGetExtern() {
    return &FLECS_IDecs_u32_tID_;
}
BINDGEN_API void* FLECS_IDecs_u64_tID__BindgenGetExtern() {
    return &FLECS_IDecs_u64_tID_;
}
BINDGEN_API void* FLECS_IDecs_u8_tID__BindgenGetExtern() {
    return &FLECS_IDecs_u8_tID_;
}
BINDGEN_API void* FLECS_IDecs_uptr_tID__BindgenGetExtern() {
    return &FLECS_IDecs_uptr_tID_;
}
BINDGEN_API void* FLECS_IDEcsAlertCriticalID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertCriticalID_;
}
BINDGEN_API void* FLECS_IDEcsAlertErrorID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertErrorID_;
}
BINDGEN_API void* FLECS_IDEcsAlertID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertID_;
}
BINDGEN_API void* FLECS_IDEcsAlertInfoID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertInfoID_;
}
BINDGEN_API void* FLECS_IDEcsAlertInstanceID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertInstanceID_;
}
BINDGEN_API void* FLECS_IDEcsAlertsActiveID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertsActiveID_;
}
BINDGEN_API void* FLECS_IDEcsAlertTimeoutID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertTimeoutID_;
}
BINDGEN_API void* FLECS_IDEcsAlertWarningID__BindgenGetExtern() {
    return &FLECS_IDEcsAlertWarningID_;
}
BINDGEN_API void* FLECS_IDEcsArrayID__BindgenGetExtern() {
    return &FLECS_IDEcsArrayID_;
}
BINDGEN_API void* FLECS_IDEcsBitmaskID__BindgenGetExtern() {
    return &FLECS_IDEcsBitmaskID_;
}
BINDGEN_API void* FLECS_IDEcsComponentID__BindgenGetExtern() {
    return &FLECS_IDEcsComponentID_;
}
BINDGEN_API void* FLECS_IDEcsCounterID__BindgenGetExtern() {
    return &FLECS_IDEcsCounterID_;
}
BINDGEN_API void* FLECS_IDEcsCounterIdID__BindgenGetExtern() {
    return &FLECS_IDEcsCounterIdID_;
}
BINDGEN_API void* FLECS_IDEcsCounterIncrementID__BindgenGetExtern() {
    return &FLECS_IDEcsCounterIncrementID_;
}
BINDGEN_API void* FLECS_IDEcsDefaultChildComponentID__BindgenGetExtern() {
    return &FLECS_IDEcsDefaultChildComponentID_;
}
BINDGEN_API void* FLECS_IDEcsDocDescriptionID__BindgenGetExtern() {
    return &FLECS_IDEcsDocDescriptionID_;
}
BINDGEN_API void* FLECS_IDEcsEnumID__BindgenGetExtern() {
    return &FLECS_IDEcsEnumID_;
}
BINDGEN_API void* FLECS_IDEcsGaugeID__BindgenGetExtern() {
    return &FLECS_IDEcsGaugeID_;
}
BINDGEN_API void* FLECS_IDEcsIdentifierID__BindgenGetExtern() {
    return &FLECS_IDEcsIdentifierID_;
}
BINDGEN_API void* FLECS_IDEcsMemberID__BindgenGetExtern() {
    return &FLECS_IDEcsMemberID_;
}
BINDGEN_API void* FLECS_IDEcsMemberRangesID__BindgenGetExtern() {
    return &FLECS_IDEcsMemberRangesID_;
}
BINDGEN_API void* FLECS_IDEcsMetricID__BindgenGetExtern() {
    return &FLECS_IDEcsMetricID_;
}
BINDGEN_API void* FLECS_IDEcsMetricInstanceID__BindgenGetExtern() {
    return &FLECS_IDEcsMetricInstanceID_;
}
BINDGEN_API void* FLECS_IDEcsMetricSourceID__BindgenGetExtern() {
    return &FLECS_IDEcsMetricSourceID_;
}
BINDGEN_API void* FLECS_IDEcsMetricValueID__BindgenGetExtern() {
    return &FLECS_IDEcsMetricValueID_;
}
BINDGEN_API void* FLECS_IDEcsOpaqueID__BindgenGetExtern() {
    return &FLECS_IDEcsOpaqueID_;
}
BINDGEN_API void* FLECS_IDEcsPipelineID__BindgenGetExtern() {
    return &FLECS_IDEcsPipelineID_;
}
BINDGEN_API void* FLECS_IDEcsPipelineStatsID__BindgenGetExtern() {
    return &FLECS_IDEcsPipelineStatsID_;
}
BINDGEN_API void* FLECS_IDEcsPolyID__BindgenGetExtern() {
    return &FLECS_IDEcsPolyID_;
}
BINDGEN_API void* FLECS_IDEcsPrimitiveID__BindgenGetExtern() {
    return &FLECS_IDEcsPrimitiveID_;
}
BINDGEN_API void* FLECS_IDEcsRateFilterID__BindgenGetExtern() {
    return &FLECS_IDEcsRateFilterID_;
}
BINDGEN_API void* FLECS_IDEcsRestID__BindgenGetExtern() {
    return &FLECS_IDEcsRestID_;
}
BINDGEN_API void* FLECS_IDEcsScriptConstVarID__BindgenGetExtern() {
    return &FLECS_IDEcsScriptConstVarID_;
}
BINDGEN_API void* FLECS_IDEcsScriptFunctionID__BindgenGetExtern() {
    return &FLECS_IDEcsScriptFunctionID_;
}
BINDGEN_API void* FLECS_IDEcsScriptID__BindgenGetExtern() {
    return &FLECS_IDEcsScriptID_;
}
BINDGEN_API void* FLECS_IDEcsScriptMethodID__BindgenGetExtern() {
    return &FLECS_IDEcsScriptMethodID_;
}
BINDGEN_API void* FLECS_IDEcsScriptTemplateID__BindgenGetExtern() {
    return &FLECS_IDEcsScriptTemplateID_;
}
BINDGEN_API void* FLECS_IDEcsStructID__BindgenGetExtern() {
    return &FLECS_IDEcsStructID_;
}
BINDGEN_API void* FLECS_IDEcsSystemStatsID__BindgenGetExtern() {
    return &FLECS_IDEcsSystemStatsID_;
}
BINDGEN_API void* FLECS_IDEcsTickSourceID__BindgenGetExtern() {
    return &FLECS_IDEcsTickSourceID_;
}
BINDGEN_API void* FLECS_IDEcsTimerID__BindgenGetExtern() {
    return &FLECS_IDEcsTimerID_;
}
BINDGEN_API void* FLECS_IDEcsTypeID__BindgenGetExtern() {
    return &FLECS_IDEcsTypeID_;
}
BINDGEN_API void* FLECS_IDEcsTypeSerializerID__BindgenGetExtern() {
    return &FLECS_IDEcsTypeSerializerID_;
}
BINDGEN_API void* FLECS_IDEcsUnitID__BindgenGetExtern() {
    return &FLECS_IDEcsUnitID_;
}
BINDGEN_API void* FLECS_IDEcsUnitPrefixID__BindgenGetExtern() {
    return &FLECS_IDEcsUnitPrefixID_;
}
BINDGEN_API void* FLECS_IDEcsVectorID__BindgenGetExtern() {
    return &FLECS_IDEcsVectorID_;
}
BINDGEN_API void* FLECS_IDEcsWorldStatsID__BindgenGetExtern() {
    return &FLECS_IDEcsWorldStatsID_;
}
BINDGEN_API void* FLECS_IDEcsWorldSummaryID__BindgenGetExtern() {
    return &FLECS_IDEcsWorldSummaryID_;
}
BINDGEN_API void* FLECS_IDFlecsAlertsID__BindgenGetExtern() {
    return &FLECS_IDFlecsAlertsID_;
}
BINDGEN_API void* FLECS_IDFlecsMetricsID__BindgenGetExtern() {
    return &FLECS_IDFlecsMetricsID_;
}
BINDGEN_API void* FLECS_IDFlecsStatsID__BindgenGetExtern() {
    return &FLECS_IDFlecsStatsID_;
}
