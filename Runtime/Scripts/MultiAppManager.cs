using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public static class MultiAppManager
{

	[DllImport("__Internal")]
	private static extern string MAMGetAppId();

	[DllImport("__Internal")]
	private static extern string MAMGetInitialPosition();

	[DllImport("__Internal")]
	private static extern string MAMGetModelLoader();

	[DllImport("__Internal")]
	private static extern void MAMLaunchApp();

	public static string GetAppId()
	{
		return MAMGetAppId();
	}
	public static string GetInitialPosition()
	{
		return MAMGetInitialPosition();
	}

	public static string GetModelLoader()
	{
		return MAMGetModelLoader();
	}

	public static void LaunchApp(string appUrl, Transform transform)
	{
		MAMLaunchApp();
	}
}
