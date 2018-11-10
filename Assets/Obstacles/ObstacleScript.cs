using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private Transform obsTF;
    private Vector3 pos;
    private float speed;

    private float leftEdge;

    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        obsTF = GetComponent<Transform>();
        leftEdge = -16f;
    }
	
	// Update is called once per frame
	void Update () {
        // Run only if the player hasn't died
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            // Get current position
            pos = obsTF.position;
            
            // If the object has gone over the left edge, remove it
            if (pos.x < leftEdge)
            {
                GameObject.Destroy(this.gameObject);
            }

            // Update position based on speed
            obsTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
        }
    }

    public void SetObsSpeed(float speedValue)
    {
        speed = speedValue;
    }
}
