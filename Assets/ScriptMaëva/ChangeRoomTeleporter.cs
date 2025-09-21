using UnityEngine;



public class ChangeRoomTeleporter : MonoBehaviour
{
    public Transform teleportPoint;
    public Transform player;
    public Transform cam;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.transform;
            Vector2 targetPos = new Vector2(teleportPoint.transform.position.x, teleportPoint.transform.position.y);
            switch (gameObject.name)
            {
                case "CenterToLeft": 
                    cam.transform.position = new Vector2 (-25, 0);
                    break;

                case "CenterToRight":
                    cam.transform.position = new Vector2(25, 0);
                    break;
                
                default:
                    cam.transform.position = new Vector2(0, 0);
                    break;
                    
            }
            player.position = targetPos;
        }
    }
}