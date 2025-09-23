using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Recyclable") || collision.gameObject.CompareTag("Block" )) 
        {
            Debug.Log("Collision contre le mur"+ collision.gameObject.name);
            collision.transform.position = respawnPoint.position;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
            Debug.Log("Objet recyclable renvoy� au spawn" + collision.gameObject.tag);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
