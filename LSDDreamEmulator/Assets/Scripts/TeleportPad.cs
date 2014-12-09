using UnityEngine;
using System.Collections;

public class TeleportPad : MonoBehaviour {
	
	public int portalCode;
	float disabledTimer = 0; 
	public AudioClip audioClip;

	void Update()
	{
		if (disabledTimer > 0) 
		{
			disabledTimer -= Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "Player" && disabledTimer <= 0) 
		{
			audio.clip = audioClip;
			audio.Play();

			int destinationCode = 0;
			
			switch(portalCode)
			{
			case 1:
				destinationCode = 4;
				break;
			case 2:
				destinationCode = 6;
				break;
			case 3:
				destinationCode = 1;
				break;
			case 4:
				destinationCode = 3;
				break;
			case 5:
				destinationCode = 2;
				break;
			case 6:
				destinationCode = 5;
				break;
			}

			foreach(TeleportPad tp in FindObjectsOfType<TeleportPad>())
			{
				if(tp.portalCode == destinationCode)
				{
					tp.disabledTimer = 3; //po teleportacji portal będzie zamknięty przez 3 sekundy, żeby przechodzenie się nie zapętliło
					Vector3 newPosition = tp.gameObject.transform.position;
					newPosition.y += 10; //efekt spadania na docelowy portal
					collider.gameObject.transform.position = newPosition;
				}
			}

		}
	}
}
