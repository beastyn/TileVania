using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointerHandler : MonoBehaviour {

    [SerializeField] Transform exit;
    [SerializeField] float rotationSpeed = 8;
    [SerializeField] Vector3[] distance;
    [SerializeField] float vibrationTime = 2;

    [SerializeField] float clampMinX = 0.2f;
    [SerializeField] float clampMaxX = 0.4f;

    [SerializeField] float clampMinY = 0.4f;
    [SerializeField] float clampMaxY = 0.6f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotatePointer();
        VibratePointer();

    }

    private void RotatePointer()
    {
        Vector3 relativePosition = exit.position - transform.position;
        Vector3 upwardVector = new Vector3(0, 0, 0);
        if (exit.position.x >= transform.position.x)
        {
            upwardVector = Vector3.up;
        }
        else
        {
            upwardVector = Vector3.down;
        }
        var newRotation = Quaternion.LookRotation(relativePosition, upwardVector);
        newRotation.x = 0f;
        newRotation.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }

    private void VibratePointer()
    {
        int randDistance = Random.Range(0, distance.Length);
        float clampedLocalPositionX = Mathf.Clamp(transform.localPosition.x + distance[randDistance].x, clampMinX, clampMaxX);
        float clampedLocalPositionY = Mathf.Clamp(transform.localPosition.y + distance[randDistance].y, clampMinY, clampMaxY);

        Vector3 endDistance = new Vector3(clampedLocalPositionX, clampedLocalPositionY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, endDistance, vibrationTime * Time.deltaTime);
    }


}
