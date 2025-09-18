using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pausePanel;
    public GameObject settingsPanel;

    public Button resumeButton;
    public Button settingButton;
    public Button menuButton;
    public Button settingExitButton;

    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    private void Awake()
    {
        
        // Persiste entre les scènes
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) OpenPauseMenu();
            else ResumeGame();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isPaused = false;
        Time.timeScale = 1f;
        
    }

    // Callback optionnel pour actions après chargement
    public void OpenPauseMenu(Action onReady = null)
    {
        if (!SceneManager.GetSceneByName("PauseMenu").isLoaded)
        {
            SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive).completed += (op) =>
            {
                SetupPauseMenu();
                onReady?.Invoke();
            };
        }
        else
        {
            SetupPauseMenu();
            onReady?.Invoke();
        }
    }

    public void SetupPauseMenu()
    {
        Scene pauseScene = SceneManager.GetSceneByName("PauseMenu");
        if (!pauseScene.isLoaded) return;

        //GameObject pauseMenuGameObject = pauseScene.GetRootGameObjects();
        
        //settingsPanel = gameObject.transform.Find("Canvas/MenuPause Panel/Setting/SettingScreen")?.gameObject;

        GameObject[] roots = pauseScene.GetRootGameObjects();
        foreach (GameObject root in roots)
        {
            // Panels
            Debug.Log(root.name);
            pausePanel = root.transform.Find("MenuPause Panel")?.gameObject;
            settingsPanel = root.transform.Find("MenuPause Panel/Setting/SettingScreen")?.gameObject;

            if (pausePanel != null) pausePanel.SetActive(true);
            if (settingsPanel != null) settingsPanel.SetActive(false);

            // Boutons MenuPause
            resumeButton = root.transform.Find("MenuPause Panel/Resume")?.GetComponent<Button>();
            settingButton = root.transform.Find("MenuPause Panel/Setting")?.GetComponent<Button>();
            menuButton = root.transform.Find("MenuPause Panel/Menu")?.GetComponent<Button>();

            resumeButton?.onClick.RemoveAllListeners();
            resumeButton?.onClick.AddListener(ResumeGame);

            settingButton?.onClick.RemoveAllListeners();
            settingButton?.onClick.AddListener(OpenSettings);

            menuButton?.onClick.RemoveAllListeners();
            menuButton?.onClick.AddListener(ReturnToMainMenu);

            // Bouton Setting Exit
            settingExitButton = root.transform.Find("MenuPause Panel/Setting/SettingScreen/Exit")?.GetComponent<Button>();
            settingExitButton?.onClick.RemoveAllListeners();
            settingExitButton?.onClick.AddListener(BackToPauseMenu);

            // Dropdown / Slider / Toggle
            // Find GameObject.GetComponent<Dropdown>();

            //resolutionDropdown = root.transform.Find("MenuPause Panel/Setting/SettingScreen/Resolution/Dropdown")?.GetComponent<Dropdown>();
            resolutionDropdown = root.transform.Find("MenuPause Panel/Setting/SettingScreen/Resolution/Dropdown")?.GetComponent<TMP_Dropdown>();
            if (resolutionDropdown != null)
            {
                resolutionDropdown.onValueChanged.RemoveAllListeners();
                resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
            }

            volumeSlider = root.transform.Find("MenuPause Panel/Setting/SettingScreen/Volume/Slider")?.GetComponent<Slider>();
            if (volumeSlider != null)
            {
                volumeSlider.onValueChanged.RemoveAllListeners();
                volumeSlider.onValueChanged.AddListener(ChangeVolume);
            }

            fullscreenToggle = root.transform.Find("MenuPause Panel/Setting/SettingScreen/Fullscreen/Toggle")?.GetComponent<Toggle>();
            if (fullscreenToggle != null)
            {
                fullscreenToggle.onValueChanged.RemoveAllListeners();
                fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            }
        }

        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
            SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void OpenSettings()
    {
        //if (pausePanel == null || settingsPanel == null) SetupPauseMenu();
        //if (pausePanel != null) pausePanel.SetActive(false);
        //if (settingsPanel != null) settingsPanel.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    // ---------- Paramètres ----------
    private void ChangeResolution(int index) { Debug.Log("Changer résolution : " + index); }
    private void ChangeVolume(float value) { AudioListener.volume = value; }
    private void SetFullscreen(bool isFullscreen) { Screen.fullScreen = isFullscreen; }
}
