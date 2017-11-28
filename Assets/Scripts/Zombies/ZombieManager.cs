using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] private int life = 3;
    [SerializeField] private float moveSpeed = 5f;
    private float timeToDisappear = 2f;

    //Constants
    private const int rotation = -90;
    private const float distanceToFloor = 0.55f;
    private const float minimumDistanceToPlayer = 1f;

    //Sound
    private SoundManager soundManager;
    //Component
    public BoxCollider2D zombieCollider;
    public Rigidbody2D zombieRigid;
    public Animator zombieAnimator;
    private ZombiesSpawner zombieSpawner;

    private GameManager gameManager;

    private PlayerControler playerControler;
    // Use this for initialization
    void Start()
    {

        soundManager = FindObjectOfType<SoundManager>();
        zombieCollider = GetComponent<BoxCollider2D>();
        zombieRigid = GetComponent<Rigidbody2D>();
        zombieAnimator = GetComponent<Animator>();
        zombieSpawner = FindObjectOfType<ZombiesSpawner>();

        gameManager = FindObjectOfType<GameManager>();

        playerControler = FindObjectOfType<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "SpawnedZombie" && life > 0)
        {
            //Make zombie move to the player
            transform.LookAt(playerControler.transform);
            transform.Rotate(new Vector3(0, rotation, 0), Space.Self);
            if (Vector3.Distance(transform.position, playerControler.transform.position) > minimumDistanceToPlayer)
            {
                zombieAnimator.SetFloat("ZombieSpeed", moveSpeed);
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ZombieTakeDamage();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Player")
        {
            zombieAnimator.SetTrigger("CollisionPlayer");
            playerControler.PlayerTakeDamage();
            soundManager.PlayZombiesAttack();
        }
        //Menu zombies
        if (collision.gameObject.tag == "Bullet" && gameObject.name == "ZombieStart")
        {
            StartZombieTakeDammage();
        }

        if (collision.gameObject.tag == "Bullet" && gameObject.name == "ZombieExit")
        {
            ExitZombieTakeDammage();
        }
    }

    private void ZombieTakeDamage()
    {
        life--;
        if (life <= 0)
        {
            zombieCollider.enabled = false;
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        soundManager.PlayZombiesDeath();
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - distanceToFloor, transform.position.z), new Quaternion());

        zombieAnimator.SetTrigger("Death");

        Destroy(zombieRigid);
        zombieCollider.enabled = false;

        yield return new WaitForSeconds(timeToDisappear);
        Destroy(gameObject);
    }

    //MenuManager
    private void StartZombieTakeDammage()
    {
        life--;

        if (life <= 0)
        {
            SceneManager.LoadScene("GameLevel");
        }
    }

    private void ExitZombieTakeDammage()
    {
        life--;

        if (life <= 0)
        {
            Application.Quit();
        }
    }
}
