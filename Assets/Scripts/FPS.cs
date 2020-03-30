using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class FPS : MonoBehaviour
{
	public Text fpsText;
	float deltaTime = 0.0f;
 
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
 
	void OnGUI()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		//string text = string.Format("{1:0.0} FPS\n{0:0.0} MS", msec, fps);
		string text = string.Format("{1:0.0} FPS", msec, fps);
		fpsText.text = text;
	}
}