using UnityEngine;
using System.Collections;

public class SniperBird : Bird
{
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject bullet;
    [SerializeField] float force;

    public override void OnClick()
    {
        Sniper();
    }

    void Sniper()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        GameObject ch = Instantiate(crosshair, target, Quaternion.identity);
        ch.transform.position = new Vector3(ch.transform.position.x, ch.transform.position.y, 0);
        Destroy(ch, 1);

        Vector3 difference = target - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        
        GameObject bul = Instantiate(bullet, transform.position, Quaternion.identity);
        bul.GetComponent<Rigidbody2D>().velocity = direction * force;
        bul.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        StartCoroutine(Bullet(bul));
        Destroy(bul, 2);
    }

    IEnumerator Bullet(GameObject bullet)
    {
        yield return new WaitForSeconds(0.05f);
        bullet.GetComponent<CapsuleCollider2D>().isTrigger = false;
    }
}
