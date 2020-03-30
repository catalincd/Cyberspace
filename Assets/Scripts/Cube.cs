using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{


	
    public GameObject parentCube;
    public GameObject childCube;
    public Blinker blinker;
    public TileManager manager;
    public OverlayManager overlay;
    public ScoreManager scoreManager;
   

    [Header("Runtime")]
    public bool godMode = false;
    public float speed = 7.0f;
    public float targetSpeed = 9.0f;
    public AnimationCurve speedAnimationCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 1.0f));
    public float xTarget = 30.0f;
    public float yPos = 0.51f;
    public float initialYPos = 5f;
    public bool debugFullSpeed = false;

    [Header("Jumping")]

    public AnimationCurve jumpCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.15f, 1.0f), new Keyframe(0.75f, 0.7f),new Keyframe(1.0f, 0.0f));
    public float jumpOffset = 1.25f;
    public float downGravity = 8.0f;
    public float jumpDuration = 0.3f;
    public float animationRatio = 2.0f;
    public float defaultJumpFactor = 1.5f;
    public float jumpFactorMultiplier = 2.0f;

    [Header("Jumping Down Flat")]
    public float verticalScale = 0.5f;
    public float horizontalScale = 0.2f;
    public float jumpDownOffset = 0.25f;
    public float jumpDownDuration = 0.2f;

    [Header("Movement")]

    public float lateralOffset = 2.0f;
    public float lateralDuration = 0.17f;

    float movementFactor = 1.0f;
    bool stopped = false;
    bool gameOver = false;

    Quaternion currentRot;
    Quaternion targetRot;

    Quaternion currentJump;
    Quaternion targetJump;

    bool rotate = true;
    bool jumpQueue = false;
    bool jumpDownQueue = false;

    bool jumpingDown = false;
    bool endingThen = false;


	[Header("Score")]

    public Text refText;
    public Text scoreText;
    public float decimalsMultiplier = 1000;
    public float scoreMultiplier = 1.0f;
    public Image img;


    float initXPos;

    [Header("Camera")]
    public GameObject cam;
    public bool autoAdjustCam = true;
    public bool followCubeLateral = true;
    public float shakeAmplitude = 1.0f;
    public float shakeDuration = 0.7f;
    public AnimationCurve shakeCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.15f, 1.0f),new Keyframe(1.0f, 0.0f));

    Vector3 eulers = Vector3.zero;
    Vector3 finalCamPos;

    int moving = 0;
    int location = 0;
    int oldLocation = 0;
    bool jumping = false;
    float lateralBias = 0.0f;
    float imageBias = 0.0f;
    float jumpBias = 0.0f;
    float jumpFactor = 1.0f;
    Vector3 camOffset = Vector3.zero;
    float cameraShakeBias = 0.0f;
    bool animateImage = false;
    bool animateJump = true;
    float yieldLateral = 1.0f;

    void Start()
    {
    	moving = 0;
        jumping = false;
        jumpingDown = false;
        jumpFactor = defaultJumpFactor;

        lateralBias = 0.0f;
        jumpBias = 0.0f;
        imageBias = 0.0f;
        location = 0;
        jumpQueue = false;
        jumpDownQueue = false;
        stopped = false;
        animateImage = false;
        yieldLateral = lateralDuration / 4 - 0.02f;

        if(autoAdjustCam)
        {
            camOffset = cam.transform.position - transform.position;
        }  

        initXPos = transform.position.x;

        transform.position = new Vector3(transform.position.x, initialYPos, 0);

        if(debugFullSpeed)
            xTarget = 1;

    }

    void Update()
    {
        //animate jump
        //animate swipe

        //debug
        if(Input.GetKey("d")){goLeft();}
        if(Input.GetKey("a")){goRight();}
        if(Input.GetKey("w")){jump();}
        if(Input.GetKey("s")){jumpDown();}

        

        if(!stopped)
        {

        	float zPos = transform.position.z;
	        float nyPos = transform.position.y;
	        Quaternion rot = Quaternion.AngleAxis(90, Vector3.forward);

	       	refText.text = transform.up.ToString() + '\n' + abs(transform.up).ToString();

	        if(moving != 0)
	        {
	            lateralBias += Time.deltaTime / lateralDuration;


	            zPos = Mathf.Lerp(oldLocation, location, lateralBias) * lateralOffset;

	            if(!jumping || true)
	            {
	                transform.rotation = Quaternion.Slerp(currentRot, targetRot, lateralBias);
	            }

	            if(lateralBias >= 1.0f)
	            {
	                moving = 0;
	                oldLocation = location;
	                lateralBias = 0.0f;
	                currentRot = targetRot;
	                if(!jumping || true)
	                {   
	                    transform.rotation = targetRot;
	                    clearAngles();
	                }
	            }

	        }

	        if(jumping)
		    {
		    	

		    	if(jumpingDown)
		        {
		        	jumpBias += jumpFactor * Time.deltaTime / jumpDownDuration;
		    		float jumpAnimationBias = Mathf.Min(1.0f, jumpBias * animationRatio);
		        	nyPos = yPos - Mathf.Lerp(0, jumpDownOffset, jumpCurve.Evaluate(jumpBias));

		        	float nowBias = jumpCurve.Evaluate(jumpBias);
		        	float scaleVertical =  Mathf.Lerp(0.0f, verticalScale, nowBias);
		        	float scaleHorizontal =  Mathf.Lerp(0.0f, horizontalScale, nowBias);
		        	Vector3 currentScale = Vector3.one;
		        	Vector3 up = abs(transform.up);
		        	Vector3 forward = abs(transform.forward);

		        	currentScale -= up * scaleVertical;
		        	currentScale += Mul((Vector3.one - up), forward) * scaleHorizontal ;

		            transform.localScale = currentScale;

		            if(jumpBias >= 1.0f)
		            {
		                jumping = false;
		                jumpBias = 0.0f;
		                currentJump = targetJump;
		                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		            }
		        }
		        else
		        {    
		        	jumpBias += jumpFactor * Time.deltaTime / jumpDuration;
		    		float jumpAnimationBias = Mathf.Min(1.0f, jumpBias * animationRatio);

		            nyPos = yPos + Mathf.Lerp(0, jumpOffset, jumpCurve.Evaluate(jumpBias));

                    if(animateJump)
		              childCube.transform.rotation = Quaternion.Slerp(currentJump, targetJump, jumpAnimationBias);

		            if(jumpBias >= 1.0f)
		            {
		                jumping = false;
		                jumpBias = 0.0f;
                        if(animateJump)
                        {
                            currentJump = targetJump;
                            childCube.transform.rotation = currentJump;
                            clearAngles();
                        }
		            }
		        }
		    }


	        //move
            float thisSpeed = Mathf.Lerp(speed, targetSpeed, speedAnimationCurve.Evaluate(Mathf.Min(1.0f, transform.position.x / xTarget)));
	        //Debug.Log("" + thisSpeed);
            float xOffset = Time.deltaTime * thisSpeed * movementFactor;
	        transform.position = new Vector3(transform.position.x + xOffset, nyPos, zPos);

	        float currentScore = (transform.position.x - initXPos) * scoreMultiplier * decimalsMultiplier;
	        int displayScore = (int) currentScore;
	        scoreText.text = "" + displayScore;

	        //gravity
	        if(!jumping && transform.position.y > yPos)
	        {
	            transform.position = transform.position - new Vector3(0,downGravity * Time.deltaTime,0);
	        }

	        //set camera
	        cam.transform.position = transform.position + camOffset;
	        cam.transform.position = new Vector3(transform.position.x + camOffset.x, 1.2f, followCubeLateral? cam.transform.position.z : 0);
	    
	        //jumpQueue
	        if(jumpQueue && !jumping)
	        {
	            jumpQueue = false;
	            jump();
	        }

	        if(jumpDownQueue && !jumping)
	        {
	            jumpDownQueue = false;
	            jumpDown();
	        }

        	if(endNow())
        	{
                endGame();
        		
        	}
        }
        else
        {
        	cameraShakeBias += Time.deltaTime / shakeDuration;
        	float currentAmplitude = shakeAmplitude * shakeCurve.Evaluate(cameraShakeBias);
        	Vector3 offset = Random.insideUnitSphere * currentAmplitude;

        	refText.text = offset.ToString();
        	cam.transform.position = finalCamPos + offset;

        	//if(animateImage)
        	//{
        	//	imageBias += Time.deltaTime / 1.0f;
        	//	imageBias = Mathf.Min(imageBias, 1.0f);
///
        	///	img.color = new Color(0.0f, 0.0f, 0.0f, imageBias);

        	//}

        }

        //rotate
        //Quaternion quat = Quaternion.identity;
        //quat.eulerAngles = getGlobalEuler();
        //transform.rotation = quat;        

        

    }

    IEnumerator StartFading()
    {
        yield return new WaitForSeconds(1.0f);
        blinker.fadeOut();
    }

    IEnumerator StartOverlay()
    {
        yield return new WaitForSeconds(2.0f);
        overlay.start();
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    void clearAngles()
    {
    	Vector3 angles = transform.rotation.eulerAngles;
    	angles.x = Mathf.RoundToInt(angles.x / 90) * 90.0f;
    	angles.y = Mathf.RoundToInt(angles.y / 90) * 90.0f;
    	angles.z = Mathf.RoundToInt(angles.z / 90) * 90.0f;
    	transform.rotation = Quaternion.Euler(angles);

    	angles = childCube.transform.rotation.eulerAngles;
    	angles.x = Mathf.RoundToInt(angles.x / 90) * 90.0f;
    	angles.y = Mathf.RoundToInt(angles.y / 90) * 90.0f;
    	angles.z = Mathf.RoundToInt(angles.z / 90) * 90.0f;
    	childCube.transform.rotation = Quaternion.Euler(angles);
    }

    Vector3 getGlobalEuler()
    {
        return eulers * 90;
    }

    public void goRight()
    {
    	int target = location + 1;

        

        if(moving == 0)
        {
            if(!validLateralTarget(target) || location == 1)
            {
                StartCoroutine(endThen());
            }
            moving = 1;
            location++;
            if(!jumping)
            {
                currentRot = transform.rotation;
                targetRot = currentRot * Quaternion.AngleAxis(90.0f, transform.right);
            }
        }
    }

    public void goLeft()
    {
    	int target = location - 1;
        if(moving == 0)
        {
            if(!validLateralTarget(target) || location == -1)
            {
              StartCoroutine(endThen());
            }
            moving = -1;
            location--;
            if(!jumping)
            {
                currentRot = transform.rotation;
                targetRot = currentRot * Quaternion.AngleAxis(-90.0f, transform.right);
            }
        }
    }

    public void jump()
    {
        if(!jumping)
        {
        	jumpingDown = false;

            animateJump = (moving == 0);

            jumpFactor = defaultJumpFactor;
            jumping = true;

            if(animateJump)
            {
                currentJump = childCube.transform.rotation;
                targetJump = currentJump * Quaternion.AngleAxis(90, childCube.transform.forward);
            }
        }
        else
        {
        	if(jumpBias > 0.8f && !jumpingDown)
        	{
                jumpQueue = true;
                jumpFactor = defaultJumpFactor * jumpFactorMultiplier;
            }
        	if(jumpingDown)
        	{
        		jumpQueue = true;
        		jumpFactor = defaultJumpFactor * jumpFactorMultiplier;
        	}
        }
    }


    public void jumpDown()
    {
        if(!jumping)
        {
        	jumpingDown = true;
            jumpFactor = defaultJumpFactor;
            jumping = true;
        }
        else
        {
        	if(jumpBias > 0.8f && jumpingDown)
        	{
                jumpDownQueue = true;
                jumpFactor = defaultJumpFactor * jumpFactorMultiplier;
            }
            else if(jumpBias < 0.8f && jumpingDown)
            {
                jumpBias = 0.4f;
                jumpFactor = defaultJumpFactor * 1.07f;
            }

        	if(!jumpingDown)
        	{
        		jumpDownQueue = true;
        		jumpFactor = defaultJumpFactor * jumpFactorMultiplier;
        	}
        }
    }

    IEnumerator endThen()
    {
        endingThen = true;
        yield return new WaitForSeconds(yieldLateral);
        endGame();
    }

    public void endGame()
    {
        if(godMode || gameOver)return;
        endingThen = false;
        stopped = true;
        gameOver = true;
        movementFactor = 0.0f;
        finalCamPos = cam.transform.position;
        blinker.dissolveColor();
        
        StartCoroutine(StartFading());
        StartCoroutine(StartScene());
        StartCoroutine(StartOverlay());

        scoreManager.addCoins();
    }

    public void CollideGreen()
    {
        if(!jumping || jumpBias < 0.10f || jumpBias > 0.85f)
            endGame();
    }

    bool validTarget(int target)
    {
    	return manager.validTarget(target, transform.position.x);
    }

    bool validLateralTarget(int target)
    {
        return manager.validTarget(target, transform.position.x + 0.5f);
    }

    bool endNow()
    {
        if(endingThen)return false;
    	if(transform.position.x < 10) return false;
    	return !manager.validTarget(location, transform.position.x + 0.5f);
    }



    Vector3 abs(Vector3 a)
    {
    	return new Vector3(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z));
    }

    Vector3 Mul(Vector3 a, Vector3 b)
    {
    	return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    void OnCollisionEnter()
    {
       // Debug.Log("Collision" + (i++));
    }

    
}
