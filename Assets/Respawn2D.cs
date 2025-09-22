using UnityEngine;

public class Respawnable2D : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform customRespawnPoint; // optionnel (par d�faut d�fini au d�part)

    private Vector3 spawnPosition;
    //private Quaternion spawnRotation;

    void Start()
    {
        // Position initiale = backup si pas de point d�fini
        spawnPosition = transform.position;
        //spawnRotation = transform.rotation;
    }

    public void Respawn()
    {
        if (customRespawnPoint != null)
        {
            //Debug.Log("[Respawnable2D] Respawn " + gameObject.name + " vers " + customRespawnPoint.position);
            transform.position = customRespawnPoint.position;
            //transform.rotation = customRespawnPoint.rotation;
        }
        else
        {
            //Debug.Log("[Respawnable2D] Respawn " + gameObject.name + " vers position initiale " + spawnPosition);
            transform.position = spawnPosition;
            //transform.rotation = spawnRotation;
        }

        ResetPhysics();
    }

    // Respawn vers un point impose par la zone
    public void RespawnAt(Transform newPoint)
    {
        transform.position = newPoint.position;
        //transform.rotation = newPoint.rotation;
        ResetPhysics();
    }

    private void ResetPhysics()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
