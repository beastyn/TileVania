using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonDustController : MonoBehaviour {


	void Start () {
        StartCoroutine(DustOnPlayerDestroy());
		
	}

    IEnumerator DustOnPlayerDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
       

    
}
