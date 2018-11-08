using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCloudsScript : MonoBehaviour {

    public GameObject cloud;
    public GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("CreateClouds", 0, 10.0f);
    }

    void CreateClouds()
    {
        Vector3 pos = this.transform.position;
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            Instantiate(cloud, new Vector2(pos.x, pos.y + Random.Range(-1f, 2.5f)), new Quaternion());
        }
    }
}
