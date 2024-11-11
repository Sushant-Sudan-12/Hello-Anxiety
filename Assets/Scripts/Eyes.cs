using UnityEngine;
using System.Collections;

public class EnableGameObjects : MonoBehaviour
{
    public GameObject[] gameObjects; 
    public float minInterval = 3f;  
    public float maxInterval = 4f; 

    private void Start()
    {
        StartCoroutine(EnableObjectsAtIntervals());
    }

    IEnumerator EnableObjectsAtIntervals()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
            gameObjects[i].SetActive(true);
        }
    }
}
