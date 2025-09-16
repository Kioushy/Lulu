using UnityEngine;

public class GhostPushCoop : MonoBehaviour
{
    [Header("Suivi du joueur")]
    public Vector3 offset = new Vector3(-0.5f, 0, 0);
    public float followSpeed = 3f;

    [Header("Bloc � pousser")]
    public float detectRadius = 2f;       // distance pour "aider" le joueur
    public float moveSpeed = 2f;          // vitesse de d�placement du bloc
    public LayerMask blockLayer;

    private Transform player;

    void Start()
    {
        // trouver le joueur automatiquement
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        else Debug.LogError("Player non trouv� !");
    }

    void FixedUpdate()
    {
        if (player == null) return;

        //  le fant�me suit le joueur
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);

        //  d�tecter un bloc proche DU JOUEUR
        Collider2D blockCollider = Physics2D.OverlapCircle(player.position, detectRadius, blockLayer);
        if (blockCollider != null)
        {
            // v�rifier si le joueur TOUCHE le bloc
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null && blockCollider.IsTouching(playerCollider))
            {
                // prendre la direction d'entr�e du joueur (input)
                Vector2 playerDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

                // d�placer le bloc dans cette direction
                Rigidbody2D rb = blockCollider.attachedRigidbody;
                if (rb != null)
                {
                    Vector2 newPos = rb.position + playerDir * moveSpeed * Time.fixedDeltaTime;
                    rb.MovePosition(newPos);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(player.position, detectRadius);
        }
    }
}
