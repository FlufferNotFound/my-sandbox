using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevMovementSystem : MonoBehaviour
{

    [Header("Flying settings")]

    public float flyingSpeed = 1.0f;

    public float flyingMultiplier = 1.0f;


    [Header("Walking settings")]

    public float walkingSpeed = 5.0f;

    public float runningMultiplier = 2.0f;


    [Header("Jumping settings")]

    public float jumpPower = 5.0f;

    public float jumpCooldownTime;

    public float verticalSpeedThreshold;

    [Header("Ground check settings")]
    public Transform rayEmitter;
    public float groundThreshold = 0.2f;
    public float rayLength = 2f;

    private float cooldownWait;

    private Rigidbody rigidBody;

    //0 is walking, 1 is flying
    [HideInInspector]
    public int movementMode;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        //Locks the cursor so it doesn't slide out of the window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movementMode = 0;

        ConstraintRBaxis(true);
    }

    private void Update()
    {
        ScrollCameraSpeed();
    }

    //Fixed update to make sure you can move arround even with low framerates
    private void FixedUpdate()
    {
        switch (movementMode)
        {
            case 0:
                WalkingMovement();

                break;

            case 1:
                FlyingMovement();

                break;

                //default:
                //Debug.Log("Wtf");
        }
    }

    //IMPLEMENT: Use unity's new Input System Package on WalkingMovement and FlyingMovement, this is a mess.
    public void WalkingMovement()
    {
        ConstraintRBaxis(true);

        //Stores rigid body forces
        Vector3 movementForces = Vector3.zero;

        //Code gets repetitive but basically it detects WASD presses
        //and adds the according forces to the rigid body
        if (Input.GetKey(KeyCode.W))
        {
            movementForces += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementForces -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementForces += transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementForces -= transform.right;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rigidBody.AddForce((movementForces * walkingSpeed) * runningMultiplier, ForceMode.Force);
        }
        else
        {
            rigidBody.AddForce(movementForces * walkingSpeed, ForceMode.Force);
        }

        if (Input.GetButton("Jump") && GroundCheck() && Time.time >= cooldownWait)
        {
            rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            cooldownWait = Time.time + jumpCooldownTime;
        }
    }

    public void FlyingMovement()
    {
        //Same thing like movement but it only affects the transform property 
        ConstraintRBaxis(false);

        Vector3 movementPosition = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementPosition += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementPosition -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementPosition += transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementPosition -= transform.right;
        }

        if (Input.GetKey(KeyCode.E))
        {
            movementPosition += transform.up;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            movementPosition -= transform.up;
        }

        flyingSpeed = Mathf.Clamp(flyingSpeed, 0f, 10f);

        //Running.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += movementPosition.normalized * flyingSpeed * flyingMultiplier;
        }
        else
        {
            transform.position += movementPosition.normalized * flyingSpeed;
        }


    }

    public void ConstraintRBaxis(bool constraintThem)
    {
        if (constraintThem)
        {
            rigidBody.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }

    public void ToggleGravity(bool b)
    {
        rigidBody.useGravity = b;
    }

    public void ToggleKinematic(bool b)
    {
        rigidBody.isKinematic = b;
    }

    public void ToggleColision(bool b)
    {
        GetComponent<Collider>().enabled = b;
    }

    void ResetAllPlayerRotationAxis()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void ResePlayerRotationAxisX()
    {
        transform.rotation = Quaternion.Euler(0, transform.position.y, rigidBody.position.z);
    }

    void ResetPlayerRotationAxisY()
    {
        transform.rotation = Quaternion.Euler(transform.position.x, 0, transform.position.z);
    }

    void ResetPlayerRotationAxisZ()
    {
        transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, 0);
    }

    void SetAllPlayerRotationAxis(float r)
    {
        transform.rotation = Quaternion.Euler(r, r, r);
    }

    void SetPlayerRotationAxisX(float r)
    {
        transform.rotation = Quaternion.Euler(r, transform.position.y, transform.position.z);
    }

    void SetPlayerRotationAxisY(float r)
    {
        transform.rotation = Quaternion.Euler(transform.position.x, r, transform.position.z);
    }

    void SetPlayerRotationAxisZ(float r)
    {
        transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, r);
    }

    bool GroundCheck()
    {
        RaycastHit hit;

        // Cast a ray downward
        if (Physics.Raycast(rayEmitter.position, Vector3.down, out hit, rayLength))
        {
            float distanceToGround = hit.distance;

            return distanceToGround <= groundThreshold;
        }

        return false;
    }

    private void ScrollCameraSpeed()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f) //Up
        {
            flyingSpeed += scroll;
        }
        else if (scroll < 0f) //Down
        {
            flyingSpeed += scroll;
        }
    }
}