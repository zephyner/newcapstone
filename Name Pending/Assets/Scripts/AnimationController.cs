using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anima;
    int walkingForwardHash;
    int walkingBackHash;
    int walkingLeftHash;
    int walkingRightHash;

    int runningForwardHash;
    int runningBackHash;
    int runningLeftHash;
    int runningRightHash;

    int jumpHash;
    int isGroundedHash;
    

    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();

        //Corrisponds to the string in the animator
        walkingForwardHash = Animator.StringToHash("isWalkingForward");
        walkingBackHash = Animator.StringToHash("isWalkingBack");
        walkingLeftHash = Animator.StringToHash("isWalkingLeft");
        walkingRightHash = Animator.StringToHash("isWalkingRight");

        runningForwardHash = Animator.StringToHash("isRunningForward");
        runningBackHash = Animator.StringToHash("isRunningBack");
        runningLeftHash = Animator.StringToHash("isRunningLeft");
        runningRightHash = Animator.StringToHash("isRunningRight");

        jumpHash = Animator.StringToHash("jump");
        isGroundedHash = Animator.StringToHash("isGrounded");
    }

    // Update is called once per frame
    void Update()
    {
        //Corrisponds to the walkingHash of that direction
        bool isWalking_F = anima.GetBool(walkingForwardHash);
        bool isWalking_B = anima.GetBool(walkingBackHash);
        bool isWalking_L = anima.GetBool(walkingLeftHash);
        bool isWalking_R = anima.GetBool(walkingRightHash);
        
        //Checks if we are already running
        bool isRunning_F = anima.GetBool(runningForwardHash);
        bool isRunning_B = anima.GetBool(runningBackHash);
        bool isRunning_L = anima.GetBool(runningLeftHash);
        bool isRunning_R = anima.GetBool(runningRightHash);

        //
        bool isJumping = anima.GetBool(jumpHash);
        bool grounded = anima.GetBool(isGroundedHash);

        //The direction that uses the animation
        bool forwardInput = Input.GetKey("w");
        bool backInput = Input.GetKey("s");
        bool leftInput = Input.GetKey("a");
        bool rightInput = Input.GetKey("d");
        bool runInput = Input.GetKey("left shift");

        bool jumpInput = Input.GetKey("space");

        

        //Used for the input to walk when pressed
        if (!isWalking_F && forwardInput) 
        {
            anima.SetBool("isWalkingForward", true);
        }
        else if (!isWalking_R && backInput)
        {
            anima.SetBool("isWalkingBack", true);
        }
        else if (!isWalking_L && leftInput)
        {
            anima.SetBool("isWalkingLeft", true);
        }
        else if (!isWalking_R && rightInput)
        {
            anima.SetBool("isWalkingRight", true);
        }

        //Used for the input to walk when not being pressed
        if (isWalking_F && !forwardInput)
        {
            anima.SetBool("isWalkingForward", false);
        }
        else if (isWalking_B && !backInput)
        {
            anima.SetBool("isWalkingBack", false);
        }
        else if (isWalking_L && !leftInput)
        {
            anima.SetBool("isWalkingLeft", false);
        }
        else if (isWalking_R && !rightInput)
        {
            anima.SetBool("isWalkingRight", false);
        }

        //Used for when walking, and not running when holding the run button
        if (!isRunning_F && (forwardInput && runInput))
        {
            anima.SetBool("isRunningForward", true);
        }
        else if (!isRunning_B && (backInput && runInput))
        {
            anima.SetBool("isRunningBack", true);
        }
        else if (!isRunning_L && (leftInput && runInput))
        {
            anima.SetBool("isRunningLeft", true);
        }
        else if (!isRunning_R && (rightInput && runInput))
        {
            anima.SetBool("isRunningRight", true);
        }

        //Used for when running and stops running or walking
        if (isRunning_F && (!forwardInput || !runInput))
        {
            anima.SetBool("isRunningForward", false);
        }
        else if (isRunning_B && (!backInput || !runInput))
        {
            anima.SetBool("isRunningBack", false);
        }
        else if (isRunning_L && (!leftInput || !runInput))
        {
            anima.SetBool("isRunningLeft", false);
        }
        else if (isRunning_R && (!rightInput || !runInput))
        {
            anima.SetBool("isRunningRight", false);
        }

        //Used for jumping
        if (!isJumping && jumpInput)
        {
            anima.SetBool("jump", true);


        }
        else if (isJumping && !jumpInput)
        {
            anima.SetBool("jump", false);
        }

        //Checks if the player is grounded
        if (!grounded && anima.GetBool("isGrounded"))    
        {
            anima.SetBool("isGrounded", true);
        }
        else if (grounded && !anima.GetBool("isGrounded"))
        {
            anima.SetBool("isGrounded", false);
        }
    }
}
