using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // For Light2D
using UnityEngine.UI;                 // For UI

public class IlluminateController1 : MonoBehaviour
{
    public GameObject illuminate;
    public Light2D[] lights;
    public float fadeDuration = 3f;
    public float initialFalloff = 0.1f;
    public float finalFalloff = 1f;
    public float stableDuration = 2f;
    public GameObject mainlight;

    private bool isOperationRunning = false;

    // Spotlight management
    public int maxSpotlights = 4; // Maximum number of spotlights the player can use
    private int currentSpotlights;
    public Text spotlightCounterText; // Reference to the UI text element

    private void Start()
    {
        // Initialize the number of spotlights at the start
        currentSpotlights = maxSpotlights;
        UpdateSpotlightUI();
    }

    private void Update()
    {
        // Activate spotlight if space is pressed and operation is not running, and we have available spotlights
        if (Input.GetKeyDown(KeyCode.Space) && !isOperationRunning && currentSpotlights > 0)
        {
            // Use a spotlight and start the illumination process
            UseSpotlight();
            mainlight.SetActive(false);
            StartCoroutine(HandleLights());
        }
    }

    // Coroutine to handle the spotlight effect
    private IEnumerator HandleLights()
    {
        isOperationRunning = true;
        illuminate.SetActive(true);

        // Keep the light on for the stable duration
        yield return new WaitForSeconds(stableDuration);

        float elapsedTime = 0f;

        // Fade the lights over time
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            foreach (var light in lights)
            {
                light.falloffIntensity = Mathf.Lerp(initialFalloff, finalFalloff, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        illuminate.SetActive(false);

        // Reset the falloff intensity for the next use
        foreach (var light in lights)
        {
            light.falloffIntensity = initialFalloff;
        }

        isOperationRunning = false;
        mainlight.SetActive(true);
    }

    // Method to use a spotlight and update the UI
    private void UseSpotlight()
    {
        if (currentSpotlights > 0)
        {
            currentSpotlights--; // Decrease the available spotlights
            UpdateSpotlightUI();  // Update the UI with the new count
        }
        else
        {
            Debug.Log("No more spotlights available!");
        }
    }

    // Method to replenish spotlights (e.g., when picking up an item)
    public void ReplenishSpotlight(int amount)
    {
        currentSpotlights = Mathf.Clamp(currentSpotlights + amount, 0, maxSpotlights);
        UpdateSpotlightUI();
    }

    // Update the UI text to reflect the current spotlight count
    private void UpdateSpotlightUI()
    {
        spotlightCounterText.text = "x" + currentSpotlights.ToString();
    }
}
