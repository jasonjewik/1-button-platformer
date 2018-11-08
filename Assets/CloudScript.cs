using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

    private Transform cloudTF;
    private Vector3 pos;
    private float speed;
    private float leftEdge = -16f;

    public GameObject player;

    private void Start()
    {
        speed = Random.Range(0.2f, 0.7f); ;
        cloudTF = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update () {
        pos = cloudTF.position;

        if (player.GetComponent<PlayerScript>().getGameState())
        {
            cloudTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
        }

        if (pos.x < leftEdge)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
