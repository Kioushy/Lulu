using UnityEngine;

public class GameStartup : MonoBehaviour
{
    void Awake()
    {
        // R�cup�re la r�solution d�finie dans Unity Player Settings
        int defaultWidth = 1920;
        int defaultHeight = 1080;
        bool defaultFullscreen = true;

        // Force au d�marrage (Unity va appliquer �a m�me si l�OS a gard� autre chose en m�moire)
        Screen.SetResolution(defaultWidth, defaultHeight, defaultFullscreen);
    }
}
