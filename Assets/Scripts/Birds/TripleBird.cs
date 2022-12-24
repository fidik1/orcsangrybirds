using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBird : Bird
{
    [SerializeField] float force;
    public override void OnClick()
    {
        Triple();
    }

    void Triple()
    {
        GameObject bird = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + 0.25f), Quaternion.identity);
        bird.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        bird.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        bird = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y - 0.25f), Quaternion.identity);
        bird.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        bird.GetComponent<Rigidbody2D>().AddForce(Vector2.down * force);
    }
}
