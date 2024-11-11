using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaMouseExit : MonoBehaviour
{
    public string areaToLoad;
    public float fadeDuration = 1f;
    public DialogueManager dialogueManager;
    public string[] exitDialogueLines;
    public float waitAfterDialogue = 1f;

    private bool isDialogueComplete = false;

    void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDialogueComplete)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider == GetComponent<Collider2D>())
            {
                UIFade.instance.FadeToBlack();
                StartCoroutine(TriggerDialogueSequence());
            }
        }
    }

    IEnumerator TriggerDialogueSequence()
    {
        yield return new WaitForSeconds(fadeDuration); // Wait for fade-in
        dialogueManager.StartDialogue(exitDialogueLines); // Start the dialogue

        while (!dialogueManager.IsDialogueComplete())
        {
            yield return null; // Wait until dialogue is done
        }

        yield return new WaitForSeconds(waitAfterDialogue); // Wait after dialogue ends

        StartCoroutine(FadeAndLoadScene()); // Automatically transition to the scene
    }

    IEnumerator FadeAndLoadScene()
    {
        UIFade.instance.FadeToBlack(); // Fade out before loading scene
        yield return new WaitForSeconds(fadeDuration); // Wait for the fade-out duration
        SceneManager.LoadScene(areaToLoad); // Load the new scene
    }
}

