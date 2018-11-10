using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    public GameObject platform;
    public GameObject[] obstacle = new GameObject[2];
    private int GROUND = 0;
    private int FLYING = 1;
    private bool groundObsEnabled = true;
    private bool flyingObsEnabled = true;
    public float[] flyingRange = { 1.0f, 2.0f };

    [Tooltip("How quickly the platform will move to the left of the screen.")]
    public float speed = 5.0f;
    [Tooltip("How many times faster flying obstacles will be compared to platforms.")]
    public float[] speedMultiplers = { 1.5f, 2.0f };
    [Tooltip("Percent chance that the next platform will come with an obstacle. Number between 0 and 100.")]
    public float obsSpawnChance = 50;
    [Tooltip("Percent chance that the obstacle spawned will be a ground obstacle. Number between 0 and 100.")]
    public float groundObsChance = 80;
    [Tooltip("Percent chance that the obstacle spawned will be a flying obstacle. Number between 0 and 100.")]
    public float flyingObsChance = 20;

    private Transform platformTF;
    private Vector3 pos;
    private Collider2D platformCol;

    private bool createdPlatform = false;
    private bool createdObstacle = false;
    private bool canCreateObstacle = true;
    private float leftEdge = -16f;
    private float rightEdge = 16f;
    private float xBounds, yBounds;
    private float flyingObsLimit = 2.0f;
    private float groundObsLimit = 5.0f;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        platformTF = GetComponent<Transform>();
        platformCol = GetComponent<Collider2D>();

        xBounds = platformCol.bounds.size.x;
        yBounds = platformCol.bounds.size.y;
    }
	
	// Update is called once per frame
	void Update () {
        // Run only if the player hasn't died
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            // Get current position
            pos = platformTF.position;

            // Create a platform behind this one
            // pos.x < rightEdge prevents new platforms from being created infinitely
            if (!createdPlatform && pos.x < rightEdge)
            {
                // "xBounds - speed * Time.deltaTime" is needed to prevent gaps between platforms
                GameObject newPlatform = Instantiate(platform, new Vector2(pos.x + xBounds - speed * Time.deltaTime, pos.y), new Quaternion());
                newPlatform.name = "Platform";
                newPlatform.GetComponent<PlatformScript>().incLimits();

                canCreateObstacle = player.GetComponent<PlayerScript>().GetSpawnObstacles();
                // Creates an obstacle
                if (!createdObstacle && canCreateObstacle)
                {
                    // Checks for existing obstacles to prevent too many from cluttering the screen
                    checkExistingObstacles();

                    if (Random.value < obsSpawnChance / 100)
                    {
                        float obsNum = Random.value;
                        if (obsNum <= groundObsChance / 100 && groundObsEnabled)
                        {
                            GameObject newObstacle = Instantiate(obstacle[GROUND], new Vector2(pos.x + Random.Range(-xBounds / 2, xBounds / 2), pos.y + yBounds), new Quaternion());
                            newObstacle.name = "Obstacle";
                            newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(speed);
                        }
                        else if (obsNum <= (groundObsChance + flyingObsChance) / 100 && flyingObsEnabled)
                        {
                            GameObject newObstacle = Instantiate(obstacle[FLYING], new Vector2(pos.x + 2 * xBounds, pos.y + yBounds), new Quaternion());
                            newObstacle.name = "Flying Obstacle";
                            newObstacle.transform.position = new Vector2(pos.x, Random.Range(pos.y + flyingRange[0], pos.y + flyingRange[1]));
                            newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(Random.Range(speed * speedMultiplers[0], speed * speedMultiplers[1]));
                        }
                    }
                    // Ensures only one obstacle is created per platform
                    createdObstacle = true;
                }
                // Ensures only one platform is created
                createdPlatform = true;
            }


            // If the object has gone over the left edge, remove it
            if (pos.x < leftEdge)
            {
                GameObject.Destroy(this.gameObject);
            }

            // Update position based on speed
            platformTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
        }
	}

    private void checkExistingObstacles()
    {
        // Gets an array of all existing obstacles
        GameObject[] existingObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        int numFlyingObstacles = 0;
        int numGroundObstacles = 0;
        for (int i = 0; i < existingObstacles.Length; i++)
        {
            if (existingObstacles[i].name == "Flying Obstacle")
            {
                numFlyingObstacles++;
            }
            else if (existingObstacles[i].name == "Obstacle")
            {
                numGroundObstacles++;
            }
        }
        // Clamps flying obstacles on screen at once
        if (numFlyingObstacles >= Mathf.RoundToInt(flyingObsLimit))
        {
            flyingObsEnabled = false;
        }
        // Clamps ground obstacles on screen at once
        if (numGroundObstacles >= Mathf.RoundToInt(groundObsLimit))
        {
            groundObsEnabled = false;
        }
    }

    public void incLimits()
    {
        // Increases number of obstacles allowed on screen at once
        flyingObsLimit += 0.05f;
        groundObsLimit += 0.05f;
    }
}
