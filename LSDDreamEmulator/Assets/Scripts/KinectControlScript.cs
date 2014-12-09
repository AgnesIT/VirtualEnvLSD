using UnityEngine;
using System.Collections;



public class KinectControlScript : MonoBehaviour {
	private GestureListener gestureListener;
	// Use this for initialization
	void Start () {
		KinectManager kinectManager = KinectManager.Instance;
	

		gestureListener = Camera.main.GetComponent<GestureListener>();
		Debug.Log ("Zainicjowano listenera");
	}
	
	// Update is called once per frame
	void Update () {


		KinectManager kinectManager = KinectManager.Instance;
		if(!kinectManager || !kinectManager.IsInitialized() || !kinectManager.IsUserDetected())
			return;

	}
}
