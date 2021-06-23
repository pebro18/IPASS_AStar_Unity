using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A simple implementation to go to another scene and to quit the app
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Goes to another scene based on name
    /// </summary>
    /// <param name="LevelName"></param>
    public void LoadNextScene(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }

}
