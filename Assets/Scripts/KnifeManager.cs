using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{
    private Vector2 path;
    private Rigidbody2D knifeRigid;
    [SerializeField] private float knifeSpeed;

    // Use this for initialization
    void Start()
    {
        knifeRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        knifeRigid.velocity = path * knifeSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name == "Player" || collision.gameObject.tag == "Ground"
            || collision.gameObject.tag == "Object") && gameObject.tag == "Knife")
        {
            Destroy(gameObject);
        }
    }
    public void GetDirection(Vector2 path)
    {
        this.path = path;
    }
}
