using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    Camera cam;

    [Header("Inscribed")]
    public float spawnRate = 2f;
    public float startDelay = 1f;
    public GameObject[] shapeArray;
    public Vector3 startPoint = new Vector3(15f, -5f, -20f);
    public Vector3 offset = new Vector3(15f, 10f, 0f);

    void Start()
    {
        // Set the camera
        cam = GetComponent<Camera>();
        //spawn function
        InvokeRepeating("SpawnShape", startDelay, spawnRate);
    }

    void SpawnShape()
    {
        // Get the position of the camera
        Vector3 viewPos = cam.transform.position;
        viewPos.z = offset.z;

        //set spawning point
        Vector3 spawnPoint = startPoint;
        spawnPoint.x = viewPos.x;
        spawnPoint += offset;

        //set random y-plane spawn point
        spawnPoint.y += Random.Range(-5, 5);


        //get random number
        int randomShape = Random.Range(0, shapeArray.Length);
        //spawn random shape
        GameObject shape = Instantiate<GameObject>(shapeArray[randomShape], spawnPoint, Quaternion.identity);
        print("Shape spawned.");
    }
}
