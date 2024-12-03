using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapeMovement : MonoBehaviour
{
    public float m_Speed = 2.0f;
    public Vector3 direction = new Vector3(-1f, -1f, 0f);
    
    Rigidbody m_Rigidbody;
    

    void Start()
    {
        //Fetch the RigidBody you attach to the GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Move the RigidBody diagonal at the speed you define
        m_Rigidbody.velocity = direction * m_Speed;


        // If below the bottom of the screem
        if (gameObject.transform.position.y < -10F)
        {
            // Destroys the object
            print("Shape destroyed.");
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Shape Hit! Transitioning to Failing Scene...");
        SceneManager.LoadScene("Failing");
    }


}
