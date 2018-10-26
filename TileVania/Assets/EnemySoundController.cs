using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour {
    [SerializeField] Transform enemyListener;
    [SerializeField] float enemySoundRadius = 5f;
    [SerializeField] AudioClip[] enemySound;

    AudioSource myAudioSource;

    // Use this for initialization
    void Start () {

        myAudioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {

       
        Vector2 currentListenerRadius = enemyListener.position - transform.position;


        if (currentListenerRadius.magnitude <= enemySoundRadius && myAudioSource.isPlaying == false)
        {
            int pickedAudioNum = Random.Range(0, enemySound.Length);
            Debug.Log(pickedAudioNum);
            myAudioSource.PlayOneShot(enemySound[pickedAudioNum]);
        }
        else if (currentListenerRadius.magnitude >= enemySoundRadius && myAudioSource.isPlaying == true)
        {
            myAudioSource.Stop();
        }
        

	}
}
