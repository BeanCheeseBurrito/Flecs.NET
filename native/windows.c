// Temporary fix for undefined symbol errors when statically linking on windows.
void __mingw_vsnprintf() { }
void __mingw_vfprintf() { }
void __isnanf() { }
void __fpclassifyf() { }
