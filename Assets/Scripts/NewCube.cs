using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCube : MonoBehaviour
{

	public GameObject cam;
	public bool autoAdjustCamera = true;
	public bool followCubeLateral = true;
	Vector3 camOffset = Vector3.zero;

	public GameObject cube;
	public float speed = 5.0f;
	public float yPos = 0.51f;
	public float downGravity = 10.0f;
	public bool jumping = false;
	public float lateralOffset = 5.0f;
	public float laterialDuration = 0.3f;
	public float jumpDuration = 0.4f;
	public AnimationCurve jumpCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.25f, 1.0f), new Keyframe(1.0f, 0.0f));
	public float targetYPos = 5;

	public Vector3 movement = Vector3.zero;
	int moving = 0;
	int zPos = 0;
	float bias = 0.0f;
	Vector3 currentPos;
	Vector3 targetPos;
	Quaternion currentRot;
	Quaternion targetRot;


	float duration = 0.21f;


	void Start()
    {
    	transform.position = new Vector3(transform.position.x, transform.position.y, 0);



    	if(autoAdjustCamera)
    	{
    		camOffset = cam.transform.position - transform.position;
    	}  
    	movement = Vector3.zero;
    	zPos = 0;
    	bias = 0.0f;
    }

    void Update()
    {

    	updateKeyboardInput();


    	if(moving != 0)
    	{
 
    		transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(currentPos.y, targetPos.y, jumpCurve.Evaluate(bias)), Mathf.SmoothStep(currentPos.z, targetPos.z, bias));
    		

    		if(jumping)	
	    		cube.transform.rotation = Quaternion.Slerp(currentRot, targetRot, bias);
	    	else	
	    		transform.rotation = Quaternion.Slerp(currentRot, targetRot, bias);

    		if(bias >= 1.0f)
    		{
    			moving = 0;
    			transform.position = new Vector3(transform.position.x, currentPos.y, targetPos.z);

    			if(jumping)
    				cube.transform.rotation = targetRot;
    			else
    				transform.rotation = targetRot;
    			
    			jumping = false;
    		}

    		bias += Time.deltaTime / duration;
    	}

    	Vector3 offset = transform.right * Time.deltaTime * speed;

    	if(!jumping && transform.position.y > yPos)
    	{
    		transform.position = transform.position - new Vector3(0,downGravity * Time.deltaTime,0);
    	}



		transform.position = transform.position + offset;

		cam.transform.position = transform.position + camOffset;
    	cam.transform.position = new Vector3(transform.position.x + camOffset.x, 1.2f, followCubeLateral? cam.transform.position.z : 0);
    	
    	
    }

    

    void updateKeyboardInput()
    {

    		if(Input.GetKey("d")){goLeft();}
    		if(Input.GetKey("a")){goRight();}
    		if(Input.GetKey("w")){jump();}
    	
    }

    public void goLeft()
    {
    	if(moving != 1 && moving != 2 && zPos != -1)
    	{
    		zPos--;
    		moving = 1;
    		bias = 0.0f;
    		duration = laterialDuration;
    		
    		currentRot = transform.rotation;
    		targetRot = Quaternion.identity;
    		targetRot.eulerAngles = currentRot.eulerAngles - new Vector3(90, 0, 0);

    		currentPos = transform.position;
    		targetPos = new Vector3(0, transform.position.y, zPos * lateralOffset);
    	}
    }

    public void goRight()
    {
    	if(moving != 1 && moving != 2 && zPos != 1)
    	{
    		zPos++;
    		moving = 2;
    		bias = 0.0f;
    		duration = laterialDuration;

    		currentRot = transform.rotation;
    		targetRot = Quaternion.identity;
    		targetRot.eulerAngles = currentRot.eulerAngles + new Vector3(90, 0, 0);

    		currentPos = transform.position;
    		targetPos = new Vector3(0, transform.position.y, zPos * lateralOffset);
    	}
    }

    public void jump()
    {
    	if(moving != 3)
    	{

    		moving = 3;
    		bias = 0.0f;
    		duration = jumpDuration;
    		jumping = true;

    		currentRot = cube.transform.rotation;
    		targetRot = Quaternion.identity;

    		Vector3 xq = Vector3.zero;

    		if(zPos == 1) xq = new Vector3(0, 0, -90);
    		if(zPos == 0) xq = new Vector3(90, 0, 0);
    		if(zPos == -1) xq = new Vector3(0, 0, 90);


    		targetRot.eulerAngles = currentRot.eulerAngles + xq;

    		currentPos = transform.position;
    		targetPos = new Vector3(0, transform.position.y - targetYPos, transform.position.z);
    	}
    }

}
