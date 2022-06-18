using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // variables 
    [SerializeField] Vector2 deathAni = new Vector2 (8f, 8f);
    [SerializeField] float climbS = 5f; // climbing speed
    
    [SerializeField] GameObject bullet; // instance of the bullet class
    [SerializeField] float runS = 10f; // run speed


    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Transform gun; // where bullet shoots from
    
    CapsuleCollider2D mainCollider; // body collider of player
    Vector2 moveInput;
    
   
    Animator Ani; // gets animations
    BoxCollider2D bottomCollider; // feet collider of player

    float gravityS;
    Rigidbody2D myRigidbody; // rigidbody

    bool alive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); // get rigidbody
        Ani = GetComponent<Animator>(); // gets animations
        gravityS = myRigidbody.gravityScale; // gravity of player
        
        mainCollider = GetComponent<CapsuleCollider2D>(); // collider 1
        bottomCollider = GetComponent<BoxCollider2D>(); // collider 2
    }

    void Update()
    {
        if (!alive) { return; } // check if alive
        Run();

        Flip();

        Climb();

        Death();
    }
    
   void OnMove(InputValue value)
    {
        // checks if alive
        if (!alive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!alive) { return; } // checks if alive

        // checks if the player is touching the level or not
        if (!bottomCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}
        
        if(value.isPressed)
        {
            // moves on the y-axis
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

     void OnFire(InputValue value)
    {
        if (!alive) { return; } // checks if alive

        // shoots bullet from the position the player is at
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void Run()
    {
        // left and right movement for player
        Vector2 playerV = new Vector2 (moveInput.x * runS, myRigidbody.velocity.y);
        myRigidbody.velocity = playerV;

        // checks if the player is moving and if so it executes the isRunning animation
        bool playerRun = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        Ani.SetBool("isRunning", playerRun);

    }

     void Death()
    {
        // if the players collider hits hazard or enemy, alive status set to dead, death animation triggered and sent to beginning of level
        if (mainCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            alive = false;

            Ani.SetTrigger("Dying");

            myRigidbody.velocity = deathAni;

            FindObjectOfType<GameSession>().PlayerDeath(); 
        }
    }
    
   
    void Climb()
    {
        // if your not touching the ladder gravity stays same and no climbing animation
        if (!bottomCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        { 
            myRigidbody.gravityScale = gravityS;

            Ani.SetBool("isClimbing", false);
            
            return;
        }
        
        // move upwards on the ladder and animation is triggered if touching a ladder
        Vector2 climbSpeed = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbS);

        myRigidbody.velocity = climbSpeed;

        myRigidbody.gravityScale = 0f; // gravity is set on zero only when player is on ladder
       
        bool playerLadderSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        Ani.SetBool("isClimbing", playerLadderSpeed);
    }

     // flips sprite when moving left or right
    void Flip()
    {
        // bool to check if the absoulute value of your movement and if it's greater than 0
        bool playerRun = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // if true
        if (playerRun)
        {
            // checks if the x-axis is positive or negative and flips sprite depending on that
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }


   

}
