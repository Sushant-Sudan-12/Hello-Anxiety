using UnityEngine;

public class SpotLightIndicator : MonoBehaviour
{
    private AnxietySystem anxietySystem;

    private void Start()
    {
        // Find the AnxietySystem in the scene
        anxietySystem = FindObjectOfType<AnxietySystem>();
        
        // Update anxiety state based on initial active state
        UpdateAnxietyState();
    }

    private void OnEnable()
    {
        // When this object is enabled, the player is in darkness
        UpdateAnxietyState();
    }

    private void OnDisable()
    {
        // When this object is disabled, the player is in light
        anxietySystem.SetIlluminateActive(false);
    }

    private void UpdateAnxietyState()
    {
        if (anxietySystem != null)
        {
            anxietySystem.SetIlluminateActive(true); // Player is in darkness
        }
    }
}