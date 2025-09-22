using UnityEngine;

public class SceneIntroDialogue : MonoBehaviour
{
    public DialogueData introDialogue; // ton ScriptableObject
    private DialogueManager manager;

    void Awake()
    {
        manager = FindFirstObjectByType<DialogueManager>();
    }

    void Start()
    {
        if (manager != null && introDialogue != null)
        {
            manager.StartDialogue(introDialogue);
        }
    }
}
