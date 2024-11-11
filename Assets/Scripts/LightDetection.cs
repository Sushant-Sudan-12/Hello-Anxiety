using UnityEngine;

public class TorchColliderDetector : MonoBehaviour
{
    private float enemyStayTime = 0f;  // Time the enemy stays in the trigger
    private GameObject detectedEnemy = null;  // Store the enemy that's detected

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the detected object is an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Torch light detected: " + other.gameObject.name);
            detectedEnemy = other.gameObject;  // Store the enemy
            enemyStayTime = 0f;  // Reset the stay time when enemy enters
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // If we are still detecting an enemy, and it's been in for more than 1 second, destroy it
        if (other.CompareTag("Enemy") && detectedEnemy != null)
        {
            enemyStayTime += Time.deltaTime;  // Increase the stay time

            // Check if the enemy has been in the light's range for more than 1 second
            if (enemyStayTime >= 1.5f)
            {
                Debug.Log("Enemy has been in the light for 1 second, destroying: " + detectedEnemy.name);
                Destroy(detectedEnemy);  // Destroy the enemy
                detectedEnemy = null;  // Reset the detected enemy
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset when the enemy exits the trigger area
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Torch light stopped detecting: " + other.gameObject.name);
            detectedEnemy = null;  // Reset the detected enemy
            enemyStayTime = 0f;  // Reset the timer
        }
    }
}

