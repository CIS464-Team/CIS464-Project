using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TimelineControl timeLineController;
    public NPCDialogue currentDialogue;
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    private int dialogueIndex;
    private bool isTyping;
    
    public void StartDialogue()
    {
        dialogueIndex = 0;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue());
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(currentDialogue.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else
        {
            dialogueIndex++;
            if (dialogueIndex < currentDialogue.dialogueLines.Length)
            {
                StartCoroutine(TypeDialogue());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    IEnumerator TypeDialogue()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach (char letter in currentDialogue.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentDialogue.typingSpeed);
        }
        isTyping = false;

        if(currentDialogue.autoProgressLines.Length > dialogueIndex && currentDialogue.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(currentDialogue.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        timeLineController.ResumeTimeline();
    }
}
