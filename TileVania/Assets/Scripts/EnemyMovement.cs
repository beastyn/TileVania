using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 10f;

    bool isPlayerDead = false;

    Rigidbody2D myRigidbody;
    Animator myAnimator;

 

    // Use this for initialization
    void Start () {

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
       
        if (!isPlayerDead)
        {

            if (IsFacingRight())
            {
                myRigidbody.velocity = new Vector2(moveSpeed, 0);
            }
            else
            {
                myRigidbody.velocity = new Vector2(-moveSpeed, 0);
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
            myAnimator.SetBool("PlayerDead", true);
            GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
		
	}

    private void OnEnable()
    {
        EventManager.StartListening("PlayerDeath", PlayerDeath);
        
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerDeath", PlayerDeath);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    void PlayerDeath()
    {
        isPlayerDead = true;
    }
}
