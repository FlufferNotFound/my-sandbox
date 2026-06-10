using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
DevPlayerController
 -> DevPlayerMovementSystem
 -> DevPlayerCameraSystem
Where systems do something and controllers, well, control them.
*/

public class DevPlayerController : MonoBehaviour
{
    private DevCameraSystem dcs;

    private DevMovementSystem dms;

    private void Awake()
    {
        dcs = GetComponent<DevCameraSystem>();
        dms = GetComponent<DevMovementSystem>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            switch (dms.movementMode)
            {
                case 0:
                    dms.movementMode = 1;
                    dms.ToggleGravity(false);
                    dms.ToggleKinematic(true);
                    dms.ToggleColision(false);
                    break;

                case 1:
                    dms.movementMode = 0;
                    dms.ToggleGravity(true);
                    dms.ToggleKinematic(false);
                    dms.ToggleColision(true);
                    break;
            }
        }
    }
}
