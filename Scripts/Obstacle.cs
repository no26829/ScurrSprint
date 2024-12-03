using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    // camera loaction tracker for obstacle deleting
    Camera cam;

    // varaiables for Boulder spawn and control;
    // public GameObject boulder = boulderPrefab;
    [Header("Inscribed")]
    public float minY = -5.7f;
    public float maxY = 2f;
    public float size = 2f;
    

    // private Rigidbody rb; // for if we want rolling bolders or for them to interact with env.

    // Start is called before the first frame update
    void Start()
    {
        /* Here if we want Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = false; */
        cam = Camera.main;
    }

    // Update function for keeping tack of obstacle deletion
    private void Update()
    {
       
        // get current camera x pos
        float CamX = cam.transform.position.x;

        // see if obstacle is outside of camera view
        if (CamX - 30f > transform.position.x)
        {

            // despawn obstacle
            Destroy(gameObject);

        }

    }

}
