using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoAdjust : MonoBehaviour
{
    [Header("Réglages de base")]
    public float baseOrthographicSize = 7f;   // Taille de référence pour ton level design
    public float targetAspect = 16f / 9f;     // Ratio de référence (ex: 16/9)

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void AdjustCamera()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        // Si l’écran est plus étroit que le ratio cible, on ajuste la taille
        if (currentAspect < targetAspect)
        {
            cam.orthographicSize = baseOrthographicSize * (targetAspect / currentAspect);
        }
        else
        {
            cam.orthographicSize = baseOrthographicSize;
        }
    }
}
