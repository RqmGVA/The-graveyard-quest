using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public int life = 30;
    private float timeToDisappear = 2f;
    private const float bossSize = 0.5f;
    private bool lookingLeft;
    [SerializeField] private float moveSpeed;

    public GameObject Target { get; set; }

    private IBossState currentState;

    private SoundManager soundManager;

    private Collider2D bossCollider;
    public Animator bossAnimator;
    [SerializeField] private Collider2D player;
    [SerializeField] private Transform knifePosition;
    [SerializeField] private GameObject knifePrefab;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        bossAnimator = GetComponent<Animator>();
        bossCollider = GetComponent<Collider2D>();
        lookingLeft = true;

        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDirection = Target.transform.position.x - transform.position.x;
            if ((xDirection < 0 && !lookingLeft) || (xDirection > 0 && lookingLeft))
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IBossState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        bossAnimator.SetFloat("Speed", moveSpeed);
        transform.Translate(GetDirection() * moveSpeed * Time.deltaTime);

    }

    public Vector2 GetDirection()
    {
        return lookingLeft ? Vector2.left : Vector2.right;
    }

    public void ChangeDirection()
    {
        lookingLeft = !lookingLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, bossSize, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            BossTakeDamage();
            if (Target == null)
            {
                ChangeDirection();
            }
            Destroy(collision.gameObject);
        }

        Physics2D.IgnoreCollision(bossCollider, player, true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

    public void ThrowKnife(int value)
    {
        if (lookingLeft)
        {
            soundManager.PlayKnifeThrow();
            GameObject knife = Instantiate(knifePrefab, knifePosition.position, knifePosition.rotation);
            knife.GetComponent<KnifeManager>().GetDirection(Vector2.left);
        }
        else
        {
            soundManager.PlayKnifeThrow();
            GameObject knife = Instantiate(knifePrefab, knifePosition.position, knifePosition.rotation);
            knife.GetComponent<KnifeManager>().GetDirection(Vector2.right);
            knife.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void BossTakeDamage()
    {
        life--;
        if (life == 0)
        {
            currentState = new IdleState();
            bossAnimator.SetTrigger("Death");
            soundManager.PlayBossDeath();
        }
    }
}
