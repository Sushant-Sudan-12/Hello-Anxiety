using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public GameObject skillTreeMenu; // Reference to the Skill Tree UI object
    private bool isSkillTreeActive = false; // Track if the skill tree menu is active

    void Start()
    {
        // Ensure the skill tree menu starts inactive
        skillTreeMenu.SetActive(false);
    }

    void Update()
    {
        // Check if the "Z" key is pressed to toggle the skill tree menu
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleSkillTree();
        }
    }

    void ToggleSkillTree()
    {
        // Toggle the active state of the skill tree menu
        isSkillTreeActive = !isSkillTreeActive;
        skillTreeMenu.SetActive(isSkillTreeActive);
    }
}

