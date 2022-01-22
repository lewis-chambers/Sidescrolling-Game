using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Image[] hearts;
    public GameObject endPanel;
    public GameObject player;
    public GameObject scoreWall;

    public ScoreHandler scoreHandler;

    private int health = 3;
    private int maxHealth = 3;

    public void changeHealth(int change)
    {
        if (health == maxHealth && change > 0)
        {
            health = maxHealth;
        } else
        {
            health += change;
        }
        
        if (health <= 0)
        {
            //die();
        }

        for(int i = 1; i <= hearts.Length; i++)
        {
            if (i <= health)
            {
                hearts[i - 1].enabled = true; 
            } else
            {
                hearts[i - 1].enabled = false;
            }
        }
    }

    void die()
    {
        endPanel.SetActive(true);
        Destroy(player);
        Destroy(scoreWall);
        PlayerPrefs.SetInt("high_score", scoreHandler.score);
    }
}
