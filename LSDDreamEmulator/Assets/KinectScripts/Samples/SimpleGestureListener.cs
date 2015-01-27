using UnityEngine;
using System.Collections;
using System;

public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	public enum PlaymakerGestures
	{
		None = 0,
		RaiseRightHand = 1,
		RaiseLeftHand = 2,
		Psi = 3,
		Stop = 4,
		Wave = 5,
		//Click = 6,
		SwipeLeft = 7,
		SwipeRight = 8,
		SwipeUp = 9,
		SwipeDown = 10,
		//RightHandCursor = 11,
		//LeftHandCursor = 12,
		//ZoomOut = 13,
		//ZoomIn = 14,
		//Wheel = 15,
		Jump = 16,
		Squat = 17
	}
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	public bool userDetected=false;
	public Vector3 outputPosition;
	public KinectWrapper.NuiSkeletonPositionIndex joint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private GestureListener gestureListener;
	private KinectGestures.Gestures gesture;
	public PlaymakerGestures kinectGesure;

	public void Start()
	{

		Debug.Log ("Gesture Start");
		KinectManager kinectManager = KinectManager.Instance;
		if(!kinectManager || !kinectManager.IsInitialized() || !kinectManager.IsUserDetected())
			return;
		
		gestureListener = Camera.main.GetComponent<GestureListener>();
		gesture = (KinectGestures.Gestures) Enum.Parse(typeof(KinectGestures.Gestures), kinectGesure.ToString());
	}

	public void Update()
	{
		
	
		KinectManager manager = KinectManager.Instance;
		if(manager && manager.IsUserDetected())
		{


			uint userId = manager.GetPlayer1ID();
			Debug.Log ("userID "+userId);
			if(!manager.IsGestureDetected(userId, gesture))
			{
				Debug.Log ("Detect Gesture "+gesture);
				manager.DetectGesture(userId, gesture);

			}
			
			if(manager.IsGestureComplete(userId, gesture, true))
			{
				Debug.Log ("Wykryto "+gesture);
			}
			else
			{
				Debug.Log ("W trakcie "+gesture + "- "+manager.GetGestureProgress(userId, gesture));
			
			}
		}







	

	}
	
	public void UserDetected(uint userId, int userIndex)
	{
		Debug.Log ("User Detected!");
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
		manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
		manager.DetectGesture(userId, KinectGestures.Gestures.Push);
		manager.DetectGesture(userId, KinectGestures.Gestures.Pull);
		
//		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeUp);
//		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeDown);

		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = "SwipeLeft, SwipeRight, Jump, Push or Pull.";
			Debug.Log (GestureInfo);
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
		//GestureInfo.guiText.text = string.Format("{0} Progress: {1:F1}%", gesture, (progress * 100));
		if(gesture == KinectGestures.Gestures.Click && progress > 0.3f)
		{
			string sGestureText = string.Format ("{0} {1:F1}% complete", gesture, progress * 100);
			if(GestureInfo != null)
				GestureInfo.guiText.text = sGestureText;
			
			progressDisplayed = true;
		}		
		else if((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, zoom={1:F1}%", gesture, screenPos.z * 100);
			if(GestureInfo != null)
				GestureInfo.guiText.text = sGestureText;
			
			progressDisplayed = true;
		}
		else if(gesture == KinectGestures.Gestures.Wheel && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, angle={1:F1} deg", gesture, screenPos.z);
			if(GestureInfo != null)
				GestureInfo.guiText.text = sGestureText;
			
			progressDisplayed = true;
		}
	}

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
		KinectWrapper.SkeletonJoint joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " detected";
		Debug.Log(sGestureText);
		if(gesture == KinectGestures.Gestures.Click)
			sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
		
		if(GestureInfo != null)
			GestureInfo.guiText.text = sGestureText;
		
		progressDisplayed = false;
		
		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
		KinectWrapper.SkeletonJoint joint)
	{
		if(progressDisplayed)
		{
			// clear the progress info
			if(GestureInfo != null)
				GestureInfo.guiText.text = String.Empty;
			
			progressDisplayed = false;
		}
		
		return true;
	}
	
}
