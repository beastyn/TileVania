using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float deathBouncing = 5f;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip steps;


    //State
    bool isIntroPlayerEnded = false;
    bool isAlive = true;
    bool isGrounded = true;
    bool isAtTree = false;

    //Cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    AudioSource myAudioSource;
    PlayerTimelineSequencer timelineSequencer;

    float startingGravity;

    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAudioSource = GetComponent<AudioSource>();
        timelineSequencer = GetComponent<PlayerTimelineSequencer>();
        startingGravity = myRigidbody.gravityScale;
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        isIntroPlayerEnded = timelineSequencer.IsIntroPlayerEnded();
        if (isAlive && isIntroPlayerEnded)
        {
            isGrounded = IsGrounded();
            isAtTree = IsAtTree();
            HandleMovement();
            Death();
        }
        


    }

    private void HandleMovement()
    {
        HandleLayers();
        Run();
        Jump();
        ClimbTree();
    }

    private void HandleLayers()
    {
        if (isGrounded)
        {
            myAnimator.SetLayerWeight(1, 0);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 1);
        }

        if (isAtTree)
        {
            myAnimator.SetLayerWeight(2, 1);
           
        }
        else
        {
            myAnimator.SetLayerWeight(2, 0);
           
        }
    }

    private void Run()
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        myRigidbody.velocity = new Vector2(horizontalThrow * runSpeed, myRigidbody.velocity.y);
        FlipSprite();
        RunAnimationAndVSEffects();
    }

    private void RunAnimationAndVSEffects()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
       
        myAnimator.SetBool("Walking", playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed && !playerHasVerticalSpeed && !myAudioSource.isPlaying)
        {
            myAudioSource.clip = steps;
            myAudioSource.PlayOneShot(steps);
        }
        else if ((!playerHasHorizontalSpeed && myAudioSource.isPlaying) || playerHasVerticalSpeed)
        {
            myAudioSource.Stop();
        }
    }

    private bool IsGrounded()
    {
        int GroundLayerMask = LayerMask.GetMask("Ground");
        if (myFeetCollider.IsTouchingLayers(GroundLayerMask))
        {
            myAnimator.ResetTrigger("Jump");
            myAnimator.SetBool("Landing", false);
            return true;
        }

        return false;
    }

    private bool IsAtTree()
    {
        int climbingTreeLayerMask = LayerMask.GetMask("ClimbingTree");
        if (myFeetCollider.IsTouchingLayers(climbingTreeLayerMask))
        {
            myAnimator.SetTrigger("CanClimb");
            return true;
        }
        else
        {
            myAnimator.ResetTrigger("CanClimb");
            return false;
        }
    }

    private bool IsAtTreeTop()
    {
        int climbingTreeLayerMask = LayerMask.GetMask("ClimbingTree"); // TODO Make Shorter if can
        return !myBodyCollider.IsTouchingLayers(climbingTreeLayerMask);
        
    }

    private void Jump()
    {
        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("Landing", true);
        }
            if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
            {
                isGrounded = false;
                Vector2 jumpVelocityToJump = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity += jumpVelocityToJump;
                myAnimator.SetTrigger("Jump");
               
            }
    }
       
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
           transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), transform.localScale.y);
        }
    }

    private void ClimbTree()
    {

        if (!isAtTree)
        {

            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = startingGravity;
            return;
        }


        float verticalThrow = 0;
        if (IsAtTreeTop()) //can`t go up if on top of tree
        {
            verticalThrow = Mathf.Clamp(CrossPlatformInputManager.GetAxis("Vertical"), -1f, 0f);
        }
        else
        {
            verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        }
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, verticalThrow * climbSpeed);
        myRigidbody.gravityScale = 0;

        ClimbAnimationAndEffects();
    }

       
    private void ClimbAnimationAndEffects()
    {
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            bool playerOnGroundAtTree = IsAtTreeTop() || isGrounded;
            myAnimator.SetBool("Climbing", playerHasVerticalSpeed || !playerOnGroundAtTree);
            if (!playerHasVerticalSpeed && !playerOnGroundAtTree)
            {
                myAnimator.speed = 0;
            }
            else
            {
                myAnimator.speed = 1;
            }

            myAnimator.SetBool("Landing", false);

        
    }

    void Death()
    {
        bool isTouchingEnemy = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Enviroment Hazzard")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Enviroment Hazzard"));
 
        if (!isTouchingEnemy) { return ; }

        isAlive = false;

        float currentVelocitySign = Mathf.Sign(myRigidbody.velocity.x); //form death effect by moving opposite direction
        myRigidbody.velocity = new Vector2(-currentVelocitySign * deathBouncing, myRigidbody.velocity.y);

        myAnimator.SetBool("Landing", false);
        myAnimator.SetBool("Death", true);

        EventManager.TriggerEvent("PlayerDeath");
                
        FindObjectOfType<GameManager>().ProcessPlayerDeath();

              
    }

    IEnumerator LevelReload()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

   
}
