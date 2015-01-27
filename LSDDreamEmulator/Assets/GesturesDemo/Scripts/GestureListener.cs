		using UnityEngine;
		using System.Collections;
		using System;



		public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
		{
			// GUI Text to display the gesture messages.
			public GUIText GestureInfo;
			private Vector3 directionVector;
			public GameObject motor; 
			CharacterController cc;

			private const float BASE_SPEED = 0.5f;
			private const float BASE_TURNRATE = 0.5f;
			private const int TURNING_FRAMES = 180;
			private const float DEFAULT_JUMP_SPEED = 15.0f;

			private float curSpeed = BASE_SPEED;
			private float turnRate = 0;
			private int turnCounter = 0;
			private bool willJump = false;
			private bool hasPoweredUp = false;

			public void Update()
			{
				if (cc.isGrounded) {
						if (willJump) {
								willJump = false;
								Vector3 upward = transform.TransformDirection (Vector3.up);
								float jumpSpeed = DEFAULT_JUMP_SPEED;
					
								if (hasPoweredUp) {
										hasPoweredUp = false;
										jumpSpeed *= 2;
								}
					
								Vector3 newPos = cc.transform.position;
								newPos.y += jumpSpeed;
								cc.transform.position = newPos;				
						} else {
								if (turnCounter-- > 0) {
										transform.Rotate (0, turnRate, 0, Space.World);
								} else {
										Vector3 forward = transform.TransformDirection (Vector3.forward);
										cc.Move (forward * curSpeed * Time.deltaTime);										
								}
						}
				}
			}

			public void UserDetected(uint userId, int userIndex)
			{
				GameObject player = GameObject.Find ("Player");
				if (player != null) {
					Component comp = player.GetComponent<CharacterController> ();
					if(comp != null) {
						cc = (CharacterController) comp;
					}
				}

				// detect these user specific gestures
				KinectManager manager = KinectManager.Instance;
				
				manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
				manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
				manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
				manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
				manager.DetectGesture(userId, KinectGestures.Gestures.Psi);
				manager.DetectGesture(userId, KinectGestures.Gestures.Stop);
				manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
				manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
				manager.DetectGesture(userId, KinectGestures.Gestures.Push);
				manager.DetectGesture(userId, KinectGestures.Gestures.Pull);
				manager.DetectGesture(userId, KinectGestures.Gestures.ZoomIn);
				manager.DetectGesture(userId, KinectGestures.Gestures.ZoomOut);
			}
			
			public void UserLost(uint userId, int userIndex)
			{
			}

			public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
				float progress, KinectWrapper.SkeletonJoint joint, Vector3 screenPos)
			{
			}

			public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
				KinectWrapper.SkeletonJoint joint, Vector3 screenPos)
			{
			if (turnCounter > 0) {
				Debug.Log ("ignoring " + gesture);
				return true;
			}
			
			Debug.Log (gesture + " detected");

				switch (gesture) {
				case KinectGestures.Gestures.Stop:
				case KinectGestures.Gestures.Psi:
					hasPoweredUp = false;
					break;
				case KinectGestures.Gestures.Jump:
					willJump = true;
					break;
				case KinectGestures.Gestures.Squat:
					curSpeed = 0;
					hasPoweredUp = true;
					break;
				case KinectGestures.Gestures.SwipeRight:
				case KinectGestures.Gestures.RaiseLeftHand:
					turnRate = -BASE_TURNRATE;
					turnCounter = TURNING_FRAMES;
					hasPoweredUp = false;
					break;
				case KinectGestures.Gestures.SwipeLeft:
				case KinectGestures.Gestures.RaiseRightHand:
					turnRate = BASE_TURNRATE;
					turnCounter = TURNING_FRAMES;
					hasPoweredUp = false;
					break;
				case KinectGestures.Gestures.Pull:
				case KinectGestures.Gestures.ZoomIn:
					curSpeed += BASE_SPEED;
					hasPoweredUp = false;
					break;
				case KinectGestures.Gestures.Push:
				case KinectGestures.Gestures.ZoomOut:
					curSpeed -= BASE_SPEED;
					if (curSpeed < 0)
						curSpeed = 0;
					hasPoweredUp = false;
					break;
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
