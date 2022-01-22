using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int maxRows;
    public float[] positionArray;

    [Header("Components")]
    public GameObject monster;
    public GameObject coin;
    public GameObject heart;
    public GameObject mobWall;


    private float delay = 2f;
    private float t = 0f;
    public float mobSpeed;
    public float speedUpFactor;
    public float xPosition;
    private int collectiblesPerWave = 1;
    private int collectiblesMade = 0;

    private List<GameObject> gameObjects = new List<GameObject>();
    private GameObject tempObj;
    private float destroyTime;

    public void setMaxRows(int max)
    {
        maxRows = max;
    }

    public void setPositionArray(float[] posArray)
    {
        positionArray = posArray;
    }

    // Update is called once per frame
    void Update()
    {
        // spawn obstacles

        if (t < Time.time)
        {
            if (delay >= 0.25)
            {
                delay = delay - (delay * speedUpFactor);
                mobSpeed = mobSpeed + (mobSpeed * speedUpFactor);
                destroyTime = 30f / mobSpeed;
            }
            SpawnMonsters();
            t = Time.time + delay;
            collectiblesMade = 0;
        }
        
        // spawn collectibles

        if (t - (delay*0.2) > Time.time && t - (delay * 0.8) < Time.time)
        {
            float chance = Random.Range(1, 100);
            if (chance > 90 && collectiblesMade < collectiblesPerWave)
            {
                float heartChance = Random.Range(1, 100);
                if (heartChance >= 70)
                {
                    SpawnHeart();
                } else
                {
                    SpawnCoin();
                }
                
                collectiblesMade++;
            }
        }
        
        //move all moveable objects

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i] == null)
            {
                gameObjects.RemoveAt(i);
            } else
            {
                gameObjects[i].transform.Translate(Vector3.left * Time.deltaTime * mobSpeed);
            }
            
        } 

    }

    void SpawnMonsters()
    {
        int toMake = Random.Range(1, maxRows);

        List<int> opts = new List<int>();
        int[] outArray = new int[toMake];

        for (int i = 1; i <= maxRows; i++)
        {
            opts.Add(i);
        }

        for (int i = 1; i <= toMake; i++)
        {
            int selection = Random.Range(0, opts.Count);

            outArray[i - 1] = opts[selection];
            opts.RemoveAt(selection);

        }

        foreach (int i in outArray)
        {
            tempObj = Instantiate(monster, new Vector3(xPosition, positionArray[i - 1], 0f), Quaternion.identity);
            Destroy(tempObj, destroyTime);
            gameObjects.Add(tempObj);
        }

        tempObj = Instantiate(mobWall, new Vector3(xPosition, 0f, 0f), Quaternion.identity);
        Destroy(tempObj, destroyTime);
        gameObjects.Add(tempObj);
    }
    void SpawnCoin()
    {
        int pos = Random.Range(1, maxRows);

        tempObj = Instantiate(coin, new Vector3(xPosition, positionArray[pos - 1], 0f), Quaternion.identity);
        Destroy(tempObj, destroyTime);
        gameObjects.Add(tempObj);
    }
    void SpawnHeart()
    {
        int pos = Random.Range(1, maxRows);

        tempObj = Instantiate(heart, new Vector3(xPosition, positionArray[pos - 1], 0f), Quaternion.identity);
        Destroy(tempObj, destroyTime);
        gameObjects.Add(tempObj);
    }
}
