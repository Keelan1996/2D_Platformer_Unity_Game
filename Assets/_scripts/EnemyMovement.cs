using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // variables 
    
    Rigidbody2D myRigidbody;

    [SerializeField] float speed = 1f; // movement speed
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (speed, 0f);
    }
    
    // if enemy hits an edge it changes direction the other way and flips sprite
    void OnTriggerExit2D(Collider2D other) 
    {
        speed = -speed;
        flip();
    }
    
    // flips enemy sprite
    void flip()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
