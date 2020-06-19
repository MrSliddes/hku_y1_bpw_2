using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the correct scene and starts the game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
