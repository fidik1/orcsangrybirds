using UnityEngine;
using System.Collections;

public class BoomBird : Bird
{
    [SerializeField] ParticleSystem particleBoom;
    [SerializeField] float fieldOfImpact;
    [SerializeField] float force;
    [SerializeField] LayerMask layer;
    
    bool isExploded = false;

    public override void OnClick()
    {
        StartCoroutine(Explosion());
    }

    void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layer);
        
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;

            if(direction.magnitude > 0)
            {  
                float explosionForce = force / direction.magnitude;
                obj.GetComponent<Rigidbody2D>().AddForce(direction.normalized * explosionForce);
            }
        }
    }

    public override void InFixedUpdate()
    {
        if (isGrounded && !isExploded)
            StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        isExploded = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        Instantiate(particleBoom, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        Explode();
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
