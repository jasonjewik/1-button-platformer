using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private Transform obsTF;
    private Vector3 pos;
    private float speed;

    private float leftEdge;

    // Use this for initialization
    void Start () {
        obsTF = GetComponent<Transform>();
        leftEdge = -16f;
    }
	
	// Update is called once per frame
	void Update () {
        pos = obsTF.position;

        if (pos.x < leftEdge)
        {
            GameObject.Destroy(this.gameObject);
        }

        obsTF.position = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
    }

    public void SetObsSpeed(float speedValue)
    {
        speed = speedValue;
    }
}
