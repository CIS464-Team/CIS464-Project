using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TimelineControl timeLineController;
    public NPCDialogue currentDialogue;
    public NPCDialogue alternateDialogue;
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    private int dialogueIndex;
    private bool isTyping;
    
    void Start()
    {
        if(DebugController.Instance != null && DebugController.Instance.ICheated)
        {
            currentDialogue = alternateDialogue;
        }
    }
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
        bool cheated = DebugController.Instance != null && DebugController.Instance.ICheated;
        
        string[] randomColors = { "#FF0000", "#00FF00", "#0000FF", "#FF00FF", "#FFFF00", "#00FFFF", "#FF6600" };

        foreach (char letter in currentDialogue.dialogueLines[dialogueIndex])
        {
            if (cheated && dialogueIndex == 15)
            {
                string color = randomColors[Random.Range(0, randomColors.Length)];
                dialogueText.text += $"<color={color}>{letter}</color>";
                yield return new WaitForSeconds(currentDialogue.typingSpeed * 0.2f);
            }
            else if (cheated && dialogueIndex >= 5)
            {
                dialogueText.text += $"<color=#FF0000>{letter}</color>";
                yield return new WaitForSeconds(currentDialogue.typingSpeed);
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(currentDialogue.typingSpeed);
            }
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
