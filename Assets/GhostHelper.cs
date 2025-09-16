using UnityEngine;

public class GhostHelperFollowBlock : MonoBehaviour
{
    public float detectRadius = 3f;     // distance à laquelle le fantôme détecte le bloc
    public float pushForce = 50f;       // force appliquée sur le bloc
    public float followSpeed = 2f;      // vitesse de déplacement du fantôme
    public LayerMask blockLayer;        // layer du bloc

    private Transform player;
    private Transform targetBlock;

    void Start()
    {
        // trouver le joueur dynamiquement même après spawn
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        else Debug.LogError("Player non trouvé !");
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // détecter le bloc dans le rayon
        Collider2D blockCollider = Physics2D.OverlapCircle(transform.position, detectRadius, blockLayer);
        if (blockCollider != null)
        {
            targetBlock = blockCollider.transform;

            Rigidbody2D rb = blockCollider.attachedRigidbody;
            if (rb != null)
            {
                Vector2 dir = ((Vector2)blockCollider.transform.position - (Vector2)player.position).normalized;
                rb.AddForce(dir * pushForce, ForceMode2D.Force);
            }

            // déplacement du fantôme pour l'effet visuel
            Vector3 movePos = Vector3.MoveTowards(transform.position, targetBlock.position + Vector3.back, followSpeed * Time.fixedDeltaTime);
            transform.position = movePos;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
