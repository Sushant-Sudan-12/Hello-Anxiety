using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnKeyPress : MonoBehaviour
{
    void Update()
    {
        // If the 'R' key is pressed, reload the current scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    // Method to reload the current scene
    void ReloadScene()
    {
        // Get the active scene and reload it by its name
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

