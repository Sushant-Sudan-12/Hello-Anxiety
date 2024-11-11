using UnityEngine;

public class ActivateOnXPress : MonoBehaviour
{
    public GameObject[] objectsToActivate; // Array to hold the GameObjects that activate once
    public GameObject toggleObject; // Separate GameObject that toggles on and off

    private bool areObjectsActive = false; // Track if objects have been activated
    private bool isToggleObjectActive = false; // Track the state of the toggle object

    void Start()
    {
        // Ensure the GameObjects are inactive at the start
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
        // Ensure the toggle object starts inactive
        toggleObject.SetActive(false);
    }

    void Update()
    {
        // Check if X is pressed and objects are not already active
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!areObjectsActive)
            {
                ActivateObjects(); // Activate the objects that only activate once
            }

            ToggleObject(); // Toggle the separate object
        }
    }

    void ActivateObjects()
    {
        // Set each GameObject's active state to true
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
        areObjectsActive = true; // Update the state to indicate objects are now active
    }

    void ToggleObject()
    {
        // Toggle the active state of the separate toggle object
        isToggleObjectActive = !isToggleObjectActive;
        toggleObject.SetActive(isToggleObjectActive);
    }
}



