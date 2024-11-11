using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public float fadeDuration = 1f;
    public float waitAfterDialogue = 5f;
    public DialogueManager dialogueManager;
    public string[] exitDialogueLines;

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
        if (isDialogueComplete && dialogueManager.IsDialogueComplete())
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIFade.instance.FadeToBlack();
            StartCoroutine(TriggerDialogueSequence());
        }
    }

    IEnumerator TriggerDialogueSequence()
    {
        yield return new WaitForSeconds(fadeDuration);
        dialogueManager.StartDialogue(exitDialogueLines);
        while (!dialogueManager.IsDialogueComplete())
        {
            yield return null;
        }
        isDialogueComplete = true;
        yield return new WaitForSeconds(waitAfterDialogue);
    }

    IEnumerator FadeAndLoadScene()
    {
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(areaToLoad);
    }
}


