using UnityEngine;

public class GhostHelperFollowBlock : MonoBehaviour
{
    public float detectRadius = 3f;     // distance � laquelle le fant�me d�tecte le bloc
    public float pushForce = 50f;       // force appliqu�e sur le bloc
    public float followSpeed = 2f;      // vitesse de d�placement du fant�me
    public LayerMask blockLayer;        // layer du bloc

    private Transform player;
    private Transform targetBlock;

    void Start()
    {
        // trouver le joueur dynamiquement m�me apr�s spawn
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        else Debug.LogError("Player non trouv� !");
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // d�tecter le bloc dans le rayon
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

            // d�placement du fant�me pour l'effet visuel
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
