using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // variable 
    bool wasCollected = false;
    [SerializeField] int pointsForCoinPickup  = 100;
    [SerializeField] AudioClip coinPickupSFX;
   

    void OnTriggerEnter2D(Collider2D other) 
    {
        // if the player hits a coin
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;

            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup); // adds pointsForCoinPickup  to score board

            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position); // plays sound at the position of the coin

            gameObject.SetActive(false);

            Destroy(gameObject); // destroys coin
        }
    }
}
