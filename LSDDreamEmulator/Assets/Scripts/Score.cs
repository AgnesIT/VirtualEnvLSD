using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	int currentScore = 0;

	public float offsetY = 10;
	public float sizeX = 150;
	public float sizeY = 23;
		
	// Update is called once per frame
	void Update () {
		currentScore = GameVariables.itemCount;
	}

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width - sizeX - offsetY, offsetY, sizeX, sizeY), "Collected items: " + currentScore + "/6");
	}
}
