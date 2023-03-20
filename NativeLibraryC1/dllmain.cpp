#include <windows.h>
#include "..\NativeLibraryA1\NativeLibraryA1.h"
#pragma comment(lib, "NativeLibraryA.lib")

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
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
    INT APIENTRY GetIntegerC()
    {
        return 0xc100 + GetIntegerA();
    }
}