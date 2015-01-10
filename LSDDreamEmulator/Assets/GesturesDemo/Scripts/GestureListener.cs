using UnityEngine;
using System.Collections;
using System;



public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	private bool swipeLeft;
	private bool swipeRight;
	private bool raiseLeftHand;
	private bool raiseRightHand;
	private bool swipeUp;
	private bool swipeDown;
	static bool isGoing=false;
	private Vector3 directionVector;
	public GameObject motor; 
	public void Update()
	{
		if (isGoing == true) {

			//rigidbody.MovePosition(transform.forward * (Time.deltaTime * 2));
			
			GameObject player = GameObject.Find("Player");
			CharacterController cc = player.GetComponent<CharacterController>();

			if(cc.isGrounded)
			{
				Vector3 v = transform.forward * (Time.deltaTime * 2);
				//cc.Move(v);
				//transform.position += v; //TODO
				Debug.Log ("Position: " + v);

				Vector3 forward = transform.TransformDirection(Vector3.forward);
				//float curSpeed = speed * Input.GetAxis ("Vertical");
				float curSpeed = 2;
				cc.SimpleMove(forward * curSpeed);
			}

				}
	}

	public bool IsSwipeLeft()
	{
		if(swipeLeft)
		{
			swipeLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeRight()
	{
		if(swipeRight)
		{
			swipeRight = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeUp()
	{
		if(raiseLeftHand)
		{
			raiseLeftHand = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeDown()
	{
		if(raiseRightHand)
		{
			raiseRightHand = false;
			return true;
		}
		
		return false;
	}
	

	public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		Debug.Log ("Wykrylem usera");
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
		//manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
		//manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
		manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
//		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
//		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);
		
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = "Swipe left or right to change the slides.";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
		float progress, KinectWrapper.SkeletonJoint joint, Vector3 screenPos)
	{
		// don't do anything here
	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
		KinectWrapper.SkeletonJoint joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " detected";
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = sGestureText;
		}
		Debug.Log ("wykrylem ruch" + gesture);
		if (gesture == KinectGestures.Gestures.SwipeLeft)
						transform.Rotate (0, 45, 0, Space.World);
				else if (gesture == KinectGestures.Gestures.SwipeRight)
						transform.Rotate (0, -45, 0, Space.World);
				else if (gesture == KinectGestures.Gestures.RaiseLeftHand)
						transform.Rotate (0, 5, 0, Space.World);
				else if (gesture == KinectGestures.Gestures.RaiseRightHand)
						transform.Rotate (0, -5, 0, Space.World);
				else if (gesture == KinectGestures.Gestures.Jump) {

			if (isGoing == false)
			{

				isGoing = true;

			}
			else
			{
				isGoing = false;
			}

				}

		
		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
		KinectWrapper.SkeletonJoint joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}
	
}
