using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneratorScript : MonoBehaviour {

    [Tooltip("The types of obstacles we want to create.")]
    public GameObject[] obstacles;
    [Tooltip("The corresponding probabilities of the respective obstacle spawning. Should add up to 100.")]
    public float[] probabilities;
    [Tooltip("How often (in seconds )a new obstacle should be created.")]
    public float[] spawnTimes = new float[2];
    [Tooltip("How long to delay (in seconds) until obstacles start spawning.")]
    public float startTime;
    [Tooltip("How quickly ground obstacles should move to the left of the screen.")]
    public float speed;
    [Tooltip("How many times faster flying obstacles should move compared to ground obstacles.")]
    public float[] speedMult = new float[2];
    [Tooltip("How high flying obstacles should spawn above the platforms.")]
    public float[] distance = new float[2];

    private GameObject player;
    private GameObject platform;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("CreateObstacle", startTime);
	}
	
	// Update is called once per frame
	void Update () {
        // Destroy object if the player has died
        if (!player.GetComponent<PlayerScript>().getGameState())
        {
            GameObject.Destroy(gameObject);
        }
	}

    // Creates a new obstacle
    private void CreateObstacle()
    {
        float diceRoll = Random.value * 100;
        float cumulative = 0;

        for (int i = 0; i < obstacles.Length + 1; i++)
        {
            cumulative += probabilities[i];
            if (diceRoll < cumulative)
            {
                GameObject newObstacle = Instantiate(obstacles[i], transform.position, transform.rotation);
                newObstacle.name = obstacles[i].name;
                if (newObstacle.name == "Obstacle")
                {
                    newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(speed);
                }
                if (newObstacle.name == "FlyingObstacle")
                {
                    newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(speed * Random.Range(speedMult[0], speedMult[1]));
                    newObstacle.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(distance[0], distance[1]));
                }
                if (newObstacle.name == "HighObstacle")
                {
                    newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(speed);
                    newObstacle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.75f);
                }
                break;
            }
        }
        Invoke("CreateObstacle", Random.Range(spawnTimes[0], spawnTimes[1]));
    }

    public void setSpeed(float num)
    {
        speed = num;
    }

    public void incSpeed()
    {
        speed += 0.05f;
    }

    public void incSpawnRate()
    {
        if (spawnTimes[1] > 1)
        {
            spawnTimes[1] -= 0.05f;
        }
    }
}
