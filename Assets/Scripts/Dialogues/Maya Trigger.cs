using UnityEngine;
using System.Collections;

public class TriggerDialogue1 : MonoBehaviour
{
    public DialogueManager dialogueManager;  
    [TextArea(3, 10)] 
    public string[] dialogueLines;  
    public Animator anim;
    public IsometricController playerController; // Assign in the inspector
    public GameObject Mary;
    public GameObject MaryLight;
    public GameObject light;


    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(StartDialogue(other));
        }
    }

    private IEnumerator StartDialogue(Collider2D player)
    {
        // Disable player movement
        if (playerController != null)
        {
            anim.SetBool("isMove", false);
            playerController.enabled = false;
        }

        dialogueManager.StartDialogue(dialogueLines);

        // Wait until the dialogue is complete
        while (!dialogueManager.IsDialogueComplete())
        {
            yield return null; // Wait for the next frame
        }

        // Re-enable player movement after dialogue ends
        if (playerController != null)
        {
            // anim.SetBool("isMove", true);
            playerController.enabled = true;
            light.SetActive(true);
            MaryLight.SetActive(false);
            Mary.SetActive(false);
        }
    }
}

