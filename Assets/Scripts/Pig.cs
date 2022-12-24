using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] float health = 150f;
    [SerializeField] Sprite[] spritePigs;
    [SerializeField] ParticleSystem particlePigDeath;
    [SerializeField] ParticleSystem particleScore;
    PigController pigController;
    float ChangeSpriteHealth;
    bool isDead = false;

    void Start()
    {
        pigController = GameObject.Find("PigController").GetComponent<PigController>();
        GetComponent<SpriteRenderer>().sprite = spritePigs[0];
        ChangeSpriteHealth = health - 30f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (col.gameObject.tag == "Bird")
        {
            GetComponent<AudioSource>().Play();
            Death();
        }
        else
        {
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            health -= damage;
            
            if (damage >= 10)
                GetComponent<AudioSource>().Play();

            if (health < ChangeSpriteHealth)
            {
                GetComponent<SpriteRenderer>().sprite = spritePigs[1];
            }
            if (health < ChangeSpriteHealth - 30f)
            {
                GetComponent<SpriteRenderer>().sprite = spritePigs[2];
            }
            if (health <= 0 && !isDead)
            {
                isDead = true;
                Death();
            }
        }
    }

    void Death()
    {
        GetComponent<AudioSource>().Play();
        Instantiate(particlePigDeath, transform.position, Quaternion.identity);
        Instantiate(particleScore, transform.position, Quaternion.identity);
        pigController.pigs.Remove(gameObject);
        Balance.balance += Random.Range(8, 13);
        Destroy(gameObject);
    }
}
