using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer holeSprite;
    public SpriteRenderer coverSprite;

    [Header("Cover settings")]
    [Range(0.5f, 1f)]
    public float coverThreshold = 0.9f;

    [Header("Respawn Settings")]
    public Transform playerRespawnPoint; // <- spécifique à CE trou

    bool isRevealed = false;
    bool isCovered = false;
    Collider2D holeCollider;

    void Start()
    {
        if (holeSprite != null) holeSprite.enabled = false;
        if (coverSprite != null) coverSprite.enabled = false;

        holeCollider = GetComponent<Collider2D>();
        if (holeCollider == null)
            Debug.LogError("[BlackHole] Missing Collider2D (set as Trigger).");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCovered) return;

        // PLAYER
        if (other.CompareTag("Player"))
        {
            if (!isRevealed && holeSprite != null)
            {
                holeSprite.enabled = true;
                isRevealed = true;
            }
            other.transform.position = playerRespawnPoint.position;

            //Respawnable2D resp = other.GetComponent<Respawnable2D>();
            //if (resp != null)
            //{
            //    if (playerRespawnPoint != null)
            //        resp.RespawnAt(playerRespawnPoint); // respawn spécifique à ce trou
            //    else
            //        resp.Respawn(); // respawn global
            //}
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isCovered) return;

        if (other.CompareTag("Box"))
        {
            if (holeCollider == null) return;

            Bounds hb = holeCollider.bounds;
            Bounds bb = other.bounds;

            float xMin = Mathf.Max(hb.min.x, bb.min.x);
            float xMax = Mathf.Min(hb.max.x, bb.max.x);
            float yMin = Mathf.Max(hb.min.y, bb.min.y);
            float yMax = Mathf.Min(hb.max.y, bb.max.y);

            float overlapW = Mathf.Max(0f, xMax - xMin);
            float overlapH = Mathf.Max(0f, yMax - yMin);
            float overlapArea = overlapW * overlapH;

            float holeArea = hb.size.x * hb.size.y;
            float coverage = holeArea > 0f ? overlapArea / holeArea : 0f;

            if (coverage >= coverThreshold)
            {
                isCovered = true;

                if (coverSprite != null) coverSprite.enabled = true;
                if (holeSprite != null) holeSprite.enabled = false;

                Destroy(other.gameObject); // la box bouche le trou
            }
        }
    }
}
