using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlamScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Flying Obstacle")
        {
            GameObject.Destroy(collision.gameObject);
        }
    }

}
