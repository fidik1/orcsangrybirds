using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float grav;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grav = rb.gravityScale;
        rb.gravityScale = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Pig" || col.gameObject.tag == "Brick")
            rb.gravityScale = grav;
    }
}
