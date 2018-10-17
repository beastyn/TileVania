using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement; //TODO delete after testing


public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float deathBouncing = 5f;


    //State
    bool isAlive = true;
    

    //Cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float startingGravity;

    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = myRigidbody.gravityScale;
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isAlive)
        {
            Run();
            Jump();
            ClimbTree();
            Death();
        }

    }

    private void Run()
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        myRigidbody.velocity = new Vector2(horizontalThrow * runSpeed, myRigidbody.velocity.y);
        FlipSprite();
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walking", playerHasHorizontalSpeed);
        
    }

    private void Jump()
    {
        int GroundLayerMask = LayerMask.GetMask("Ground");
        bool isPlayerOnGround = myFeetCollider.IsTouchingLayers(GroundLayerMask);
        if (!isPlayerOnGround) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToJump = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToJump;
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
        int climbingTreeLayerMask = LayerMask.GetMask("ClimbingTree");
        bool isTouchingClimbTree = myFeetCollider.IsTouchingLayers(climbingTreeLayerMask);

        if (!isTouchingClimbTree)
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = startingGravity;
            return;
        }

        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
              
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, verticalThrow * climbSpeed);
        myRigidbody.gravityScale = 0;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
     
    }

    void Death()
    {
        bool isTouchingEnemy = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Enviroment Hazzard"));
 
        if (!isTouchingEnemy) { return ; }

        isAlive = false;
        float currentVelocitySign = Mathf.Sign(myRigidbody.velocity.x); //form death effect by moving opposite direction
        myAnimator.SetBool("Death", true);
        myRigidbody.velocity = new Vector2(-currentVelocitySign * deathBouncing, myRigidbody.velocity.y);

        EventManager.TriggerEvent("PlayerDeath");

        
    }

}
