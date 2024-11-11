using UnityEngine;
using UnityEngine.UI;

public class TorchController : MonoBehaviour
{
    public Transform torchLight;  
    public GameObject torch;  
    public GameObject mainLight; 
    public Slider batterySlider; 

    public float maxBatteryLife = 100f;  
    public float batteryDrainRate = 10f;  
    public float maxTorchDistance = 10f;  

    private float currentBatteryLife; 

    // Torch's Collider (child object)
    private Collider2D torchCollider;

    private void Start()
    {
        currentBatteryLife = maxBatteryLife;  
        batterySlider.maxValue = maxBatteryLife;  
        batterySlider.value = currentBatteryLife;  

        // Get the torch collider (assuming it's the first child with Collider2D)
        torchCollider = torch.GetComponentInChildren<Collider2D>();

        if (torchCollider == null)
        {
            Debug.LogError("Torch doesn't have a Collider2D child!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && currentBatteryLife > 0)  
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            Vector2 direction = (mousePosition - torchLight.position).normalized;  
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;  
            torchLight.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  

            currentBatteryLife -= batteryDrainRate * Time.deltaTime;
            if (currentBatteryLife < 0) currentBatteryLife = 0;

            batterySlider.value = currentBatteryLife;

            torch.SetActive(currentBatteryLife > 0);
            // Activate the torch collider when the torch is active
            if (torch.activeSelf && torchCollider != null)
            {
                torchCollider.enabled = true; // Enable the collider to detect enemies
            }
        }
        else
        {
            torch.SetActive(false);
            if (torchCollider != null)
            {
                torchCollider.enabled = false; // Disable the collider when the torch is off
            }
        }
    }

    public void IncreaseBatteryLife(float amount)
    {
        currentBatteryLife += amount;
        if (currentBatteryLife > maxBatteryLife)
        {
            currentBatteryLife = maxBatteryLife;
        }
        batterySlider.value = currentBatteryLife;
    }

    // Detect enemies entering the torch range (trigger area)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detected by torch!");
            // You can deactivate or apply effects to the enemy here.
            other.gameObject.SetActive(false); // Example of deactivating enemy on detection
        }
    }

    // Detect enemies exiting the torch range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy left torch range.");
        }
    }
}
