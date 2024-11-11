using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;  
    [TextArea(3, 10)] 
    public string[] dialogueLines;  

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            print("gay");
            dialogueManager.StartDialogue(dialogueLines);
        }
    }
}


