using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    private Transform bgTF;
    private Collider2D bgCol;
    private Vector3 pos;
    public float speed = 1.0f;

    public GameObject bg;
    private GameObject newBG;

    private bool createdBG;
    private float leftEdge, rightEdge;
    private float xBounds;

    public GameObject player;

    // Use this for initialization
    void Start () {
        bgTF = GetComponent<Transform>();
        bgCol = GetComponent<Collider2D>();

        createdBG = false;
        leftEdge = -22f;
        rightEdge = 22f;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<PlayerScript>().getGameState())
        {
            pos = bgTF.position;
            xBounds = bgCol.bounds.size.x;
            if (!createdBG && pos.x < rightEdge)
            {
                newBG = Instantiate(bg, new Vector2(pos.x + xBounds - speed * Time.deltaTime, pos.y), new Quaternion());
                newBG.name = "Background";

                createdBG = true;
            }

            if (pos.x < leftEdge)
            {
                GameObject.Destroy(this.gameObject);
            }

            bgTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
        }
    }
}
