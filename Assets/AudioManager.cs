using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip, float targetVolume = 1f, float fadeInTime = 2f)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
            return;

        audioSource.clip = clip;
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeVolume(targetVolume, fadeInTime));
    }

    public void StopMusic(float fadeOutTime = 2f)
    {
        StartCoroutine(FadeOut(fadeOutTime));
    }

    private IEnumerator FadeVolume(float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
