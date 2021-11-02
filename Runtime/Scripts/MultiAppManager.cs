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
	private static extern void MAMGetApps(string receiverName, string dataMethodName, string errorMethodName);

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

	public static void LaunchApp(string appUrl, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		MAMLaunchApp(appUrl, transformJSONFromElements(position, rotation, scale));
	}

	public static void GetApps(string receiverName, string dataMethodName, string errorMethodName)
	{
		MAMGetApps(receiverName, dataMethodName, errorMethodName);
	}

	public static void LaunchAppByNameId(string name, string id, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		MAMLaunchAppByNameId(name, id, transformJSONFromElements(position, rotation, scale));
	}

	private static string transformJSONFromElements(Vector3 position, Quaternion rotation, Vector3 scale)
	{
        return @$"{{
            ""position"": {{ ""x"":{position.x}, ""y"":{position.y}, ""z"":{-position.z} }},
            ""rotation"": {{ ""x"":{rotation.x}, ""y"":{rotation.y}, ""z"":{rotation.z}, ""w"":{rotation.w} }},
            ""scale"": {{ ""x"":{scale.x}, ""y"":{scale.y}, ""z"":{scale.z} }}
        }}";
	}
}
