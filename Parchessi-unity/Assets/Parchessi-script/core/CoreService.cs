using System;
using Parchessi;
using UnityEngine;

/// <summary>
/// Core service.
/// Quan li toan bo cac service connect mang.
/// </summary>
public class CoreService
{
	const String TAG = "CoreService";
#if UNITY_EDITOR || UNITY_STANDALONE
	MobileClient session;
#elif UNITY_ANDROID
	AndroidJavaObject androidSession;
#endif
	
	Server currentServer;
	private static CoreService _instance;
	
	public static CoreService Instance {
		get { return _instance ?? (_instance = new CoreService ()); }
	}
	
	public void connect (Server server, String messageHandlerObject)
	{	
		Log.log(TAG, "Connecting to " + server.ip + " - " + server.port);
#if UNITY_EDITOR || UNITY_STANDALONE
		if (session == null) {
		session = new MobileClient ();
		}
		session.connect (server, new GlobalMessageHandler ());
#elif UNITY_ANDROID
		if(androidSession == null) {
			androidSession = new AndroidJavaObject("vn.gg.core.MobileClient");
		}
		androidSession.Call("connect", new object[] {server.ip, server.port});
#endif
		setMessageHandlerObject(messageHandlerObject);
	}
	
	/// <summary>
	/// Sets the message handler object.
	/// </summary>
	/// <param name='messageHandlerObject'>
	/// Message handler object.
	/// </param>
	private void setMessageHandlerObject (String messageHandlerObject)
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		
#elif UNITY_ANDROID
		androidSession.Call("setMessageHandler", messageHandlerObject);
#endif		
	}
	
	public bool isConnected ()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		return false;
		#elif UNITY_ANDROID
		return androidSession.Call<bool>("isConnected");
#endif	
	}
	
	public void sendMessage (Message message)
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_ANDROID
		androidSession.Call("sendMessage", message.toJson());
#endif		
	}
	
	public void close ()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
#elif UNITY_ANDROID
		androidSession.Call("close");
#endif		
	}
}
