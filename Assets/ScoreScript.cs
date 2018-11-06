using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    Text scoreText;
    private int score;

    public GameObject player;
    public GameObject HIscore;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        InvokeRepeating("incScore", 0, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!player.GetComponent<PlayerScript>().getGameState())
        {
            CancelInvoke("incScore");
            if (HIscore.GetComponent<HIScript>().getScore() < score)
            {
                HIscore.GetComponent<HIScript>().setScore(score);
            }
        }
        scoreText.text = score.ToString("D6");
	}

    public void incScore()
    {
        score++;
    }
}
