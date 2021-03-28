using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;

    public float speed = 6f;

    //To make the turning look more natural on the movement
    public float smoothTurnTime = 0.1f;
    private float smoothTurnSpeed;

    public float gravity = -9.81f;
    public float groundDist = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;

    public float jump = 3f;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Helps ensure the velocity isn't increasing as we are grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        float hori = Input.GetAxisRaw("Horizontal");        
        float vert = Input.GetAxisRaw("Vertical");
      
        //Ensures there is no y-axis movement when moving on the ground
        Vector3 dir = new Vector3(hori, 0f, vert).normalized;

        //Checks if there is a direction the player is moving in
        if (dir.magnitude >= 0.1f)
        {
            //used to rotate the z-axis to face the direction the player is facing
            float lookingDir = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookingDir, ref smoothTurnSpeed, smoothTurnTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Helps to have the camera and player move in the same direction
            Vector3 moveDir = Quaternion.Euler(0f, lookingDir, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


        //The gravity of how fast the player falls
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
