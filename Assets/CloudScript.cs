using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

    private Transform cloudTF;
    private Vector3 pos;
    public float speed = 1.0f;

    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update () {
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            cloudTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
        }
    }
}
