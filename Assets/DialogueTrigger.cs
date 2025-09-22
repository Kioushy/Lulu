using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;          // L’asset du PNJ
    public DialogueManager manager;        // Référence vers ton manager

    [Tooltip("Le dialogue ne peut être joué qu'une seule fois")]
    public bool playOnlyOnce = true;
    private bool hasPlayed = false;
    void Awake()
    {
        manager = FindFirstObjectByType<DialogueManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && manager != null)
        {
            if (playOnlyOnce && hasPlayed)
                return;

            manager.StartDialogue(dialogue);

            if (playOnlyOnce)
                hasPlayed = true;
        }
        else
        { 
            Debug.LogError("DialogueManager introuvable dans la scène !");
        }
    }
}
