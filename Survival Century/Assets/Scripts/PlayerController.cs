using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public AudioSource jumpSound;

    public float speed = 6f;

    public Camera playerCam;

    public float desiredRotation = 0f;
    public float rotateSpeed = 15f;
    public float gravity = -9.81f;
    private float speedY = 0f;

    bool jump = false;
    public float jumpSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //We use x and z due to the x moving side to side, and z moving forward and backward
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //Thejump of the player
        if (Input.GetButtonDown("Jump") && !jump)
        {
            jump = true;
            jumpSound.Play();
            speedY += jumpSpeed;
        }
        if (jump && speedY < 0)
        {
            RaycastHit ground;
            if (Physics.Raycast(transform.position, Vector3.down, out ground, 0.5f, LayerMask.GetMask("Ground")))
            {
                jump = false;
            }
        }

        //The gravity of the player
        speedY += gravity * Time.deltaTime;
        Vector3 vertMove = Vector3.up * speedY;
        if (!controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if (speedY < 0)
        {
            speedY = 0;
        }

        Vector3 move = new Vector3(x, 0, z).normalized;

        //Keeps us from moving up and down on the Y axis when rotating the camera
        Vector3 rotateMove = Quaternion.Euler(0, playerCam.transform.rotation.eulerAngles.y, 0)* move;

        //Moves our character at a controllable speed
        controller.Move(vertMove + (rotateMove * speed * Time.deltaTime));

        //Rotates the camera moving with the player
        if (rotateMove.magnitude > 0)
        {
            desiredRotation = Mathf.Atan2(rotateMove.x, rotateMove.z) * Mathf.Rad2Deg;
            
        }

        //Makes it so the player doesn't snap when changing directions
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, desiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
