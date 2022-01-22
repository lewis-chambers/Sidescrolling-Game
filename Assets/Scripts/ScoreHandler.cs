using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{

    public int score = 0;
    public int numLanes;
    public TMPro.TMP_Text scoreText;
    public TMP_Text highScoreText;
    public laneHandler lanes;

    void Start()
    {
        numLanes = lanes.rowCount;
        ResizeObject();
        if (PlayerPrefs.HasKey("high_score")) {
            highScoreText.enabled = true;
            highScoreText.text = "Best Score: " + PlayerPrefs.GetInt("high_score").ToString();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mob Wall"))
        {
            score++;
            UpdateScore();
        }
    }

    void ResizeObject()
    {
        float y = Camera.main.orthographicSize * 2;
        transform.position.Scale(new Vector3(0.1f, y, 1f));
    }

    public void CoinCollected()
    {
        score += 5;
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.SetText("Score: " + score.ToString());

        if  (score >= 250)
        {
            if  (numLanes < 8)
            {
                numLanes = 8;
                ExpandLanes(numLanes);
            }
        } else if (score >=200)
        {
            if (numLanes < 7)
            {
                numLanes = 7;
                ExpandLanes(numLanes);
            }
        }
        else if (score >= 150)
        {
            if (numLanes < 6)
            {
                numLanes = 6;
                ExpandLanes(numLanes);
            }
        }
        else if (score >= 100)
        {
            if (numLanes < 5)
            {
                numLanes = 5;
                ExpandLanes(numLanes);
            }
        } else if (score >= 50)
        {
            if (numLanes < 4)
            {
                numLanes = 4;
                ExpandLanes(numLanes);
            }
        }
    }

    public void ExpandLanes(int newLanes)
    {

        lanes.ChangeRows(newLanes);
    }
}
