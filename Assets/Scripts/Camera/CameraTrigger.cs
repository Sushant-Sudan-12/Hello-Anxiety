using UnityEngine;
public class ExampleTrigger : MonoBehaviour
{
    public CameraMovementWithFade cameraMovement; // Reference to the CameraMovementWithFade script
    public GameObject light;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the player enters the trigger zone
        {
            cameraMovement.StartCameraMovement(); // Start the camera movement and fade
            light.SetActive(false);
        }
    }
}
