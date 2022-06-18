using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
       SceneManager.LoadScene(2); // loads into the game
    }

     public void QuitGame()
    {
       Application.Quit(); // quits game
    }
}
