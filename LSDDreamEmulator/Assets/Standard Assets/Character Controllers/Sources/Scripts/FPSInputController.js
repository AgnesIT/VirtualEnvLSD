private var motor : CharacterMotor;
public var speed = 1;
private var cameraTransform;
// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
}


function Start()
{


}


// Update is called once per frame
function Update () {
	// Get the input vector from keyboard or analog stick
	//var directionVector = new Vector3(0, 0, 1);
	var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	var yRotation : float = 5.0;
	cameraTransform = Camera.main.transform;
	
	
	
	
	  if(Input.GetKey(KeyCode.RightArrow))
     {
     //transform.eulerAngles = Vector3.Lerp(Camera.mainCamera.transform.eulerAngles, new Vector3(0, 1000, 0), Time.deltaTime*0.2);
    transform.Rotate(0,-3,0, Space.World);
     }
     if(Input.GetKey(KeyCode.LeftArrow))
     {
     //Camera.mainCamera.transform.eulerAngles = Vector3.Lerp(Camera.mainCamera.transform.eulerAngles, new Vector3(0, -360, 0), Time.deltaTime*0.2);
    
     //transform.eulerAngles = Vector3.Lerp(Camera.mainCamera.transform.eulerAngles, new Vector3(0, -360, 0), Time.deltaTime*0.2);
		//transform.Rotate(0, 0.5, 0);
     transform.Rotate(0,3,0, Space.World);
        }
   /*  if(Input.GetKey(KeyCode.DownArrow))
     {
         motor.inputMoveDirection = Vector3(0,-1,0);
     }*/
     
  if(Input.GetKey(KeyCode.UpArrow))
     {
     /* transform.position += cameraTransform.forward * (Time.deltaTime * 3);
              Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 80, 2*Time.deltaTime);
       */
       
       directionVector = new Vector3(0, 0, 1);
       if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}
	//motor.inputMoveDirection = transform.rotation * directionVector;
	//motor.inputJump = Input.GetButton("Jump"); 
       
 
     }
     else
     {
	
	// Apply the direction to the CharacterMotor
	//motor.inputMoveDirection = new Vector3(0,0,0);
	//motor.inputJump = Input.GetButton("Jump"); 
	}
	
	
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
