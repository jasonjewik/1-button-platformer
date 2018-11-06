using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public float jumpForce;
    public float groundSlamForce;

    private Rigidbody2D playerRB2D;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumpsValue;
    private int extraJumps;

    private bool spawnObstacles;
    private bool playGame;

    public GameObject gameOver;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        extraJumps = extraJumpsValue;
        playerRB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawnObstacles = false;
        playGame = true;

        Invoke("SetSpawnObstacles", 2);
    }

    // Called every frame
    private void Update()
    {
        if (playGame)
        {
            if (isGrounded)
            {
                extraJumps = extraJumpsValue;
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                playerRB2D.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0)
            {
                if (isGrounded)
                {
                    playerRB2D.velocity = Vector2.up * jumpForce;
                }
                else
                {
                    playerRB2D.velocity = Vector2.down * groundSlamForce;
                }
            }
        }
        else
        {
            playerRB2D.velocity = Vector2.zero;
            playerRB2D.gravityScale = 0;
            gameOver.GetComponent<Text>().enabled = true;
            anim.enabled = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // For physics-related stuff
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            playGame = false;
        }
    }

    public bool GetSpawnObstacles()
    {
        return spawnObstacles;
    }

    public void SetSpawnObstacles()
    {
        spawnObstacles = true;
    }

    public bool getGameState()
    {
        return playGame;
    }
}
