using UnityEngine;
using System.Collections;

public class CameraMovementWithFade : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(-5.5f, 17f, -10f); // Target camera position
    public float targetOrthographicSize = 11f; // Target orthographic size
    public float moveDuration = 2f; // Duration for the camera movement
    public float fadeDuration = 1f; // Duration for the fade effect
    private Vector3 initialPosition; // Initial camera position
    private Camera cameraComponent; // Reference to the Camera component

    void Start()
    {
        initialPosition = transform.position; // Store initial camera position
        cameraComponent = GetComponent<Camera>(); // Get the Camera component
    }

    public void StartCameraMovement()
    {
        StartCoroutine(MoveCameraAndFade());
    }

    private IEnumerator MoveCameraAndFade()
    {
        // Start the fade to black
        UIFade.instance.FadeToBlack();
        
        // Wait for the fade to complete
        yield return new WaitForSeconds(fadeDuration);

        // Move the camera to the target position
        float elapsedTime = 0f;
        float initialOrthographicSize = cameraComponent.orthographicSize; // Store initial orthographic size
        
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration; // Normalize time
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t); // Move camera
            cameraComponent.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, t); // Adjust orthographic size
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure final position and orthographic size are set
        transform.position = targetPosition;
        cameraComponent.orthographicSize = targetOrthographicSize;

        // Fade back from black
        UIFade.instance.FadeFromBlack();
        
        // Wait for the fade to complete
        yield return new WaitForSeconds(fadeDuration);
    }
}