using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public Image avatarImage;
    public TextMeshProUGUI dialogueText;
    
    private DialogueData currentDialogue;
    private int currentIndex;
    private bool isActive = false;
    
    
    void Awake()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue (DialogueData dialogue)
    {
        currentDialogue = dialogue;
        currentIndex = 0;
        isActive = true;
        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        if (currentDialogue != null && currentIndex < currentDialogue.Lines.Length)
        {
            DialogueLine line = currentDialogue.Lines[currentIndex];
            avatarImage.sprite = line.avatar;
            dialogueText.text = line.text;
        }
        else
        {
            EndDialogue();
        }
    }
    public void NextLine()
    {
        currentIndex++;
        ShowLine();
    }

    void EndDialogue()
    {
        isActive = false;
        dialoguePanel.SetActive(false);
    }

      public bool IsActive()
    {
        return isActive;
    }

}