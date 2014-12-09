using UnityEngine;
using System.Collections;

public class RemoveFlames : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player" && GameVariables.itemCount == 6)
		{
			Destroy (gameObject);
		}
	}
}
