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
    public float groundCheckRadius;
    public float groundSlamCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsFlyingObstacle;
    private Collider2D flyingObsCol;

    public int extraJumpsValue;
    private int extraJumps;
    private bool groundSlamming;

    private bool spawnObstacles;
    private bool playGame;

    public GameObject gameOver;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        extraJumps = extraJumpsValue;
        groundSlamming = false;
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
                groundSlamming = false;
                extraJumps = extraJumpsValue;
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                groundSlamming = false;
                playerRB2D.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0)
            {
                if (isGrounded)
                {
                    groundSlamming = false;
                    playerRB2D.velocity = Vector2.up * jumpForce;
                }
                else
                {
                    groundSlamming = true;
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        flyingObsCol = Physics2D.OverlapCircle(groundCheck.position, groundSlamCheckRadius, whatIsFlyingObstacle);
  
        if (groundSlamming && flyingObsCol)
        {
            GameObject.Destroy(flyingObsCol.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
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
