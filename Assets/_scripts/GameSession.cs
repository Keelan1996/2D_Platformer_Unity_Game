using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    // variables 
    [SerializeField] TextMeshProUGUI scoreText;
   
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    
    [SerializeField] int playerLives = 3;
    
    void Awake()
    {
        int amountOfSessions = FindObjectsOfType<GameSession>().Length; // finds the number of sessions and stpre in int


        if (amountOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        // game starts with set lives and empty score
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();    
    }

    public void PlayerDeath()
    {
        // when player dies and has enough remaining lifes, just take one off. If they are
        // out of lives reset game
        if (playerLives > 1)
        {
            minusLife();
        }
        else
        {
            Reset();
        }
    }

    public void AddToScore(int points)
    {
        // adds the points onto the scoreboard
        score += points;
        scoreText.text = score.ToString(); 
    }

    void minusLife()
    {
        //takes away life
        playerLives--;

        // finds which scene player is at when killed
        int index = SceneManager.GetActiveScene().buildIndex; 
        
        // loads player back in that scene
        SceneManager.LoadScene(index);
        // updates the life counter in game
        livesText.text = playerLives.ToString();
    }

     void Reset()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        // loads main menu
        SceneManager.LoadScene(1);
        // creates fresh game
        Destroy(gameObject);
    }
}
