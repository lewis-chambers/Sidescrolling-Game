using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneHandler : MonoBehaviour
{

    public int rowCount = 3;
    public float[] positionArray;
    public GameObject player;
    public PlayerController playerScript;
    public MonsterController mobController;

    private float boxSize;
    public float offset;
    private int rowPosition;

    // Start is called before the first frame update
    void Start()
    {
        boxSize = (Camera.main.orthographicSize * 2) - offset;
        calculateRows(0);
        mobController.setMaxRows(rowCount);
        mobController.setPositionArray(positionArray);
    }

    public void addScripts(MonsterController m)
    {
        mobController = m;
    }
    public void setMaxRows(int max)
    {
        rowCount = max;
    }

    public void setPositionArray(float[] posArray)
    {
        positionArray = posArray;
    }

    public void calculateRows(int key) //populates postionArray with valid y coordinates
    {
        positionArray = new float[rowCount];
        for (int i = 0; i < positionArray.Length; i++)
        {
            positionArray[i] = (i * boxSize / (rowCount - 1)) - (boxSize / 2);
        }

        //Sets player to center if 0 is passed on start
        if (key == 0)
        {
            rowPosition = Mathf.RoundToInt(rowCount / 2);
        } else
        {
            rowPosition = playerScript.getPosition();
        }

        
        transform.position = new Vector3(transform.position.x, positionArray[rowPosition], 0f);
    }

    public void ChangeRows(int newRows)
    {
        rowCount = newRows;
        calculateRows(1);
        mobController.setMaxRows(newRows);
        mobController.setPositionArray(positionArray);
    }

    public float[] GetPositionArray()
    {
        return positionArray;
    }
}
