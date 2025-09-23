using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public int damage;
    public PlayerHealth health;
    
    [Header("Respawn")]
    public Transform respawnPoint;

    public bool canDamage = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Saw") && collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(damage, respawnPoint);
        }
       
        if (canDamage && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Joueur prend des d�gats");
            health.TakeDamage(damage, respawnPoint);
            //canDamage = false;
        }
    }

    // Appel� par un Animation Event dans l'Animator
    public void EnableDamage()
    {
        canDamage = true;
    }

    // Appel� par un autre Event (quand le spike est rentr�)
    public void DisableDamage()
    {
        canDamage = false;
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
       
    //    //if (canDamage && collision.gameObject.CompareTag("Player"))
    //    //{
    //    //    Debug.Log("Joueur prend des d�gats");
    //    //    health.TakeDamage(damage, respawnPoint);
    //    //    canDamage = false;
    //    //}
        
    //}
}