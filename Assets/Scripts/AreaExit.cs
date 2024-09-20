using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public float waitToLoad = 1f;
    public bool shouldFade;

    public string areaTransitionName;
    void Start()
    {
        
    }


    void Update()
    {
        if(shouldFade){
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0){
                SceneManager.LoadScene(areaToLoad);
                shouldFade = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            print("gay");
            UIFade.instance.FadeToBlack();
            shouldFade = true;
            PlayerMovement.instance.areaTransitionName = areaTransitionName;
        }
    }
}
