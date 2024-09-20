using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public Image fadeScreen;
    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;
    public static UIFade instance;

    public float fadeSpeed;
    void Start()
    {
       instance = this;

    }

    void Update()
    {
        if(shouldFadeToBlack){
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g,fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f ,fadeSpeed*Time.deltaTime));
            if(fadeScreen.color.a == 1f){
                shouldFadeToBlack = false;
            }
        }
        if(shouldFadeFromBlack){
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g,fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f ,fadeSpeed*Time.deltaTime));
            if(fadeScreen.color.a == 0f){
                shouldFadeFromBlack = false;
            }
        } 
    }

    public void FadeToBlack(){
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack(){
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}

