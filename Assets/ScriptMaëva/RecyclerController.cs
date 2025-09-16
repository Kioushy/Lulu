using UnityEngine;
using UnityEngine.Events;

public class RecyclerController : MonoBehaviour
{
    public int requirementCount = 1; // je veux 6 os Tag 'Recyclable'
    public int currentCount = 0; // je commence avec 0 os
    public UnityEvent<GameObject> spawnObjectEvent;

    public GameObject spawner; // je remet la référence du spawner 

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (other.CompareTag("Recyclable"))
            { Destroy(other.gameObject); 
            currentCount++;
            
            }
        if (currentCount >= requirementCount && spawner != null)
            {
            spawnObjectEvent?.Invoke(gameObject);
            Debug.Log("Petit Skeleton apparu");
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
