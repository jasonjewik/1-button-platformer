using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    [Tooltip("How high the player will jump.")]
    public float jumpForce;
    [Tooltip("How quickly the player will return to the ground.")]
    public float groundSlamForce;

    private Rigidbody2D playerRB2D;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [Tooltip("How many times the player can jump while airborne.")]
    public int extraJumpsValue;
    private int extraJumps;

    private bool playGame = true;

    public GameObject gameOver;
    public GameObject Score;
    public int bonusPoints = 100;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        extraJumps = extraJumpsValue;

        playerRB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Called every frame
    private void Update()
    {
        // Run only if player hasn't died
        if (playGame)
        {
            // If player is on the ground, reset jumps
            if (isGrounded)
            {
                extraJumps = extraJumpsValue;
            }

            // Jump when space is pressed and extra jumps is greater than 0
            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                playerRB2D.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            // If space is pressed and extra jumps is 0...
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0)
            {
                // ...and the player is on the ground, do a regular jump
                if (isGrounded)
                {
                    playerRB2D.velocity = Vector2.up * jumpForce;
                }
                // ...and the player is airborne, ground slam instead
                else
                {
                    playerRB2D.velocity = Vector2.down * groundSlamForce;
                }
            }
        }
        else
        {            
            gameOver.GetComponent<Text>().enabled = true;

            // Reload game upon pressing space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // For physics-related stuff
    private void FixedUpdate()
    {
        // Checks to see if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Stops player movement and animation upon death
            Rigidbody2D.Destroy(playerRB2D);
            anim.enabled = false;

            playGame = false;
        }
    }

    public bool getGameState()
    {
        return playGame;
    }
}
