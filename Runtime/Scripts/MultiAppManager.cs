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
	private static extern void MAMLaunchApp(string appUrl, string transform);

	[DllImport("__Internal")]
	private static extern void MAMGetApps();

	[DllImport("__Internal")]
	private static extern void MAMLaunchAppByNameId(string name, string id, string transform);

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

	public static void LaunchApp(string appUrl, string transform)
	{
		MAMLaunchApp(appUrl, transform);
	}

	public static void GetApps()
	{
		MAMGetApps();
	}

	public static void LaunchAppByNameId(string name, string id, string transform)
	{
		MAMLaunchAppByNameId(name, id, transform);
	}
}
