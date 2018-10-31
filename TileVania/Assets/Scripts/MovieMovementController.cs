using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovieMovementController : MonoBehaviour {

    [SerializeField] float moveSpeed;
    [SerializeField] float deltaPositionX = 0.1f;
    [SerializeField] float deltaPositionY = 0.1f;

    ExitPointerHandler exitPointer;

    // Use this for initialization
    void Start () {

        exitPointer = GetComponent<ExitPointerHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 endPosition = transform.parent.localPosition + new Vector3(deltaPositionX, deltaPositionY, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, endPosition, moveSpeed*Time.deltaTime);

        float checkPositionDeltaX = Mathf.Round(transform.localPosition.x - deltaPositionX);
        float checkPositionDeltaY = Mathf.Round(transform.localPosition.y - deltaPositionY);

        //Stop translating to player ans start pointer logic
        if (checkPositionDeltaX <= Mathf.Epsilon && checkPositionDeltaY <= Mathf.Epsilon)
        {
            this.enabled = false;
            exitPointer.enabled = true;
        }
    }
}
