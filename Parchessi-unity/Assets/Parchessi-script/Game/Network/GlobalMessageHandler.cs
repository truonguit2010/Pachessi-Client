using UnityEngine;
using System.Collections;
using Parchessi;

public class GlobalMessageHandler : MonoBehaviour,IMessageHandler {
	
	const string TAG = "GlobalMessageHandler";
	
	void Start () {
	}
	void Update ()
	{
	
	}
	public void onConnected ()
	{
		Log.log(TAG, "onConnected");
		GlobalService.login();
	}

	public void onConnectFail ()
	{
		Log.log(TAG, "onConnectFail");
	}

	public void onDisconnect ()
	{
		Log.log(TAG, "onDisconnect");
	}

	public void onMessage (string m)
	{
		Log.log(TAG, "onMessage: " + m);
	}
}
