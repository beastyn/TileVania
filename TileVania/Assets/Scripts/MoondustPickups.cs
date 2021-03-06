﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoondustPickups : MonoBehaviour {

    [SerializeField] int pointsPerDust = 10;
    [SerializeField] GameObject pickedFX;
    [SerializeField] Transform parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //AudioSource.PlayClipAtPoint(snawBallPickup, Camera.main.transform.position, 5);
        GameObject fx = Instantiate(pickedFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;

        FindObjectOfType<GameManager>().AddToScore(pointsPerDust);

        Destroy(gameObject);
    }
 
}
