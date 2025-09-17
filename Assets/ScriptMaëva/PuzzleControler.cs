using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [Header("Dalles � activer")]
    public PressurePlateWithDoorSprite dalle0; // Caillou
    public PressurePlateWithDoorSprite dalle2; // Caisse

    [Header("M�canismes")]
    public RotatingObject rotatingObject;      // La barre qui tourne
    public StrechingObject bridge;             // Pont � agrandir

    [Header("Crystal Purple")]
    public TriggerObject crystalTrigger;       // Trigger pour CrystalPurple
    public BraseroController purpleBrasero;   // Brasero + torches violettes

    private bool puzzleDone = false;
    private bool crystalActivated = false;

    void Update()
    {
        if (!puzzleDone)
        {
            // V�rifie si les dalles ont un objet dessus
            bool dalle0Active = dalle0.IsOccupied();
            bool dalle2Active = dalle2.IsOccupied();

            if (dalle0Active && dalle2Active)
            {
                puzzleDone = true;

                // Stop rotation
                if (rotatingObject != null)
                    rotatingObject.enabled = false;

                // Agrandit le pont
                if (bridge != null)
                    bridge.ChangeStretch();

                Debug.Log("Pont d�ploy�, rotation stopp�e !");
            }
        }

        // V�rifie si le CrystalPurple a �t� activ�
        if (puzzleDone && !crystalActivated && crystalTrigger != null)
        {
            // On suppose que TriggerObject appelle ActiverBrasero via OnTriggerEnter
            // Rien � faire ici si OnTriggerEnter du CrystalPurple est branch�
            // Mais si tu veux, on peut d�tecter via script :
            // crystalActivated = true; 
            // purpleBrasero.ActiverBrasero(CrystalPurple);
        }
    }
}
