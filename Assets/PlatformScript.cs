﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    public GameObject platform;
    private GameObject newPlatform;
    public GameObject[] obstacle = new GameObject[2];
    private GameObject newObstacle;
    public float speed = 5.0f;
    public float[] speedMultiplers = { 1.5f, 2.0f };
    [Tooltip("Percent chance that the next platform will come with an obstacle. Number between 0 and 100.")]
    public float obsSpawnChance = 50;
    [Tooltip("Percent chance that the obstacle spawned will be a ground obstacle. Number between 0 and 100.")]
    public float groundObsChance = 80;
    [Tooltip("Percent chance that the obstacle spawned will be a flying obstacle. Number between 0 and 100.")]
    public float flyingObsChance = 20;
    private int obsType = -1;

    private Transform platformTF;
    private Vector3 pos;
    private Collider2D platformCol;

    private bool createdPlatform;
    private float leftEdge, rightEdge;
    private float xBounds, yBounds;

    public GameObject player;

	// Use this for initialization
	void Start () {
        platformTF = GetComponent<Transform>();
        platformCol = GetComponent<Collider2D>();

        createdPlatform = false;
        leftEdge = -16f;
        rightEdge = 16f;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            pos = platformTF.position;
            xBounds = platformCol.bounds.size.x;
            yBounds = platformCol.bounds.size.y;

            if (!createdPlatform && pos.x < rightEdge)
            {
                newPlatform = Instantiate(platform, new Vector2(pos.x + xBounds - speed * Time.deltaTime, pos.y), new Quaternion());
                newPlatform.name = "Platform";

                if (Random.value < obsSpawnChance / 100)
                {
                    if (GameObject.FindGameObjectWithTag("Player"))
                    {
                        if (player.GetComponent<PlayerScript>().GetSpawnObstacles())
                        {
                            float obsNum = Random.value;
                            if (obsNum <= groundObsChance / 100)
                            {
                                obsType = 0;
                            } 
                            else if (obsNum <= (groundObsChance + flyingObsChance) / 100)
                            {
                                obsType = 1;
                            }
                            newObstacle = Instantiate(obstacle[obsType], new Vector2(pos.x + Random.Range(-xBounds / 2, xBounds / 2), pos.y + yBounds), new Quaternion());
                            if (obsType == 0)
                            {
                                newObstacle.name = "Obstacle";
                                newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(speed);
                                newObstacle.GetComponent<ObstacleScript>().SetPlayer(player);
                            }
                            else if (obsType == 1)
                            {
                                newObstacle.name = "Flying Obstacle";
                                newObstacle.transform.position = new Vector2(pos.x, Random.Range(pos.y + 1, pos.y + 3));
                                newObstacle.GetComponent<ObstacleScript>().SetObsSpeed(Random.Range(speed * speedMultiplers[0], speed * speedMultiplers[1]));
                                newObstacle.GetComponent<ObstacleScript>().SetPlayer(player);
                            }
                        }
                    }
                }

                createdPlatform = true;
            }

            if (pos.x < leftEdge)
            {
                GameObject.Destroy(this.gameObject);
            }

            platformTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
        }
	}
}
