using UnityEngine;

//NOTE: Code here is dumb. Could be done better but it works and does what i wanted.
//Sure there's some issues here and there like the birds escaping their pen, but don't worry abou it.
public class BirdBehaviour : MonoBehaviour
{
    // [SerializeField]
    // private LayerMask birdLayer;

    // [SerializeField]
    // private LayerMask obstacleLayer;

    // [HideInInspector]
    // public float birdSpeed;

    // [HideInInspector]
    // public string birdName;

    // [HideInInspector]
    // public float detectionRange;

    // [SerializeField]
    // private GameObject birdHelperObject;
    
    // Vector3 direction = Vector3.zero;


    // /*
    // states:


    // funcs:
    // Check for collision
    // Find avaliable path
    //  */

    // private void Awake()
    // {
    //     birdHelper = birdHelperObject.gameObject.GetComponent<BoidHelper>();
    // }

    // void Update()
    // {
    //     MoveBird();
    //     Separation();
    // }

    // private void MoveBird()
    // {
    //     transform.position += birdSpeed * Time.deltaTime * transform.forward;
    // }

    // //Currently birds only avoid each other or obstacles.
    // private void Separation()
    // {
    //     //Loop through each ray and check if it hits a bird or an obstacle
    //     foreach (Vector3 point in birdHelper.points)
    //     {
    //         if (
    //             Physics.Raycast(
    //                 transform.position,
    //                 (point - transform.position).normalized,
    //                 out RaycastHit hit,
    //                 detectionRange,
    //                 birdLayer | obstacleLayer
    //             )
    //         )
    //         {
    //             //if it does, randomly choose to turn left or right. This is dumb but works, so it stays here
    //             switch (Random.Range(0, 1))
    //             {
    //                 //Turns left, i think
    //                 case 0:
    //                     direction -= transform.right;
    //                     break;

    //                 //Turn right, i think
    //                 case 1:
    //                     direction += transform.right;
    //                     break;
    //             }

    //             Debug.DrawRay(
    //                 transform.position,
    //                 (point - transform.position).normalized * detectionRange,
    //                 Color.green
    //                 );
    //         }

    //     }

    //     transform.rotation = Quaternion.LookRotation(transform.forward + direction.normalized);
    // }


}
