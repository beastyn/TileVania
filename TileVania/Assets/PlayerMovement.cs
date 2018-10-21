using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
   
    //Config
    [SerializeField] float runSpeed = 10f;
  //  [SerializeField] float jumpSpeed = 5f;
  //  [SerializeField] float climbSpeed = 3f;
  //  [SerializeField] float deathBouncing = 5f;
    
    //Cached

 //   Animator myAnimator;
  //  CapsuleCollider2D myBodyCollider;
 //   BoxCollider2D myFeetCollider;
  //  AudioSource myAudioSource;
  //  float startingGravity;

    // Use this for initialization
    void Start () {

      
     //  myAnimator = GetComponent<Animator>();
     //  myBodyCollider = GetComponent<CapsuleCollider2D>();
     //  myFeetCollider = GetComponent<BoxCollider2D>();
     //  myAudioSource = GetComponent<AudioSource>();
     //  startingGravity = myRigidbody.gravityScale;

    }

    // Update is called once per frame
    public Vector2 VelocityForRun(Rigidbody2D myRigidbody)
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        return new Vector2(horizontalThrow * runSpeed, myRigidbody.velocity.y);
    }    
       
       /* bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        Debug.Log(myRigidbody.velocity.y);

        myAnimator.SetBool("Walking", playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed && !playerHasVerticalSpeed && !myAudioSource.isPlaying)
        {
            myAudioSource.clip = steps;
            myAudioSource.PlayOneShot(steps);
        }
        else if ((!playerHasHorizontalSpeed && myAudioSource.isPlaying) || playerHasVerticalSpeed)
        {
            myAudioSource.Stop();
        }*/
    
}
