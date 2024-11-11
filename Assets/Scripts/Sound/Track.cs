using System.Collections;
using UnityEngine;

/* This script changes the volume of the attached AudioSource with different public functions */
[RequireComponent(typeof(AudioSource))]
public class Track : MonoBehaviour
{
    private AudioSource track;
    [SerializeField]
    private float volumeCurrent = 0, volumeBase = 0;
    [SerializeField]
    private bool unmute = false;

    public bool additionalRandomLayer = false;

    private Coroutine volumeCoroutine;

    private void Awake()
    {
        track = GetComponent<AudioSource>();
        track.playOnAwake = false;
    }

    private void Start()
    {
        if (unmute)
        {
            FadeIn(volumeBase);
        }
    }

    IEnumerator ChangeVolumeOvertime(float startVolume, float targetVolume, float duration)
    {
        float startingCoroutine = 0.0f;

        while (startingCoroutine < duration)
        {
            if (track == null)
            {
                yield break; // Exit the coroutine if the AudioSource has been destroyed
            }

            volumeCurrent = Mathf.Lerp(startVolume, targetVolume, startingCoroutine / duration);
            track.volume = volumeCurrent;
            startingCoroutine += Time.deltaTime;
            yield return null;
        }

        if (track != null)
        {
            track.volume = volumeCurrent; // Apply the final volume change if track still exists
        }
    }

    public void FadeIn(float fadeDuration)
    {
        if (track != null)
        {
            if (volumeCoroutine != null)
            {
                StopCoroutineSafe(volumeCoroutine); // Safe method to stop coroutines
            }
            volumeCoroutine = StartCoroutine(ChangeVolumeOvertime(volumeCurrent, volumeBase, fadeDuration));
        }
    }

    public void FadeOut(float fadeDuration)
    {
        if (track != null)
        {
            if (volumeCoroutine != null)
            {
                StopCoroutineSafe(volumeCoroutine); // Safe method to stop coroutines
            }
            volumeCoroutine = StartCoroutine(ChangeVolumeOvertime(volumeCurrent, 0.0f, fadeDuration));
        }
    }

    public void SetVolume(float vol)
    {
        if (track != null)
        {
            track.volume = vol;
        }
    }

    // Safely stop coroutines (check if it's not already null)
    private void StopCoroutineSafe(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    // Stop coroutines when the object is destroyed to avoid errors
    private void OnDestroy()
    {
        if (volumeCoroutine != null)
        {
            StopCoroutineSafe(volumeCoroutine);
        }
    }
}

