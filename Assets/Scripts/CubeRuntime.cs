using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeRuntime : MonoBehaviour
{


	public GameObject cam;
    public GameObject parentCube;
    public GameObject childCube;
    public bool autoAdjustCam = true;
    public bool followCubeLateral = true;

    [Header("Runtime")]
    public float speed = 7.0f;
    public float yPos = 0.51f;

    [Header("Jumping")]

    public AnimationCurve jumpCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.25f, 1.0f), new Keyframe(0.75f, 1.0f),new Keyframe(1.0f, 0.0f));
    public float jumpOffset = 1.25f;
    public float downGravity = 8.0f;
    public float jumpDuration = 0.3f;

    [Header("Movement")]

    public float lateralOffset = 2.0f;
    public float lateralDuration = 0.17f;

    Quaternion currentRot;
    Quaternion targetRot;
    bool rotate = true;
    bool jumpQueue = false;
    public Text refText;

    Vector3 eulers = Vector3.zero;



    int moving = 0;
    int location = 0;
    int oldLocation = 0;
    bool jumping = false;
    float lateralBias = 0.0f;
    float jumpBias = 0.0f;
    Vector3 camOffset = Vector3.zero;


    void Start()
    {
    	moving = 0;
        jumping = false;
        lateralBias = 0.0f;
        jumpBias = 0.0f;
        location = 0;
        jumpQueue = false;

        if(autoAdjustCam)
        {
            camOffset = cam.transform.position - transform.position;
        }  
    }

    void Update()
    {
        //animate jump
        //animate swipe

        //debug
        if(Input.GetKey("d")){goLeft();}
        if(Input.GetKey("a")){goRight();}
        if(Input.GetKey("space")){jump();}

        float zPos = transform.position.z;
        float nyPos = transform.position.y;
        Quaternion rot = Quaternion.AngleAxis(90, Vector3.forward);

        refText.text = childCube.transform.forward.ToString() + '\n' +"";

        if(moving != 0)
        {
            lateralBias += Time.deltaTime / lateralDuration;

            if(lateralBias >= 1.0f)
            {
                moving = 0;
                oldLocation = location;
                lateralBias = 0.0f;
                if(!jumping)
                {
                    currentRot = targetRot;
                }
            }


            zPos = Mathf.Lerp(oldLocation, location, lateralBias) * lateralOffset;

            if(!jumping)
            {
                childCube.transform.rotation = Quaternion.Slerp(currentRot, targetRot, lateralBias);
                //childCube.transform.Rotate(90 * Time.deltaTime / lateralDuration,0, 0, Space.World);
            }

        }

        if(jumping)
        {
            jumpBias += Time.deltaTime / jumpDuration;

            if(jumpBias >= 1.0f)
            {
                jumping = false;
                jumpBias = 0.0f;
                currentRot = targetRot;
            }

            nyPos = yPos + Mathf.Lerp(0, jumpOffset, jumpCurve.Evaluate(jumpBias));
            childCube.transform.rotation = Quaternion.Slerp(currentRot, targetRot, jumpBias);
        }


        //move
        float xOffset = Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x + xOffset, nyPos, zPos);

        //rotate
        //Quaternion quat = Quaternion.identity;
        //quat.eulerAngles = getGlobalEuler();
        //transform.rotation = quat;        

        //gravity
        if(!jumping && transform.position.y > yPos)
        {
            transform.position = transform.position - new Vector3(0,downGravity * Time.deltaTime,0);
        }

        //set camera
        cam.transform.position = transform.position + camOffset;
        cam.transform.position = new Vector3(transform.position.x + camOffset.x, 1.2f, followCubeLateral? cam.transform.position.z : 0);
    
        //jumpQueue
        if(jumpQueue && moving == 0)
        {
            jumpQueue = false;
            jump();
        }

        

    }

    Vector3 getGlobalEuler()
    {
        return eulers * 90;
    }

    public void goRight()
    {
        if(moving == 0 && location != 1)
        {
            moving = 1;
            location++;
            if(!jumping)
            {
                currentRot = childCube.transform.rotation;
                targetRot = currentRot * Quaternion.AngleAxis(90, childCube.transform.right);
            }
        }
    }

    public void goLeft()
    {
        if(moving == 0 && location != -1)
        {
            moving = -1;
            location--;
            if(!jumping)
            {
                currentRot = childCube.transform.rotation;
                targetRot = currentRot * Quaternion.AngleAxis(-90, childCube.transform.right);
            }
        }
    }

    public void jump()
    {
        if(!jumping)
        {
            if(moving == 0)
            {
                jumping = true;
                currentRot = childCube.transform.rotation;
                targetRot = currentRot * Quaternion.AngleAxis(90, childCube.transform.forward);
            }
            else
            {
                jumpQueue = true;
            }
        }
    }

    void OnCollisionEnter()
    {
       // Debug.Log("Collision" + (i++));
    }

    
}
