using UnityEngine;
using System.Collections;

public class PickupDuck : MonoBehaviour {

	public AudioClip audioClip;

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player")
		{
			GameVariables.itemCount += 1;
			GameVariables.duckDisplayTime = 2;
			AudioSource.PlayClipAtPoint(audioClip,transform.position);
			Destroy(gameObject);
		}
	}
}
