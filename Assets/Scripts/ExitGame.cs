using UnityEngine;
using System.Collections;

public class DelayedObjectActivator : MonoBehaviour
{
    public GameObject firstObject;  // First object to activate
    public GameObject secondObject; // Second object to activate

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {}
        StartCoroutine(ActivateObjectsWithDelay());
    }

    private IEnumerator ActivateObjectsWithDelay()
    {
        // Wait for 20 seconds before activating the first object
        yield return new WaitForSeconds(1f);
        firstObject.SetActive(true);  // Activate the first object

        // Wait for 5 seconds before activating the second object
        yield return new WaitForSeconds(5f);
        secondObject.SetActive(true);  // Activate the second object

        yield return new WaitForSeconds(10f);
        Application.Quit();
    }
}

