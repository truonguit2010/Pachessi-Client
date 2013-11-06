using UnityEngine;
using System.Collections;

public class LoginScreen : MonoBehaviour
{
	
	
	// Use this for initialization
	void Start ()
	{
		Screen.orientation = ScreenOrientation.Landscape;
		string t = "{\"name\":\"Truong\",\"id\":9}";
		
		Test test = (Test)fastJSON.JSON.Instance.ToObject (t, typeof(Test));
		
		str = fastJSON.JSON.Instance.ToJSON (test);
		Debug.Log ("json: " + str);
	}
	
	string str = "";
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	string ip = "192.168.1.102";
	
	int currentY = 10;
	int currentX = 10;

	void OnGUI ()
	{
		ip = GUI.TextField (new Rect (currentX, currentY, LAF.TEXTFIELD_WIDTH, LAF.TEXTFIELD_HEIGHT), ip);
		//GUI.Box(new Rect(10,100, 500, 500), str);
		if (GUI.Button (new Rect (10, 100, 100, 40), "Connect")) {
			Debug.Log ("Button click");
			CoreService.Instance.connect (new Server(ip, 8888), "Network");
		}
		
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {

				Application.Quit ();

				return;

			}

		}
	}
}

[System.Serializable]
public class Test
{
	public string name { get; set; }

	public int id { get; set; }
}