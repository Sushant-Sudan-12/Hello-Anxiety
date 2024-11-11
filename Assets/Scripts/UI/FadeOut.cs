using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

    void Awake()
    {
        StartCoroutine(fadeOut());
    }
    IEnumerator fadeOut(){
        yield return new WaitForSeconds(2f);
        UIFade.instance.FadeFromBlack();
    }
}
