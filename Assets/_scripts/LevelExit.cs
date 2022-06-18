using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    
    void OnTriggerEnter2D(Collider2D other) 
    {        
        if (other.tag == "Player")
        {
            StartCoroutine(nextScene());
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSecondsRealtime(delay); // waits for when the delay is down

        // gets the current scene
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        // adds 1 to current scen
        int nextLevel = currentLevel + 1;
        
        // if next level equals to the amount of scenes. it's set back to 0
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();

        SceneManager.LoadScene(nextLevel); // loads scene
    }
}
