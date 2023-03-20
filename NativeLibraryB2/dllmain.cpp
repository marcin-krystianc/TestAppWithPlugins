#include <windows.h>
#include "..\NativeLibraryA1\NativeLibraryA1.h"

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }

    return TRUE;
}

extern "C" {
__declspec(dllexport)
INT APIENTRY GetIntegerB()
{
    //HINSTANCE hinstLib = LoadLibrary(TEXT("D:\\workspace\\TestParquetSharpAlcPlugins\\NativeLibraryA2\\out\\Debug\\x64\\NativeLibraryA.dll"));
    HINSTANCE hinstLib = LoadLibrary(TEXT("NativeLibraryA.dll"));

    // If the handle is valid, try to get the function address.

    if (hinstLib != NULL)
    {
        typedef int (__cdecl *MYPROC)();
        MYPROC ProcAdd = (MYPROC)GetProcAddress(hinstLib, "GetIntegerA");

        // If the function address is valid, call the function.

        if (NULL != ProcAdd)
        {
            return 0xB200 + (ProcAdd)();
        }

        // Free the DLL module.
        //FreeLibrary(hinstLib);
    }

    return 0xB2;
}
}
