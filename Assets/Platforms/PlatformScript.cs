using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    public GameObject platform;

    [Tooltip("How quickly the platform will move to the left of the screen.")]
    public float speed;
    private float initSpeed;

    private Transform platformTF;
    private Vector3 pos;
    private Collider2D platformCol;

    private bool createdPlatform = false;
    private float leftEdge = -16f;
    private float rightEdge = 16f;
    private float xBounds;

    private GameObject player;

	// Use this for initialization
	void Start () {
        initSpeed = speed;

        player = GameObject.FindGameObjectWithTag("Player");
        platformTF = GetComponent<Transform>();
        platformCol = GetComponent<Collider2D>();

        xBounds = platformCol.bounds.size.x;
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
                newPlatform.GetComponent<PlatformScript>().setSpeed(speed);
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

    public float getSpeed()
    {
        return speed;
    }

    public float getInitSpeed()
    {
        return initSpeed;
    }

    public void setSpeed(float num)
    {
        speed = num;
    }

    public void incSpeed()
    {
        speed += 0.05f;
    }
}
