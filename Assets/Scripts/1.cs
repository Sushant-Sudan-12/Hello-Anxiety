using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading

public class AreaTransitionTrigger : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load
    public string requiredTag = "Player"; // The tag of the object that can trigger the transition

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the correct tag
        if (collision.CompareTag(requiredTag))
        {
            // Trigger the transition to the next scene
            TransitionToNextArea();
        }
    }

    // Function to load the next scene
    void TransitionToNextArea()
    {
        // Check if the next scene name is valid
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set in the Inspector!");
        }
    }
}
