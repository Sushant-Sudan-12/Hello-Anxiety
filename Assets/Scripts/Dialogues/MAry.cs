using UnityEngine;

public class ActivateOnTriggerEnter : MonoBehaviour
{
    public GameObject objectToActivate; // The GameObject to activate when the trigger is entered

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has a specific tag (optional)
        if (other.CompareTag("Player")) // Change "Player" to your desired tag
        {
            // Activate the specified GameObject
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}
