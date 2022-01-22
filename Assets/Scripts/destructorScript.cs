using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructorScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ResizeObject();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
    }

    void ResizeObject()
    {
        float y = Camera.main.orthographicSize * 2;
        transform.position.Scale(new Vector3(0.1f, y, 1f));
    }
}
