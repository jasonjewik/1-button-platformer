using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCloudsScript : MonoBehaviour {

    public GameObject cloud;
    public GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("CreateClouds", 0, 5.0f);
    }

    void CreateClouds()
    {
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            Instantiate(cloud, this.transform.position, new Quaternion());
        }
    }
}
