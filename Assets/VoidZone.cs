using UnityEngine;

public class VoidZone : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform playerRespawnPoint;



    private void OnTriggerEnter2D(Collider2D other)

    {
        //Respawnable2D resp = other.GetComponent<Respawnable2D>();
        //if (resp != null)
        //{
        //    Debug.Log("[VoidZone] Respawn " + other.name); // DEBUG
        //    resp.Respawn();
        //}
        //else
        //{
        //    Debug.Log("[VoidZone] Aucun Respawnable2D trouvé sur " + other.name);
        //}
        if (other.CompareTag("Player"))
        {
           other.transform.position = playerRespawnPoint.position;
        }
        if (other.CompareTag("Box"))
        {
            other.transform.position = other.transform.parent.position;
        }
    }
}
