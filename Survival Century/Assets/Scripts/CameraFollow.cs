using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform playerObj;

    public Vector3 offset;
    public bool enableOffsetValues;
    public float rotateSpeed;

    //public Transform pivot;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!enableOffsetValues)
        {
            //The distance between the player and camera
            offset = playerObj.position - transform.position;
        }

        //Moves the pivot to where the player is
        //pivot.position = playerObj.transform.position;
        //pivot.transform.parent = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the X position of the mouse and rotates the player
        float hori = Input.GetAxis("Mouse X") * rotateSpeed;
        playerObj.Rotate(0, hori, 0);

        //Gets the Y position of the mouse and rotates the pivot
        float vert = Input.GetAxis("Mouse Y") * rotateSpeed;
        playerObj.Rotate(-vert, 0, 0);
        
        //transform.position = playerObj.position - offset;

        //Uses the camera to always be looking at our player
        transform.LookAt(playerObj);

        //Moves the camera depending on the direction the player is facing
        float yAngle = playerObj.eulerAngles.y;
        float xAngle = playerObj.eulerAngles.x;

        //Moves the camera whenever the player moves
        Quaternion rotate = Quaternion.Euler(xAngle, yAngle, 0);
        transform.position = playerObj.position - (rotate * offset);


    }
}
