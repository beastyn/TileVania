using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnawballPickups : MonoBehaviour {

    [SerializeField] AudioClip snawBallPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(snawBallPickup, Camera.main.transform.position, 5);
        Destroy(gameObject);
    }
 
}
