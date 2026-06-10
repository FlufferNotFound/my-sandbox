using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DevCameraSystem : MonoBehaviour
{
    [Header("Camera settings")]
    [Range(0, 90)]
    public float mouseSensitivity = 15f;

    [Range(0, 360)]
    public float maxVerticalRotation;

    [Range(0, -360)]
    public float minVerticalRotation;

    public Transform devCamera;

    private float rotationX = 0f;

    private float rotationY = 0f;

    private void Awake()
    {
        rotationY = transform.eulerAngles.y;

        if (devCamera != null)
        {
            rotationX = devCamera.localEulerAngles.x;
        }
    }

    private void Update()
    {
        CameraMovement();
    }

    public void CameraMovement()
    {
        //Camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Horizontal mouse movement
        rotationY += mouseX;

        //Vertical mouse movement
        rotationX -= mouseY;

        //Limits rotation so the camera can't do a barrel roll on it's vertical axis.
        rotationX = Mathf.Clamp(rotationX, (maxVerticalRotation * -1), (minVerticalRotation * -1));

        //Applies horizontal mouse rotation to parent player object
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        //Applies rotation to only the children player camera
        devCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}