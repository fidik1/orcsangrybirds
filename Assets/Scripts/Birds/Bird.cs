using UnityEngine;
using System.Collections;
using Assets.Scripts;

public abstract class Bird : MonoBehaviour
{
    public BirdState State { get; set; }
    [SerializeField] ParticleSystem particleBird;
    [SerializeField] ParticleSystem particleFeather;
    [SerializeField] Color color;
    public bool isGrounded;

    void Start()
    {
        State = BirdState.BeforeThrown;
        isGrounded = false;
        ParticleSystem.MainModule psmain = particleFeather.main;
        psmain.startColor = color;
        Invoke("Death", 5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && State == BirdState.Thrown && !isGrounded)
        {
            OnClick();
            State = BirdState.Clicked;
        }
    }

    void FixedUpdate()
    {
        CheckVelocity();
        InFixedUpdate();
    }

    public virtual void InFixedUpdate()
    {

    }

    void CheckVelocity()
    {
        if (State == BirdState.Thrown && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.15f || 
            State == BirdState.Clicked && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.15f)
            StartCoroutine(Death());
    }

    public void OnThrow()
    {
        GetComponent<TrailRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        State = BirdState.Thrown;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        Instantiate(particleBird, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual void OnClick()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brick" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pig")
        {
            isGrounded = true;
            Instantiate(particleFeather, transform.position, Quaternion.Euler(-90, 0, 0));
        }
    }
}
