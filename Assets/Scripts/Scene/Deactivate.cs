using UnityEngine;
using System.Collections;

public class DeactivateOnTrigger : MonoBehaviour
{
    public string triggeringTag = "Player"; // Tag of the object that will trigger the deactivation

    // This function will be called when another collider enters the trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided has the correct tag
        if (collision.CompareTag(triggeringTag))
        {
            // Start coroutine to deactivate after 1 second
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    // Coroutine to deactivate the GameObject after a 1-second delay
    private IEnumerator DeactivateAfterDelay()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Deactivate this GameObject
        gameObject.SetActive(false);
    }
}
