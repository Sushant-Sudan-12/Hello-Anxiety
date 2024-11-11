using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public float batteryAmount = 20f;  // The amount of battery to restore
    public IlluminateController1 illuminateController;

    // Only detect the player collisions and ensure TorchController is on a child object of the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // Find the TorchController in the player's child objects
            TorchController torchController = other.GetComponentInChildren<TorchController>();
            illuminateController.ReplenishSpotlight(2);

            if (torchController != null)
            {
                torchController.IncreaseBatteryLife(batteryAmount);  // Increase the battery life
                Destroy(gameObject);  // Destroy the battery pickup after it's collected
            }
        }
    }
}

