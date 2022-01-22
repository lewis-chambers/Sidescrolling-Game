using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundMovement : MonoBehaviour
{
	public float movementSpeed;
	public RectTransform[] spritePair;
	private float moveTo;
    // Update is called once per frame
	void Start() 
	{
	}
    void Update()
    {

        foreach (RectTransform obj in spritePair)
		{
			obj.Translate(Vector3.left * Time.deltaTime * movementSpeed);
			
		}
		
		for (int i=0; i <2; i++)
		{
			if (spritePair[i].position.x <= -spritePair[i].sizeDelta.x)
			{
				if (i == 0) {
					moveTo = spritePair[1].position.x + spritePair[1].sizeDelta.x;
				} else {
					moveTo = spritePair[0].position.x + spritePair[0].sizeDelta.x;
				}
				
				spritePair[i].position = new Vector3(moveTo, spritePair[i].position.y, 0f);
			}
		}
    }
}
