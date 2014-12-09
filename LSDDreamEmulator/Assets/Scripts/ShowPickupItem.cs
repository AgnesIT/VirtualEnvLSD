using UnityEngine;
using System.Collections;

public class ShowPickupItem : MonoBehaviour {
	
	Rect rect;
	Texture texture;
	
	// Use this for initialization
	void Start () {
		float size = Screen.width * 0.1f;
		rect = new Rect(Screen.width/2 - size/2, Screen.height * 0.7f, size, size);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameVariables.cageDisplayTime > 0)
		{
			GameVariables.cageDisplayTime-=Time.deltaTime;
		}
		else if(GameVariables.duckDisplayTime > 0)
		{
			GameVariables.duckDisplayTime-=Time.deltaTime;
		}
		else if(GameVariables.helmetDisplayTime > 0)
		{
			GameVariables.helmetDisplayTime-=Time.deltaTime;
		}
		else if(GameVariables.horseDisplayTime > 0)
		{
			GameVariables.horseDisplayTime-=Time.deltaTime;
		}
		else if(GameVariables.pistolDisplayTime > 0)
		{
			GameVariables.pistolDisplayTime-=Time.deltaTime;
		}
		else if(GameVariables.wrenchDisplayTime > 0)
		{
			GameVariables.wrenchDisplayTime-=Time.deltaTime;
		}
	}
	
	void OnGUI()
	{
		if(GameVariables.cageDisplayTime > 0)
		{
			texture = Resources.Load("Textures/cage") as Texture;
			GUI.DrawTexture(rect,texture);
		}
		else if(GameVariables.duckDisplayTime > 0)
		{
			texture = Resources.Load("Textures/duck") as Texture;
			GUI.DrawTexture(rect,texture);
		}
		else if(GameVariables.helmetDisplayTime > 0)
		{
			texture = Resources.Load("Textures/helmet") as Texture;
			GUI.DrawTexture(rect,texture);
		}
		else if(GameVariables.horseDisplayTime > 0)
		{
			texture = Resources.Load("Textures/horse") as Texture;
			GUI.DrawTexture(rect,texture);
		}
		else if(GameVariables.pistolDisplayTime > 0)
		{
			texture = Resources.Load("Textures/pistol") as Texture;
			GUI.DrawTexture(rect,texture);
		}
		else if(GameVariables.wrenchDisplayTime > 0)
		{
			texture = Resources.Load("Textures/wrench") as Texture;
			GUI.DrawTexture(rect,texture);
		}
	}
}
