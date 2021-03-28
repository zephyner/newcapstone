using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    public float distance = 10f;
    public float currentX = 0f;
    public float currentY = 0f;
    public float sensitivityX = 4f;
    public float sensitivityY = 1f;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, -currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
    }
}
