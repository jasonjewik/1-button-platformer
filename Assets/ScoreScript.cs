using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    Text scoreText;
    private int score = 0;

    private GameObject player;
    public GameObject HIscore;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GetComponent<Text>();
        InvokeRepeating("incScore", 0, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!player.GetComponent<PlayerScript>().getGameState())
        {
            CancelInvoke("incScore");
            if (HIscore.GetComponent<HIScript>().getScore() < score)
            {
                HIscore.GetComponent<HIScript>().setScore(score);
            }
        }
        else
        {   
            scoreText.text = score.ToString("D6");

            // Increase speed over time
            if (score % 100 == 0)
            {
                GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
                foreach (GameObject platform in platforms)
                {
                    platform.GetComponent<PlatformScript>().incSpeed();
                }
                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                foreach (GameObject obs in obstacles)
                {
                    obs.GetComponent<ObstacleScript>().incSpeed();
                }

                GameObject obsGen = GameObject.FindGameObjectWithTag("GameController");
                obsGen.GetComponent<ObstacleGeneratorScript>().incSpeed();
                obsGen.GetComponent<ObstacleGeneratorScript>().incSpawnRate();
            }
        }
	}

    public void incScore()
    {
        score++;
    }
}
