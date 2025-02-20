﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    //the target the camera is following 
    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    //how fast the camera rotates around the character
    public float rotateSpeed;

    public Transform pivot;

    public bool inFinalScene;


    public float maxViewAngle; //The maxium roation when going up
    public float minViewAngle; //The maxium roation when going up

    // Start is called before the first frame update
    void Start()
    {
        //the set distance the camera is follwoing the target
        offset = target.position - transform.position;

        pivot.transform.position = target.transform.position; //move the pivot where the character is located
        //pivot.transform.parent = target.transform; // make the pivot the child of the player
        pivot.transform.parent = null; // this stops the camera rotating really fast

        
        //hides the cursor
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position; //pivot will move around the player


        // get the X position of the mouse & rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0); //

        //Get the Y positon of the mouse & rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //Limit the vertical rotation
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Move the camera based on the current rotation of the target & the original offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);


        //transform.position = target.position - offset;

        //this stops the camera from going under the floor
        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        //the camera will follow the 'target' which is the player
        transform.LookAt(target);
    }
}
