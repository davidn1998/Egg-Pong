using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    //Configuration Params
    [SerializeField] float speed = 10f;

    //Cache Variables
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector2.up * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector2.down * speed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
