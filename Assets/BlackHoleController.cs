using UnityEngine;
using UnityEngine.UIElements;

public class BlackHoleController : MonoBehaviour
{
    public Transform respawnPoint;          // Point de respawn
    public SpriteRenderer holeSprite;       // Sprite noir du trou
    public SpriteRenderer coverSprite;      // Sprite  afficher quand le trou est couvert

    private bool isCovered = false;         // Si le trou est securise
    private bool activated = false;         // Si le trou a déjà été activé par player/crystal
    private Collider2D holeCollider;

    void Start()
    {
        if (holeSprite != null)
            holeSprite.enabled = false;      // on ne voit pas le trou au départ
        if (coverSprite != null)
            coverSprite.enabled = false;    // couverture invisible au depart

        holeCollider = GetComponent<Collider2D>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isCovered) return;

        // V�rifie si c'est une box
        if (collision.CompareTag("BoxBlocker"))
        {
            Collider2D boxCollider = collision.GetComponent<Collider2D>();
            if (boxCollider != null)
            {
                // V�rifie si la box recouvre compl�tement le trou
                if (holeCollider.bounds.Intersects(boxCollider.bounds))
                {
                    // Ici on peut ajouter un offset pour s'assurer que la box couvre bien
                    float coverage = Mathf.Min(
                        holeCollider.bounds.max.x - boxCollider.bounds.min.x,
                        boxCollider.bounds.max.x - holeCollider.bounds.min.x
                    );

                    // Pour simplifier, si les bounds intersectent suffisamment on consid�re le trou couvert
                    // On peut affiner avec des ratios si n�cessaire
                    if (coverage > 0)
                    {
                        isCovered = true;

                        // Affiche le sprite �couvercle�
                        if (coverSprite != null)
                            coverSprite.enabled = true;

                        // Supprime la box
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCovered) return; // trou bouché

        // Player ou Crystal tombe dans le trou
        if (collision.CompareTag("Player") || collision.CompareTag("CrystalVert"))
        {
           // Active le trou visuellement si pas déjà
            if (!activated && holeSprite != null)
            {
                holeSprite.enabled = true;
                activated = true;
            }

            // Respawn player/crystal
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;

            collision.transform.position = respawnPoint.position;
        }
    }
}