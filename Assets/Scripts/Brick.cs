using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] float health = 70f;
    [SerializeField] Sprite[] sprites;

    float maxHealth;
    AudioSource source;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        maxHealth = health;
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) 
            return;

        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

        health -= damage;

        if (damage >= 10)
            source.Play();
       

        if (health <= maxHealth / 1.5f)
            spriteRenderer.sprite = sprites[1];
        else if (health >= maxHealth / 3 && health < maxHealth / 2) 
            spriteRenderer.sprite = sprites[2];
        else if (health <= 0) 
            Destroy(this.gameObject);
    }
}
