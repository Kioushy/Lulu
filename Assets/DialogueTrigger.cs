using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;          // L’asset du PNJ
    public DialogueManager manager;        // Référence vers ton manager

    void Awake()
    {
        manager = FindFirstObjectByType<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (manager != null)
            {
                manager.StartDialogue(dialogue);
            }
            else
                Debug.LogError("DialogueManager introuvable dans la scène !");
        }
    }
}
