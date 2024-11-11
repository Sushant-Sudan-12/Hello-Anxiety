using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;  // For Light2D

public class IlluminateController : MonoBehaviour
{
    public GameObject illuminate;
    public Light2D[] lights;
    public float fadeDuration = 3f;
    public float initialFalloff = 0.1f;
    public float finalFalloff = 1f;
    public float stableDuration = 2f;    
    private bool isOperationRunning = false; 
    public GameObject mainlight;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isOperationRunning)
        {
            mainlight.SetActive(false);
            StartCoroutine(HandleLights());
        }
    }

    private IEnumerator HandleLights()
    {
        isOperationRunning = true;  
        illuminate.SetActive(true);
       
        yield return new WaitForSeconds(stableDuration);

        
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            foreach (var light in lights)
            {
                light.falloffIntensity = Mathf.Lerp(initialFalloff, finalFalloff, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        illuminate.SetActive(false);

        foreach (var light in lights)
        {
            light.falloffIntensity = initialFalloff;
        }

        isOperationRunning = false;  
        mainlight.SetActive(true);
    }
}
