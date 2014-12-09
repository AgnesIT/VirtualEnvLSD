using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "Player" ) 
		{
			//do uzupełnienia
		}
	}
}
