using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    //This is to play spalshscreen video and then move to the menu afterwards


    public float time = 5f; // time of vid
    void Start()
    {
        StartCoroutine(WaitIntro());

    }

   IEnumerator WaitIntro()
   {
        yield return new WaitForSeconds(time);  // waits for vid to play out
        
        // loads the menu scene
       SceneManager.LoadScene(1);
   
   }
   
   
}
