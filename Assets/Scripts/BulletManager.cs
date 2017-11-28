using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Vector2 path;

    private Rigidbody2D bulletRigid;
    [SerializeField] private float bulletSpeed;
    private Collider2D bulletCollider;
    [SerializeField] private Collider2D knifeCollider;
    // Use this for initialization
    void Start()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigid.velocity = path * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground"
            || collision.gameObject.tag == "Object") && gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Knife")
            Physics2D.IgnoreCollision(bulletCollider, knifeCollider, true);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void GetDirection(Vector2 path)
    {
        this.path = path;
    }
}
