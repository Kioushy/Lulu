using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthSlider;


    ////[Header("Game Manager")]
    //////public SceneGameManager gameManager; // Référence au SceneGameManager

    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        //    //// Si pas assigné dans l'inspecteur, chercher automatiquement
        //    //if (gameManager == null)
        //    //    gameManager = Object.FindFirstObjectByType<SceneGameManager>();
    }

    public void TakeDamage(float amount, Transform respawn)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die(respawn);
    }

    private void Die(Transform respawn)
    {
        Debug.Log("Player died!");
        if (respawn != null)
        {
            gameObject.transform.position = respawn.position;
        }
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;

        //Destroy(gameObject);


        // Déclenche Game Over via SceneGameManager
        //if (gameManager != null)
        //    gameManager.TriggerGameOver(gameManager.ZoneKiller);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // Zone de mort instantanée
    //    if (collision.CompareTag("InstantDeathZone"))
    //    {
    //        currentHealth = 0;
    //        if (healthSlider != null)
    //            healthSlider.value = currentHealth;

    //        Die();
    //    }

    //    // Zone de dégâts progressifs
    //    if (collision.CompareTag("DamageZone"))
    //    {
    //        StartCoroutine(DamageOverTime(10f));
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("DamageZone"))
    //    {
    //        StopAllCoroutines();
    //    }
    //}

    //private System.Collections.IEnumerator DamageOverTime(float damagePerSecond)
    //{
    //    while (true)
    //    {
    //        TakeDamage(damagePerSecond * Time.deltaTime);
    //        yield return null;
    //    }
    //}
}
