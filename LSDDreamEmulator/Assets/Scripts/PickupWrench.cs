using UnityEngine;
using System.Collections;

public class PickupWrench : MonoBehaviour {

	public AudioClip audioClip;

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player")
		{
			GameVariables.itemCount += 1;
			GameVariables.wrenchDisplayTime = 2;
			AudioSource.PlayClipAtPoint(audioClip,transform.position);
			Destroy(gameObject);
		}
	}
}
