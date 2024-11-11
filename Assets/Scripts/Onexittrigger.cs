using UnityEngine;

public class OnTriggerExitScript : MonoBehaviour
{
    public GameObject light;
    public GameObject Mary;
    void OnTriggerExit2D(Collider2D other) {
        light.SetActive(true);
        Mary.SetActive(false);
    }
        

}
