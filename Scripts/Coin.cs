using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // camera loaction tracker for coin deleting
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        // get current camera x pos
        float CamX = cam.transform.position.x;

        // see if coin is outside of camera view
        if (CamX - 30f > transform.position.x)
        {

            // despawn coin
            Destroy(gameObject);

        }
    }
}
