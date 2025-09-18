using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [Header("UI (si tu veux, link depuis l'inspector)")]
    public GameObject panel;                // racine (le panel SettingScreen)
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Button exitButton;

    private List<Resolution> availableResolutions = new List<Resolution>();

    // Keys PlayerPrefs
    const string KEY_VOLUME = "pref_volume";
    const string KEY_RES_INDEX = "pref_resolution_index";
    const string KEY_FULLSCREEN = "pref_fullscreen";

    void Awake()
    {
        if (panel == null) panel = gameObject;

        // si non assignés dans l'inspector, on essaye de les trouver par convention
        if (resolutionDropdown == null) resolutionDropdown = transform.Find("Resolution/Dropdown")?.GetComponent<Dropdown>();
        if (volumeSlider == null) volumeSlider = transform.Find("Volume/Slider")?.GetComponent<Slider>();
        if (fullscreenToggle == null) fullscreenToggle = transform.Find("Fullscreen/Toggle")?.GetComponent<Toggle>();
        if (exitButton == null) exitButton = transform.Find("Exit")?.GetComponent<Button>();

        PopulateResolutions();
        LoadSavedValues();

        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        if (exitButton != null) exitButton.onClick.AddListener(exitButtonPress);

        // IMPORTANT : on ne rattache *pas* automatiquement l'action Exit ici,
        // pour éviter de se faire supprimer ce listener depuis d'autres scripts (PauseMenu).
        // On fournit la méthode AssignExitAction pour que MainMenu/PauseMenu assignent l'action correcte.
    }

    void PopulateResolutions()
    {
        resolutionDropdown?.ClearOptions();
        availableResolutions.Clear();

        Resolution[] screenRes = Screen.resolutions; // récupérer les résolutions supportées
        List<string> options = new List<string>();
        for (int i = 0; i < screenRes.Length; i++)
        {
            Resolution r = screenRes[i];
            string option = $"{r.width} x {r.height} @ {r.refreshRate}Hz";
            if (!options.Contains(option))
            {
                options.Add(option);
                availableResolutions.Add(r);
            }
        }

        if (resolutionDropdown != null)
        {
            resolutionDropdown.AddOptions(options);
        }
    }

    void LoadSavedValues()
    {
        // Volume
        float savedVol = PlayerPrefs.GetFloat(KEY_VOLUME, AudioListener.volume);
        AudioListener.volume = savedVol;
        if (volumeSlider != null) volumeSlider.value = savedVol;

        // Fullscreen
        bool savedFS = PlayerPrefs.GetInt(KEY_FULLSCREEN, Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = savedFS;
        if (fullscreenToggle != null) fullscreenToggle.isOn = savedFS;

        // Résolution
        int savedIndex = PlayerPrefs.GetInt(KEY_RES_INDEX, -1);
        if (savedIndex >= 0 && savedIndex < availableResolutions.Count)
        {
            ApplyResolution(savedIndex, savedFS);
            if (resolutionDropdown != null) resolutionDropdown.value = savedIndex;
        }
        else
        {
            // positionner la dropdown sur la résolution courante si possible
            int curIndex = availableResolutions.FindIndex(r => r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height);
            if (curIndex >= 0 && resolutionDropdown != null) resolutionDropdown.value = curIndex;
        }
    }

    public void OnResolutionChanged(int index)
    {
        ApplyResolution(index, fullscreenToggle != null ? fullscreenToggle.isOn : Screen.fullScreen);
        PlayerPrefs.SetInt(KEY_RES_INDEX, index);
        PlayerPrefs.Save();
    }

    void ApplyResolution(int index, bool fullscreen)
    {
        if (index < 0 || index >= availableResolutions.Count) return;
        Resolution r = availableResolutions[index];
        Screen.SetResolution(r.width, r.height, fullscreen);
        // note : SetResolution peut se comporter différemment en éditeur vs build.
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // change le volume global
        PlayerPrefs.SetFloat(KEY_VOLUME, value);
        PlayerPrefs.Save();
    }

    public void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(KEY_FULLSCREEN, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        // réapplique la résolution pour être sûr que le mode soit pris en compte
        ApplyResolution(resolutionDropdown != null ? resolutionDropdown.value : 0, isFullscreen);
    }

    public void exitButtonPress()
    {
        Debug.Log(gameObject);
        gameObject.SetActive(false);
    }

    //// Méthodes utilitaires pour MainMenu/PauseMenu
    //public void OpenPanel() => panel.SetActive(true);
    //public void ClosePanel() => panel.SetActive(false);

    //// Permet à d'autres scripts d'assigner l'action d'Exit (MainMenu: fermer, PauseMenu: revenir au pause)
    //public void AssignExitAction(UnityEngine.Events.UnityAction action)
    //{
    //    if (exitButton != null)
    //    {
    //        //exitButton.onClick.RemoveAllListeners();
    //        //exitButton.onClick.AddListener(action);
    //        gameObject.SetActive(false);
    //    }
    //}
}
