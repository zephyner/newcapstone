using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public float RotateSpeed = 1f;
    public Transform target;
    public Transform player;
    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotateSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotateSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
