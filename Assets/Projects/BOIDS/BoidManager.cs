using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class BirdManager : MonoBehaviour
{


    // [Header("Bird Settings")]

    // [SerializeField]
    // private GameObject boidPrefab;

    // [SerializeField]
    // private float speed;

    // [SerializeField]
    // private float detectionRange;

    // [Header("Spawn settings")]

    // [SerializeField]
    // private bool randomSpawnLocation;

    // [SerializeField]
    // private Transform minBounds;

    // [SerializeField]
    // private Transform maxBounds;

    // [Header("UI Settings")]

    // [SerializeField]
    // private TMP_Text birdCountText;

    // [SerializeField]
    // private TMP_Text alignmentText;

    // [SerializeField]
    // private TMP_Text cohesionText;

    //     [HideInInspector]
    // private BoidHelper birdHelper;



    // private Vector3 cohesion;

    // private float alignMent;

    // private List<GameObject> birds = new List<GameObject>();

    // private int birdCount = 0;

    // private void Start()
    // {
    //     StartCoroutine(CalculateAlignment());
    //     StartCoroutine(CalculateCohesion());
    // }

    // void Update()
    // {
    //     if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         CreateBird();
    //     }

    //     if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         DestroyAllBirds();
    //     }

    //     if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3))
    //     {
    //         DestroyLastBird();
    //     }

    //     if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha4))
    //     {
    //         CreateBird();
    //     }

    //     birdCountText.text = birds.Count.ToString();

    //     alignmentText.text = alignMent.ToString();

    //     cohesionText.text = cohesion.ToString();
    // }

    // public void DestroyLastBird()
    // {
    //     if (birds.Count > 0)
    //     {
    //         //Don't forget to remove the bird and it's behaviour from their respective lists, otherwise we might run into issues when trying to access them later on.
    //         GameObject lastBird = birds[birds.Count - 1];
    //         birds.RemoveAt(birds.Count - 1);
    //         Destroy(lastBird);
    //     }
    //     birdCount = birds.Count;
    // }

    // public void DestroyAllBirds()
    // {
    //     foreach (GameObject bird in birds)
    //     {
    //         Destroy(bird);
    //     }
    //     birds.Clear();

    //     birdCount = 0;
    // }

    // public void DestroyBirdByIndex(int index)
    // {
    //     if (index >= 0 && index < birds.Count)
    //     {
    //         GameObject birdToDestroy = birds[index];
    //         birds.RemoveAt(index);
    //         Destroy(birdToDestroy);
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Index {index} is out of range.");
    //     }
    // }

    // public void CreateBird()
    // {
    //     GameObject newBird = Instantiate(boidPrefab, Vector3.zero, Quaternion.identity);

    //     // Generate the bird name
    //     string birdName = "Bird_" + birdCount;
    //     birdCount++;

    //     // Set the GameObject name in the hierarchy
    //     newBird.name = birdName;

    //     // Get the BoidBehaviour component
    //     BirdBehaviour bird = newBird.GetComponent<BirdBehaviour>();

    //     // Set the bird's name in the behavior
    //     bird.birdName = name;
    //     bird.birdSpeed = speed;
    //     bird.detectionRange = detectionRange;

    //     // Add the bird to the lists
    //     birds.Add(newBird);

    //     if (randomSpawnLocation)
    //     {
    //         Vector3 spawnPosition = Vector3.zero;

    //         spawnPosition.x = Random.Range(
    //             minBounds.transform.position.x,
    //             maxBounds.transform.position.x
    //         );

    //         spawnPosition.y = Random.Range(
    //             minBounds.transform.position.y,
    //             maxBounds.transform.position.y
    //         );

    //         spawnPosition.z = Random.Range(
    //             minBounds.transform.position.z,
    //             maxBounds.transform.position.z
    //         );

    //         Vector3 spawnRotation = Vector3.zero;

    //         spawnRotation.y = Random.Range(-360, 360);

    //         newBird.transform.position = spawnPosition;
    //         newBird.transform.rotation = Quaternion.Euler(spawnRotation);
    //     }
    // }

    // IEnumerator CalculateAlignment()
    // {

    //     if (birds.Count == 0)
    //     {
    //         yield return new WaitForSeconds(0.5f);
    //         StartCoroutine(CalculateAlignment());
    //         yield break;
    //     }

    //     //Sum each bird's Y axis and divide it by the number of birds to get the average Y axis rotation.
    //     for (int i = 0; i < birds.Count; i++)
    //     {
    //         alignMent += birds[i].transform.rotation.y;
    //     }

    //     alignMent /= birds.Count;

    //     yield return new WaitForSeconds(0.5f);
    //     StartCoroutine(CalculateAlignment());
    // }

    // IEnumerator CalculateCohesion()
    // {

    //     if(birds.Count == 0)
    //     {
    //         yield return new WaitForSeconds(0.5f);
    //         StartCoroutine(CalculateCohesion());
    //         yield break;
    //     }

    //     //Sum each bird's position in the XZ axis and divide it by the number of birds to get the average position.
    //     for (int i = 0; i < birds.Count; i++)
    //     {
    //         cohesion += birds[i].transform.position;
    //     }
    //     cohesion /= birds.Count;
    //     yield return new WaitForSeconds(0.5f);
    //     StartCoroutine(CalculateCohesion());
    // }
}
