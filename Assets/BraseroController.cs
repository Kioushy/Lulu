using UnityEngine;
using System.Collections;

public class BraseroController : MonoBehaviour
{
    [Header("Animators")]
    public Animator braseroAnimator;    // Brasero (bool "Green")
    public Animator[] torches;          // Les 3 torches murales

    [Header("Timing")]
    public float torchDelay = 0.6f;

    private bool activated = false;

    public void ActiverBrasero(GameObject other)
    {
        Debug.Log("Activation Brasero");
        if (activated)
        {
            Debug.Log("Activation vaut true");
            return;
        }
        if (other == null)
        {
            Debug.Log("GameObject vide");
            return;
        }

        //if (!other.CompareTag("CrystalVert")) return;:

        activated = true;

        //// D�truit le cristal
        //Destroy(other);

        // Active le brasero (false = allum� dans ton Animator)
        if (braseroAnimator != null)
            Debug.Log("Activation du brassero Animation");
            braseroAnimator.SetBool("Green", true);

        // Lance la s�quence des torches
        if (torches != null && torches.Length > 0)
            Debug.Log("Torches trouv�s");
            StartCoroutine(ActivateTorchesSequence());
    }

    private IEnumerator ActivateTorchesSequence()
    {
        foreach (var torch in torches)
        {
            //if (torch != null)
                torch.SetBool("GobeletGreen", true);

            yield return new WaitForSeconds(torchDelay);
        }
    }
}
