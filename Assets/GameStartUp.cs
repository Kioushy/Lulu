using UnityEngine;

public class GameStartup : MonoBehaviour
{
    void Awake()
    {
        // Récupère la résolution définie dans Unity Player Settings
        int defaultWidth = 1920;
        int defaultHeight = 1080;
        bool defaultFullscreen = true;

        // Force au démarrage (Unity va appliquer ça même si l’OS a gardé autre chose en mémoire)
        Screen.SetResolution(defaultWidth, defaultHeight, defaultFullscreen);
    }
}
