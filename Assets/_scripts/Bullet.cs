using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //  variables
    Rigidbody2D myRigidbody;
    float xS; // x-axis speed
    [SerializeField] float bulletS = 20f; // bullet speed
    PlayerMovement playerCharacter;
    
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        playerCharacter = FindObjectOfType<PlayerMovement>();

        xS = playerCharacter.transform.localScale.x * bulletS; // bullet faster than character
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (xS, 0f);
    }

     // if the bullet hits enemy it gets destroyed along with the enemy
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    // destroys bullet
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);    
    }

}
