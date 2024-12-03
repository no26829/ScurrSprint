using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the chicken's Transform
    public Vector3 offset = new Vector3(0, 5, -10); // Adjusted offset for better jump tracking
    public float followSpeed = 5f;  // Speed at which the camera follows the target
    public float movingSpeed = 1;   // Speed at which the camera moves constantly to the right

    Camera cam;

    void Start()
    {
        // Set the camera
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // If the chicken exists
        if (target != null)
        {
            // Get the position of the chicken relative to the camera
            Vector3 viewPos = cam.WorldToViewportPoint(target.position);

            // If past the left side of the screen
            if (viewPos.x < 0F)
            {
                // Replace later with function that actually shows end game screen
                print("You have lost the game!");
            }
        }
    }

    private void LateUpdate()
    {
        // Update camera position to follow the chicken's position with an offset
        if (target != null)
        {
            // Get the position of the chicken relative to the camera
            Vector3 viewPos = cam.WorldToViewportPoint(target.position);

            // Only if on the right
            if (viewPos.x > 0.5F)
            {
                // Smoothly follow the target's position plus the offset
                Vector3 desiredPosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
            else
            {
                // Continually move camera right
                transform.position += Vector3.right * Time.deltaTime * movingSpeed;
            }
        }
    }
}
