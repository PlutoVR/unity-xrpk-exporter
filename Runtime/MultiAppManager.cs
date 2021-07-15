using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public static class MultiAppManager
{

    [DllImport("__Internal")]
    private static extern string InternalGetAppId();

    [DllImport("__Internal")]
    private static extern string InternalGetInitialPosition();

    [DllImport("__Internal")]
    private static extern string InternalGetModelLoader();

    public static string GetAppId()
    {
        return InternalGetAppId();
    }

    public static string GetInitialPosition()
    {
        return InternalGetInitialPosition();
    }

    public static string GetModelLoader()
    {
        return InternalGetModelLoader();
    }
}
