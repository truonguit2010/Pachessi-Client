using System;
using UnityEngine;

namespace Parchessi
{
	public class Log
	{
		public static void log (string TAG, string m)
		{
#if UNITY_EDITOR || UNITY_STANDALONE
			Debug.Log (TAG + " - " + m);
#elif UNITY_ANDROID
			AndroidJavaClass androidLog = new AndroidJavaClass("vn.gg.core.Log");
			androidLog.CallStatic("log", new object[] {TAG, m});
#endif
		}
	
		public static void log (string TAG, Exception e)
		{
#if UNITY_EDITOR || UNITY_STANDALONE
			Debug.LogError (TAG);
			Debug.LogException (e);
#elif UNITY_ANDROID
			AndroidJavaClass androidLog = new AndroidJavaClass("vn.gg.core.Log");
			androidLog.CallStatic("error", new object[] {TAG, e.StackTrace});
#endif
		}
	}
}

