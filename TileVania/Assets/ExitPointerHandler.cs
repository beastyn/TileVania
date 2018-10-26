using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointerHandler : MonoBehaviour {

    [SerializeField] Transform exit;
    [SerializeField] float rotationSpeed = 8;
    [SerializeField] Vector3[] distance;
    [SerializeField] float vibrationTime = 2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int randDistance = Random.Range(0, distance.Length);
        Vector3 relativePosition = exit.position - transform.position;
        var newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        newRotation.x = 0f;
        newRotation.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

        float clampedLocalPositionX = Mathf.Clamp(transform.localPosition.x + distance[randDistance].x, 0.2f, 0.4f);
        float clampedLocalPositionY = Mathf.Clamp(transform.localPosition.y + distance[randDistance].y, 0.4f, 0.6f);

        Vector3 endDistance = new Vector3(clampedLocalPositionX, clampedLocalPositionY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, endDistance, vibrationTime*Time.deltaTime);
        
	}
}
