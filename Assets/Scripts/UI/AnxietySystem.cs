using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For reloading the scene
using System.Collections;

public class AnxietySystem : MonoBehaviour
{
    public Slider anxietySlider;
    public float maxAnxiety = 100f;
    public float minAnxiety = 0f;
    public float currentAnxiety = 100f;
    public float decreaseRateInDarkness = 1f; // Anxiety decrease per second in darkness
    public float increaseRateWithTorch = 4f; // Anxiety increase per second with torch
    public float rapidDecreaseRateWhenChased = 8f; // Anxiety decrease rate when chased
    public float increaseRateWithIlluminate = 3f; // Anxiety increase per second with illuminate spotlight
    public GameObject panicAttackText; // Reference to the "Panic Attack" text UI
    public float panicAttackDisplayDuration = 3f; // How long to display "Panic Attack"
    public TestMusicManager musicManager;
    
    private bool isInDarkness = false;
    private bool isTorchActive = false;
    private bool isChased = false;
    private bool isIlluminateActive = false; // New variable for spotlight state

    void Start()
    {
        anxietySlider.maxValue = maxAnxiety;
        anxietySlider.minValue = minAnxiety;
        anxietySlider.value = currentAnxiety;

        // Initially hide the "Panic Attack" text
        panicAttackText.SetActive(false);
    }

    void Update()
    {
        if (isInDarkness)
        {
            currentAnxiety -= decreaseRateInDarkness * Time.deltaTime;
        }

        if (isTorchActive)
        {
            currentAnxiety += increaseRateWithTorch * Time.deltaTime;
        }

        if (isChased)
        {
            currentAnxiety -= rapidDecreaseRateWhenChased * Time.deltaTime;
        }

        if (isIlluminateActive) // Check if the illuminate spotlight is active
        {
            currentAnxiety += increaseRateWithIlluminate * Time.deltaTime;
        }

        // Clamp anxiety between min and max values
        currentAnxiety = Mathf.Clamp(currentAnxiety, minAnxiety, maxAnxiety);
        anxietySlider.value = currentAnxiety;

        // Check if anxiety has reached 0 to trigger the panic attack
        if (currentAnxiety <= 0f)
        {
            TriggerPanicAttack();
        }
        if(currentAnxiety <=50f){
            musicManager.SetMusicState(TestMusicManager.MusicState.Anxious);
        }else{
            musicManager.SetMusicState(TestMusicManager.MusicState.FreeRoam);
        }
    }
    // Method to trigger the panic attack sequence
    public void TriggerPanicAttack()
    {
        // Start the fade-out, panic attack message, and scene reload sequence
        StartCoroutine(HandlePanicAttack());
    }

    private IEnumerator HandlePanicAttack()
    {
        // Trigger fade to black
        UIFade.instance.FadeToBlack();

        // Wait for the screen to fully fade to black (you can adjust this wait time)
        yield return new WaitForSeconds(UIFade.instance.fadeSpeed);

        // Display "Panic Attack" message
        panicAttackText.SetActive(true);

        // Wait for the message to be shown for a set duration
        yield return new WaitForSeconds(panicAttackDisplayDuration);

        // Reload the current scene after showing the message
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //f i0hjwerti9o ht9gip8hwt9p 38wght389uo g
    }

    // Setters for the different anxiety factors
    public void SetInDarkness(bool value)
    {
        isInDarkness = value;
    }

    public void SetTorchActive(bool value)
    {
        isTorchActive = value;
    }

    public void SetChased(bool value)
    {
        isChased = value;
    }

    public void SetIlluminateActive(bool value)
    {
        isIlluminateActive = value;
    }
}

