using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
   
    //Config
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float deathBouncing = 5f;
    
    //Cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    AudioSource myAudioSource;
    float startingGravity;

    // Use this for initialization
    void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
