using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HIScript : MonoBehaviour {

    Text scoreText;
    private static int score = 0;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "HI " + score.ToString("D6");
    }

    public void setScore(int s)
    {
        score = s;
    }

    public int getScore()
    {
        return score;
    }
}
