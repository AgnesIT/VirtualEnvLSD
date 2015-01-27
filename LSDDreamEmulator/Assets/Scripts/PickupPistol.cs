using UnityEngine;
using System.Collections;

public class PickupPistol : MonoBehaviour {

	public AudioClip audioClip;

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player")
		{
			GameVariables.itemCount += 1;
			GameVariables.pistolDisplayTime = 2;
			AudioSource.PlayClipAtPoint(audioClip,transform.position);
			Destroy(gameObject);
		}
	}
}
