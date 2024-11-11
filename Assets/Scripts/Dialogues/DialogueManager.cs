using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText; 
    public float dialogueSpeed = 0.05f; 
    public string[] dialogueLines; 
    private int currentLineIndex = 0; 
    public float waitTimeDialogue = 3f; 

    private bool isDialogueActive = false; 
    private Coroutine typingCoroutine;

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space) && !IsTyping())
        {
            ShowNextLine();
        }
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0; 
        isDialogueActive = true;
        ShowNextLine(); 
    }

    private void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeDialogue(dialogueLines[currentLineIndex])); 
            currentLineIndex++; 
        }
        else
        {
            EndDialogue(); 
        }
    }

    IEnumerator TypeDialogue(string line)
    {
        dialogueText.text = ""; 
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c; 
            yield return new WaitForSeconds(dialogueSpeed); 
        }
        yield return new WaitForSeconds(waitTimeDialogue); 
        ShowNextLine();
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueText.text = ""; 
    }

    public bool IsDialogueComplete()
    {
        return !isDialogueActive; 
    }

    private bool IsTyping()
    {
        return typingCoroutine != null; 
    }
}



