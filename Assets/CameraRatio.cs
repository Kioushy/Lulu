using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoAdjust : MonoBehaviour
{
    [Header("R�glages de base")]
    public float baseOrthographicSize = 7f;   // Taille de r�f�rence pour ton level design
    public float targetAspect = 16f / 9f;     // Ratio de r�f�rence (ex: 16/9)

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void AdjustCamera()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        // Si l��cran est plus �troit que le ratio cible, on ajuste la taille
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
