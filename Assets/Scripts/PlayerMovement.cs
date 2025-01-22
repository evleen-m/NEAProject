using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    GameLogic logic;
    public bool isPlayerAlive = true;
    public float immunityCounter = 0f;
    public bool hasImmunity = false;

    private float deadZoneY = 4.9f;
    private float deadZoneX = -14f;
    private float Ceiling = 15.3f;
    private float playerWall = 1f;

    //bezier curve control points
    private Vector2 startPoint;
    private Vector2 controlPoint;
    private Vector2 endPoint;

    // Player's current position
    private Vector2 currentPlayerPosition;

    // Curve time traversal parameter
    private float t = 0f;

    private bool isJumping = false;

    // Jump parameters
    [SerializeField] private float jumpHeight = 2f; // Height of the jump
    [SerializeField] private float fallHeight = 1f; // Height to fall
    [SerializeField] private float curveSpeed = 3f; // Speed of curve traversal

    // Gravity -> falling when curve is not in effect
    [SerializeField] private float gravity = 1.5f;

    // Reference to the ground level
    [SerializeField] private float groundY = 1.5f;



    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

        // Initialize the starting position of the player
        startPoint = transform.position;
        InitialiseFallCurve(); //player will fall right as game starts -> "gravity"

    }

    void Update()
    {
        CheckToResetImmunity();
        immunityCounter += Time.deltaTime;

        //if player life is 0 or below, no more inputs are taken in
        //this stops the bird from taking inputs from user causes it to fall below screen - leads to game over
        if (logic.lifeCounter > 0)
        {
            ValidPlayerInput();

        }

        // Update player's position
        if (isJumping || t < 1f)
        {
            UpdatePlayerPosition();

            
        }
        else
        {
            ApplyGravity();
        }

        // if bird falls below player view it is destroyed
        if (deadZoneY > transform.position.y)
        {        //destroys the bird
            Debug.Log("bird deleted");
            Destroy(gameObject);
            logic.GameOver();
        }
        if (deadZoneX > transform.position.x)
        {        //destroys the bird
            Debug.Log("bird deleted");
            Destroy(gameObject);
            logic.GameOver();
        }
    }

    private void ValidPlayerInput()
    {
        if (transform.position.y > Ceiling)
        {
            transform.position = new Vector3(transform.position.x, Ceiling, 0);
            //Debug.Log("bird position transform.position.y: " + transform.position.y);
            //moves bird down to if it goes above ceiling - stops players from flying off screen and over pipes
        }
        if (transform.position.x > playerWall)
        {
            transform.position = new Vector3(playerWall, transform.position.y, 0);
            //Debug.Log("bird position transform.position.x: " + transform.position.x);
            //moves bird down to if it goes above ceiling - stops players from flying off screen and over pipes
        }
        if (transform.position.x < playerWall - 3)
        {
            transform.position = new Vector3(playerWall, transform.position.y, 0);
            //Debug.Log("bird position transform.position.x: " + transform.position.x);
            //moves bird down to if it goes above ceiling - stops players from flying off screen and over pipes
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Stats.Record.FlyingTaps++; 
            StartJump();
            //Debug.Log("pressed jump");
            //moves player up by jumpheight when W is pressed and bird is in play
        }
        
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Stats.Record.FlyingTaps++;
        //    playerRigidbody.velocity = Vector2.right * JumpHeight;
        //    //moves player up by jumpheight when W is pressed and bird is in play
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Stats.Record.FlyingTaps++;
        //    playerRigidbody.velocity = Vector2.left * JumpHeight;
        //    //moves player up by jumpheight when W is pressed and bird is in play
        //}



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.DetectedCollision();
    }

    public void CheckToResetImmunity()
    {
        if (immunityCounter >= 3 && hasImmunity)
        {
            hasImmunity = false;
            logic.SetBackgroundNormal();
            Debug.Log("Reset immunity");
        }
    }

    // Initialize a downward curve for falling
    private void InitialiseFallCurve()
    {
        startPoint = transform.position;
        controlPoint = new Vector2(transform.position.x - 0.2f, transform.position.y - fallHeight / 2);
        endPoint = new Vector2(transform.position.x, transform.position.y - fallHeight);
        t = 0f;
    }

    // Start a jump with an upward curve
    private void StartJump()
    {
        isJumping = true;
        t = 0f;
        startPoint = transform.position;
        controlPoint = new Vector2(transform.position.x + 0.2f, transform.position.y + jumpHeight);
        endPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update the player's position along the current Bézier curve
    private void UpdatePlayerPosition()
    {
        // Move along the Bézier curve
        currentPlayerPosition = CalculateBezierPoint(t, startPoint, controlPoint, endPoint);
        transform.position = currentPlayerPosition;

        // Increment t based on speed
        t += Time.deltaTime * curveSpeed;

        // Check if the curve traversal is complete
        if (t >= 1f)
        {
            t = 1f; // Clamp t
            if (isJumping)
            {
                isJumping = false; // End jumping state
                InitialiseFallCurve(); // Start falling again
            }
        }
    }

    // Apply gravity if the player is falling and not jumping
    private void ApplyGravity()
    {
        if (transform.position.y > groundY)
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }
        else
        {
            // Reset to ground level when hitting the ground
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
        }
    }

    // Calculate a point on a quadratic Bézier curve
    private Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        return (u * u * p0) + (2 * u * t * p1) + (t * t * p2);
    }

}


