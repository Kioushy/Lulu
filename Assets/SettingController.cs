using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [Header("UI (si tu veux, link depuis l'inspector)")]
    public GameObject panel;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Button exitButton;

    private List<Resolution> availableResolutions = new List<Resolution>();

    void Awake()
    {
        if (panel == null) panel = gameObject;

        if (resolutionDropdown == null) resolutionDropdown = transform.Find("Resolution/Dropdown")?.GetComponent<Dropdown>();
        if (volumeSlider == null) volumeSlider = transform.Find("Volume/Slider")?.GetComponent<Slider>();
        if (fullscreenToggle == null) fullscreenToggle = transform.Find("Fullscreen/Toggle")?.GetComponent<Toggle>();
        if (exitButton == null) exitButton = transform.Find("Exit")?.GetComponent<Button>();

        PopulateResolutions();
        InitValues();

        if (resolutionDropdown != null) resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        if (volumeSlider != null) volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        if (exitButton != null) exitButton.onClick.AddListener(exitButtonPress);
    }

    void PopulateResolutions()
    {
        resolutionDropdown?.ClearOptions();
        availableResolutions.Clear();

        Resolution[] screenRes = Screen.resolutions;
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

            // Sélectionne la résolution courante
            int curIndex = availableResolutions.FindIndex(r =>
                r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height);
            if (curIndex >= 0) resolutionDropdown.value = curIndex;
        }
    }

    void InitValues()
    {
        // Volume actuel
        if (volumeSlider != null) volumeSlider.value = AudioListener.volume;

        // Fullscreen actuel
        if (fullscreenToggle != null) fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void OnResolutionChanged(int index)
    {
        if (index < 0 || index >= availableResolutions.Count) return;
        Resolution r = availableResolutions[index];
        Screen.SetResolution(r.width, r.height, fullscreenToggle != null ? fullscreenToggle.isOn : Screen.fullScreen);
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }

    public void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        // On réapplique la résolution courante
        if (resolutionDropdown != null && resolutionDropdown.value >= 0 && resolutionDropdown.value < availableResolutions.Count)
        {
            Resolution r = availableResolutions[resolutionDropdown.value];
            Screen.SetResolution(r.width, r.height, isFullscreen);
        }
    }

    public void exitButtonPress()
    {
        gameObject.SetActive(false);
    }
}
