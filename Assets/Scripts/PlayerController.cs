using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Position")]
    public int rowPosition = 1; //current lane of the player
    public float offset; //border for the playable area in the y axis
    public float[] positionArray; //hold y coordinates player can move to
    private float boxSize;

    [Header("Scripts")]
    public MonsterController mobController;
    public ScoreHandler scoreHandler;
    public Health healthHandler;
    public laneHandler lanes;

    private float lastCollision = 0f;
	private Vector2 newPosition;
	private float step = 100f;

    [Header("Particle Effects")]
    public GameObject moveParticle;
    public GameObject collisionParticle;
    public GameObject coinParticle;
    public GameObject heartParticle;
    public GameObject inkParticle;

    [Header("Components")]
    public Animator camAnimator;
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        positionArray = lanes.GetPositionArray();
		transform.position = Vector2.MoveTowards(transform.position, newPosition, step * Time.deltaTime);


        if (Input.GetKeyDown("up"))
        {
            changePosition(1);
        } else if (Input.GetKeyDown("down"))
        {
            changePosition(-1);
        }

    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (boxSize == 0f)
        {
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(2f, 10f-offset, 0f));
        } else
        {
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(2f, boxSize, 0f));
        }
    }
    void changePosition(int direction)
    {
		
        if (direction == 1 && rowPosition != lanes.rowCount-1)
        {
            camAnimator.SetTrigger("shake");
			newPosition = new Vector2(transform.position.x, positionArray[rowPosition + 1]);
            rowPosition++;
            Instantiate(moveParticle, transform.position, Quaternion.identity);

        } else if (direction == -1 && rowPosition !=0)
        {
            camAnimator.SetTrigger("shake");
            newPosition = new Vector2(transform.position.x, positionArray[rowPosition - 1]);
            rowPosition--;
            Instantiate(moveParticle, transform.position, Quaternion.identity);
        }
		
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("mob")) {
            if(Time.time > lastCollision + 0.1f)
            {
                camAnimator.SetTrigger("shake");
                Instantiate(collisionParticle, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                healthHandler.changeHealth(-1);
                lastCollision = Time.time;
            }
        } else if (collision.gameObject.CompareTag("Coin Collectible"))
        {
            Instantiate(coinParticle, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            scoreHandler.CoinCollected();
        } else if (collision.gameObject.CompareTag("Heart Collectible"))
        {
            Instantiate(heartParticle, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            healthHandler.changeHealth(+1);
        }
    }

    public int getPosition()
    {
        return rowPosition;
    }
}
