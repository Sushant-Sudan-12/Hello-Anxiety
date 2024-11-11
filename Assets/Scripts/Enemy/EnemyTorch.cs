using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // This will trigger when the enemy enters the collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Torch"))
        {
            // Check if the collider is from the torch
            Debug.Log("Enemy entered the torch range!");
            DeactivateEnemy();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Torch"))
        {
            Debug.Log("Enemy staying in the torch range!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Torch"))
        {
            Debug.Log("Enemy exited the torch range!");
        }
    }

    // Function to deactivate the enemy
    private void DeactivateEnemy()
    {
        gameObject.SetActive(false); // Deactivate the enemy
        Debug.Log("Enemy deactivated.");
    }
}
