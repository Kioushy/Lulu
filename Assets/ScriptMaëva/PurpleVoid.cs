using UnityEngine;


public class PurpleVoid : MonoBehaviour
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
  
    public void DeActivateCollider()
    {
        if (col != null)
        {
            col.enabled = false;
        }

    }

}
