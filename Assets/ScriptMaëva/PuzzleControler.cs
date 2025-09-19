using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [Header("Dalles à activer")]
    public PressurePlateWithDoorSprite dalle0; // Caillou
    public PressurePlateWithDoorSprite dalle2; // Caisse

    [Header("Mécanismes")]
    public RotatingObject rotatingObject;      // La barre qui tourne
    public StrechingObject bridge;             // Pont à agrandir

    [Header("Crystal Purple")]
    public TriggerObject crystalTrigger;       // Trigger pour CrystalPurple
    public BraseroController purpleBrasero;   // Brasero + torches violettes

    private bool puzzleDone = false;
    private bool crystalActivated = false;

    private PurpleVoid purpleVoid;

    void Awake()
    {
        purpleVoid = (PurpleVoid)FindFirstObjectByType(typeof(PurpleVoid));
    }

    void Update()
    {
        if (!puzzleDone)
        {
            // Vérifie si les dalles ont un objet dessus
            bool dalle0Active = dalle0.IsOccupied();
            bool dalle2Active = dalle2.IsOccupied();

            if (dalle0Active && dalle2Active)
            {
                puzzleDone = true;

                // Stop rotation
                if (rotatingObject.rotationSpeed > 0)
                {
                    Debug.Log("Active animation StopRotating");
                    rotatingObject.StopRotation();
                }

                // Agrandit le pont
                if (bridge != null && purpleVoid != null)  
                { 
                    bridge.ChangeStretch();
                    purpleVoid.DeActivateCollider();
                }
               
                Debug.Log("Pont déployé, rotation stoppée !");
            }
        }

        // Vérifie si le CrystalPurple a été activé
        if (puzzleDone && !crystalActivated && crystalTrigger != null)
        {
            // On suppose que TriggerObject appelle ActiverBrasero via OnTriggerEnter
            // Rien à faire ici si OnTriggerEnter du CrystalPurple est branché
            // Mais si tu veux, on peut détecter via script :
            // crystalActivated = true; 
            // purpleBrasero.ActiverBrasero(CrystalPurple);
        }
    }
}
