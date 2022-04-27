using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.UIElements;
using InputManager;
using UnityEngine.SceneManagement;

public class StatsReport : MonoBehaviour
{
	float deltaTime = 0.0f;

	[Range(1, 60)]
	public int frameRate = 31;
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

		QualitySettings.vSyncCount = 2;
		Application.targetFrameRate = frameRate;

		Screen.SetResolution(720, 408, true);

		if (Control.ButtonDown(InputManager.Button.UP_DPAD))
        {
			SceneManager.LoadScene(0);
        }
	}


	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		Rect rect2 = new Rect(0, h*8/200, w, h * 2/200);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 50;
		style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		string text2 = string.Format("res {0:0} x {1:0}", w, h);
		GUI.Label(rect, text, style);
		GUI.Label(rect2, text2, style);
	}
}
