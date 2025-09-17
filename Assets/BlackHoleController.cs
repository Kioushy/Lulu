using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer holeSprite;    // sprite du trou (invisible au départ)
    public SpriteRenderer coverSprite;   // sprite "vue de dessus" quand le trou est bouché

    [Header("Respawn")]
    public Transform respawnPoint;       // où renvoyer le player

    [Header("Cover settings")]
    [Range(0.5f, 1f)]
    public float coverThreshold = 0.9f;  // % du trou à couvrir pour valider (0.9 = 90%)

    bool isRevealed = false;
    bool isCovered = false;
    Collider2D holeCollider;

    void Start()
    {
        if (holeSprite != null) holeSprite.enabled = false;
        if (coverSprite != null) coverSprite.enabled = false;

        holeCollider = GetComponent<Collider2D>();
        if (holeCollider == null)
            Debug.LogError("[BlackHole] No Collider2D found on hole object. Add one and set IsTrigger = true.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCovered) return;

        // Player : révéler le trou et respawn
        if (other.CompareTag("Player"))
        {
            if (!isRevealed && holeSprite != null)
            {
                holeSprite.enabled = true;
                isRevealed = true;
            }

            if (respawnPoint != null)
            {
                other.transform.position = respawnPoint.position;
                Rigidbody2D rb = other.attachedRigidbody;
                if (rb != null) rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isCovered) return;

        // Box : on teste recouvrement progressif
        if (other.CompareTag("Box"))
        {
            if (holeCollider == null) return;

            Bounds hb = holeCollider.bounds;
            Bounds bb = other.bounds;

            // intersection rectangle
            float xMin = Mathf.Max(hb.min.x, bb.min.x);
            float xMax = Mathf.Min(hb.max.x, bb.max.x);
            float yMin = Mathf.Max(hb.min.y, bb.min.y);
            float yMax = Mathf.Min(hb.max.y, bb.max.y);

            float overlapW = Mathf.Max(0f, xMax - xMin);
            float overlapH = Mathf.Max(0f, yMax - yMin);
            float overlapArea = overlapW * overlapH;

            float holeArea = hb.size.x * hb.size.y;
            float coverage = holeArea > 0f ? overlapArea / holeArea : 0f;

            // debug utile pour voir pourquoi ça ne valide pas
            Debug.Log($"[BlackHole] coverage={coverage:F2} (overlap={overlapArea:F3}, holeArea={holeArea:F3}) for box {other.gameObject.name}");

            if (coverage >= coverThreshold)
            {
                // validé : on bouche le trou
                isCovered = true;

                if (coverSprite != null) coverSprite.enabled = true;
                if (holeSprite != null) holeSprite.enabled = false;

                // détruire la box (ou SetActive(false) si tu préfères)
                Destroy(other.gameObject);
            }
        }
    }
}
