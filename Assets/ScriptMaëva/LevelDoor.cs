using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    public string nextSceneName; // ex : "Level2"
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col != null)
        {  
            col.enabled = false; 
        }
    }

    public void ActivateCollider()
    {
        if (col != null)
        {  
            col.enabled = true;
            Debug.Log("Collider activé");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
