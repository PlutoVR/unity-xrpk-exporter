using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Register : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string SetAlphaBlendingMode();


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        SetAlphaBlendingMode();
    }
}
