using UnityEngine;

public class PressurePlateWithDoorSprite : MonoBehaviour
{
    [Header("Dalle")]
    public Sprite spriteOut;   // dalle non activ�e
    public Sprite spriteIn;    // dalle activ�e

    [Header("Porte")]
    public GameObject door;         // GameObject porte
    public Sprite doorClosed;       // sprite porte ferm�e
    public Sprite doorOpened;       // sprite porte ouverte

    private SpriteRenderer srPlate;
    private SpriteRenderer srDoor;
    private int blockCount = 0;

    void Start()
    {
        srPlate = GetComponent<SpriteRenderer>();
        srPlate.sprite = spriteOut;

        if (door != null)
        {
            srDoor = door.GetComponent<SpriteRenderer>();
            if (srDoor != null) srDoor.sprite = doorClosed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            blockCount++;
            srPlate.sprite = spriteIn;

            if (srDoor != null) srDoor.sprite = doorOpened;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            blockCount--;
            if (blockCount <= 0)
            {
                srPlate.sprite = spriteOut;

                if (srDoor != null) srDoor.sprite = doorClosed;
            }
        }
    }
}
