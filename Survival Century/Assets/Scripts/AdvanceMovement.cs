using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceMovement : MonoBehaviour
{
    Test_Controller walkSpeed;
    public float runSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = GetComponent<Test_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpeed.speed += runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            walkSpeed.speed -= runSpeed;
        }
    }
}
