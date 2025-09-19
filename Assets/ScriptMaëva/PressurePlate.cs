using UnityEngine;

public class PressurePlateWithDoorSprite : MonoBehaviour
{
    [Header("Dalle")]
    public Sprite spriteOut;   // dalle non activée
    public Sprite spriteIn;    // dalle activée

    [Header("Porte")]
    public Sprite doorClosed;       // sprite porte fermée
    public Sprite doorOpened;       // sprite porte ouverte

    private LevelDoor door;
    private SpriteRenderer srPlate;
    private SpriteRenderer srDoor;
    private int blockCount = 0;

    public void Awake()
    {
        door = (LevelDoor)FindFirstObjectByType(typeof(LevelDoor));
        Debug.Log(door);

        if (door != null)
        {
            srDoor = door.GetComponent<SpriteRenderer>();
        }
        
    }

    void Start()
    {
        srPlate = GetComponent<SpriteRenderer>();
        srPlate.sprite = spriteOut;

        //if (door != null)
        //{
        //    srDoor = door.GetComponent<SpriteRenderer>();
        //    if (srDoor != null) srDoor.sprite = doorClosed;
        //}

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            blockCount++;
            srPlate.sprite = spriteIn;

            if (srDoor != null && door != null)
            {
                door.ActivateCollider();
                srDoor.sprite = doorOpened;
            }
        }
    }


    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Block"))
    //    {
    //        blockCount--;
    //        if (blockCount <= 0)
    //        {
    //            srPlate.sprite = spriteOut;

    //            if (srDoor != null) srDoor.sprite = doorClosed;
    //        }
    //    }
    //}
    public bool IsOccupied()
    {
        return blockCount > 0;
    }
}
