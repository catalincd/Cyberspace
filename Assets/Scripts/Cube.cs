using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{


	[Header("Bound Objects")]
	public MaterialManager blockMaterial;
	public FadeIn respawnFade;
	public Respawn respawn;
	public GameObject respawnObject;
    public GameObject parentCube;
    public GameObject childCube;
    public Blinker blinker;
    public TileManager manager;
    public OverlayManager overlay;
    public ScoreManager scoreManager;
   
   	[Header("Events")]
   	public UnityEvent onTrig;

   	[Header("PowerUps")]
   	public float durationUps = 7.0f;
   	public float redMovementFactor = 3.0f;
   	public float idleDuration = 1.5f;
   	public float forwardRoute = 15.0f;
   	public bool m_lockWhilePowerUp = false;
   	bool upSent = false;

    [Header("Runtime")]
    public bool godMode = false;
    public float speed = 7.0f;
    public float targetSpeed = 9.0f;
    public float targetSpeedMult = 2.5f;
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
    bool respawned = false;

    Quaternion currentRot;
    Quaternion targetRot;

    Quaternion currentJump;
    Quaternion targetJump;

    bool rotate = true;
    bool jumpQueue = false;
    bool jumpDownQueue = false;

    bool jumpingDown = false;
    bool endingThen = false;

    public bool powerUp = false;
    bool redUp = false;
    bool greenUp = false;
    bool blueUp = false;
    bool noControls = false;


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
    public bool verticalCam = true;
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
    float yCamPos;

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

        powerUp = false;
    	redUp = false;
    	greenUp = false;
    	blueUp = false;

        if(autoAdjustCam)
        {
            camOffset = cam.transform.position - transform.position;
        }  

       // yCamPos = 

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

        	float speedBias = speedAnimationCurve.Evaluate(Mathf.Min(1.0f, transform.position.x / xTarget));
        	float speedAnimBias = Mathf.Lerp(1.0f, targetSpeedMult, speedBias);
        	float zPos = transform.position.z;
	        float nyPos = transform.position.y;
	        Quaternion rot = Quaternion.AngleAxis(90, Vector3.forward);

	       	refText.text = transform.up.ToString() + '\n' + abs(transform.up).ToString();

	        if(moving != 0)
	        {
	            lateralBias += Time.deltaTime * speedAnimBias / lateralDuration;


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
		        	jumpBias += jumpFactor * speedAnimBias * Time.deltaTime / jumpDownDuration;
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
		        	jumpBias += jumpFactor * speedAnimBias * Time.deltaTime / jumpDuration;
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
            float thisSpeed = Mathf.Lerp(speed, targetSpeed, speedBias);
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
	        float yPosOffset = Mathf.Max(0.0f, (transform.position.y + camOffset.y - 1.2f) * 0.65f);
	        cam.transform.position = new Vector3(transform.position.x + camOffset.x, yPosOffset + 1.2f, followCubeLateral? cam.transform.position.z : 0);
	    
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

        	if(redUp)
        		enroute();

        }
        else
        {
        	if(gameOver)
        	{
        		cameraShakeBias += Time.deltaTime / shakeDuration;
        		float currentAmplitude = shakeAmplitude * shakeCurve.Evaluate(cameraShakeBias);
        		Vector3 offset = Random.insideUnitSphere * currentAmplitude;

        		refText.text = offset.ToString();
        		cam.transform.position = finalCamPos + offset;
        	}
        }
    }

    public void enroute()
    {
    	if(!validNext(location))
    	{
    		upSent = true;
    		if(validNext(location + 1))
    		{
    			goRight();
    		}
    		else
    		if(validNext(location - 1))
    		{
    			goLeft();
    		}
    		else
    		if(validNext(location + 2))
    		{
    			goRight();
    		}
    		else
    		if(validNext(location - 2))
    		{
    			goLeft();
    		}
    	}
    }

    public void startRed()
    {
    	if(!powerUp && !gameOver)
    	{
            Achievements.incUps();
    		redUp = true;
    		powerUp = true;
    		noControls = true;
    		onTrig.Invoke();
    		movementFactor = redMovementFactor;
    		blinker.startHalf();
    		StartCoroutine(disableUp());
    	}
    }

    IEnumerator disableUp()
    {
    	yield return new WaitForSeconds(durationUps);
    	movementFactor = 1.0f;
    	noControls = false;
    	blinker.endHalf();
    	StartCoroutine(disableUpFinally());
    }

    IEnumerator disableUpFinally()
    {
    	yield return new WaitForSeconds(idleDuration);
    	powerUp = false;
    	redUp = false;
    	greenUp = false;
    	blueUp = false;
    }

    IEnumerator StartFading()
    {
        yield return new WaitForSeconds(1.0f);
        blinker.fadeOut();

        if(PlayerPrefs.GetInt("hertz", 0) > 0)
        {
        	respawnObject.SetActive(true);
        	respawn.begin();
        	respawnFade.start();
        }
    }

    IEnumerator StartOverlay()
    {
        yield return new WaitForSeconds(3.5f);
        if(!respawned)
       	{
       		 overlay.start();
      		  respawnFade.startOut();
      		  StartCoroutine(StartScene());
       	}
        else
        {
        	respawned = false;
        }
        
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1.5f);
        if(!respawned)
        {
            Achievements.incDeaths();
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }
        else
        {
          	respawned = false;
        }
    }

    public void startRespawn()
    {
        Achievements.incRespawns();
    	respawned = true;
    	gameOver = false;
        endingThen = false;
        noControls = false;
      	transform.position = new Vector3(transform.position.x, yPos, 0);
    	transform.rotation = Quaternion.identity;
    	childCube.transform.rotation = Quaternion.identity;
    	transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    	
        StartCoroutine(resetOthers());
    }

    IEnumerator resetOthers()
    {
    	yield return new WaitForSeconds(0.5f);
    	movementFactor = 1.0f;
    	
    	stopped = false;
    	cameraShakeBias = 0;
    	moving = 0;
        jumping = false;
        jumpingDown = false;
        jumpFactor = defaultJumpFactor;
        lateralBias = 0.0f;
        jumpBias = 0.0f;
        imageBias = 0.0f;
        location = 0;
        oldLocation = 0;
        jumpQueue = false;
        jumpDownQueue = false;
        stopped = false;
        animateImage = false;

        powerUp = false;
    	redUp = false;
    	greenUp = false;
    	blueUp = false;

    	blockMaterial.reset();
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

    public void goRightM(){if(!noControls || !m_lockWhilePowerUp)goRight();}
    public void goLeftM(){if(!noControls || !m_lockWhilePowerUp)goLeft();}
    public void goUpM(){if(!noControls || !m_lockWhilePowerUp)jump();}
    public void goDownM(){if(!noControls || !m_lockWhilePowerUp)jumpDown();}

    public void goRight()
    {
    	int target = location + 1;
    	if(powerUp)
    	{
    		if(!validNext(target) && !upSent)
    			return;
    	}

        if(moving == 0)
        {
            if(!validLateralTarget(target) || location == 1)
            {
                StartCoroutine(endThen());
            }
            upSent = false;
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
    	if(powerUp)
    	{
    		if(!validNext(target) && !upSent)
    			return;
    	}

        if(moving == 0)
        {
        	upSent = false;
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
            else if(jumpBias < 0.85f && jumpingDown)
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
        float speedBias = speedAnimationCurve.Evaluate(Mathf.Min(1.0f, transform.position.x / xTarget));
        float speedAnimBias = Mathf.Lerp(1.0f, targetSpeedMult, speedBias);
        yield return new WaitForSeconds(yieldLateral / speedAnimBias);
        endGame();
    }

    public void endGame()
    {
        if(godMode || gameOver || powerUp)return;
        endingThen = false;
        stopped = true;
        gameOver = true;
        movementFactor = 0.0f;
        finalCamPos = cam.transform.position;
        blinker.dissolveColor();
        
        StartCoroutine(StartFading());
        
        StartCoroutine(StartOverlay());

        scoreManager.addCoins();
    }

    public void CollideGreen(bool force = false)
    {
        if(!jumping || jumpBias < 0.10f || jumpBias > 0.85f || force)
            endGame();
    }

    public void CollideGreenTrig()
    {
        if(jumping && !jumpingDown)
            CollideGreen();
        else
            endGame();
    }

    bool validTarget(int target)
    {
    	return manager.validTarget(target, transform.position.x);
    }

    bool validNext(int target)
    {
    	if(target > 1 || target < -1) 
    		return false;

    	return manager.validTarget(target, transform.position.x + forwardRoute);
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
