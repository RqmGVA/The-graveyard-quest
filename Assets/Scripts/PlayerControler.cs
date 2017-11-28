using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private Animator playerAnimationControler;
    private Transform playerTransform;
    MenuManager MenuManager;

    private const float playerSize = 0.3358049f;
    private const float blinkingTime = 0.1f;

    [Header("Physics")]
    private Rigidbody2D rigidPlayer;
    private BoxCollider2D colliderPlayer;
    [SerializeField] private float moveForce = 5;

    [Header("Jump")]
    [SerializeField]
    private Transform positionRaycastJump;
    [SerializeField] private float radiusRaycastJump;
    [SerializeField] private LayerMask layerMaskJump;
    [SerializeField] private float forceJump = 5;

    [Header("Fire")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private float timeToFire = 2;
    private float lastTimeFire = 0;
    private bool lookingLeft = true;

    [Header("Zombies things")]
    [SerializeField]
    private GameObject zombieSpawnerRoom1;
    [SerializeField] private GameObject[] zombieSpawnerRoom2;
    [Header("Sounds")]
    [SerializeField]
    private SoundManager soundManager;
    private float lastStepSound = 0;
    private float timeToStepSound = 0.5f;

    [Header("Life")]
    public int playerLife = 5;

    //Immortality
    private bool immortal = false;
    [SerializeField] private float immortalTime;

    [Header("Keys and Chests")]
    [SerializeField]
    private GameObject chest;
    [SerializeField] private GameObject key2;
    [SerializeField] private GameObject door;

    [Header("Boss")]
    [SerializeField]
    private GameObject bossLifeBar;
    // Use this for initialization
    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimationControler = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        MenuManager = FindObjectOfType<MenuManager>();
        rigidPlayer = GetComponent<Rigidbody2D>();
        colliderPlayer = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move horizontal
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {


            playerTransform.localScale = new Vector3(-playerSize, playerSize, 1);
            lookingLeft = false;
        }
        if (horizontalInput > 0)
        {

            playerTransform.localScale = new Vector3(playerSize, playerSize, 1);
            lookingLeft = true;
        }

        playerAnimationControler.SetFloat("SpeedX", Mathf.Abs(horizontalInput));
        rigidPlayer.velocity = new Vector2(moveForce * horizontalInput, rigidPlayer.velocity.y);

        //Slide

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            playerAnimationControler.SetTrigger("Slide");
        }
        //Jump
        bool touchFloor = Physics2D.OverlapCircle(positionRaycastJump.position, radiusRaycastJump, layerMaskJump);
        playerAnimationControler.SetBool("IsGrounded", touchFloor);

        if (Input.GetAxis("Jump") > 0 && touchFloor)
        {
            playerAnimationControler.SetTrigger("Jump");
            rigidPlayer.velocity = new Vector2(rigidPlayer.velocity.x, forceJump);
        }
        else
        {
            playerAnimationControler.SetBool("IsGrounded", touchFloor);
        }

        //StepSound
        if ((horizontalInput > 0 || horizontalInput < 0) && touchFloor == true)
        {
            PlayStepSound();
        }
        //Fire
        if (Input.GetAxis("Fire1") > 0)
        {
            playerAnimationControler.SetTrigger("HasShot");
            Fire();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Zombies Spawners Management
        if (collision.name == "ActivateZombieSpawnerRoom1")
        {
            zombieSpawnerRoom1.SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.name == "DisableZombieSpawnerRoom1")
        {
            zombieSpawnerRoom1.SetActive(false);
            Destroy(collision.gameObject);
        }

        if (collision.name == "ActivateZombieSpawnerRoom2")
        {
            foreach (GameObject spawner in zombieSpawnerRoom2)
            {
                spawner.SetActive(true);
            }
            Destroy(collision.gameObject);
        }

        if (collision.name == "DisableZombieSpawnerRoom2")
        {
            foreach (GameObject spawner in zombieSpawnerRoom2)
            {
                spawner.SetActive(false);
            }
            Destroy(collision.gameObject);
        }

        // Slide power activation
        if (collision.name == "NewPower")
        {
            soundManager.PlayPowerUp();
            playerAnimationControler.SetBool("SlideActive", true);
            MenuManager.newPowerPanel.SetActive(true);
            MenuManager.isPanelActive = true;
            Destroy(collision.gameObject);
        }

        //Sign management
        if (collision.name == "IntroductionSign")
        {
            MenuManager.introductionPanel.SetActive(true);
            MenuManager.isPanelActive = true;
        }

        if (collision.name == "ParchmentExplanation")
        {
            MenuManager.improveSkillPanel.SetActive(true);
            MenuManager.isPanelActive = true;
        }

        if (collision.name == "Room2Sign")
        {
            MenuManager.room2Panel.SetActive(true);
            MenuManager.isPanelActive = true;
        }

        //Keys Management
        if (collision.name == "Key 1")
        {
            key2.SetActive(true);
            soundManager.PlayDoorOpen();
            Destroy(collision.gameObject);
        }
        if (collision.name == "Key 2")
        {
            Destroy(door);
            soundManager.PlayDoorOpen();
            Destroy(collision.gameObject);
        }

        //Boss Life Bar
        if (collision.name == "ActivateBossLife")
        {
            bossLifeBar.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            playerTransform.SetParent(collision.gameObject.transform);
        }
        if (collision.gameObject.tag == "SpawnedZombie")
        {
            StartCoroutine(PlayerTakeDamage());
        }
        if (collision.gameObject.tag == "Knife")
        {
            StartCoroutine(PlayerTakeDamage());
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            playerTransform.SetParent(null);
        }
    }

    private void PlayStepSound()
    {
        if (Time.realtimeSinceStartup - lastStepSound > timeToStepSound)
        {
            soundManager.PlayPlayerStep();
            lastStepSound = Time.realtimeSinceStartup;
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkingTime);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkingTime);
        }
    }

    public IEnumerator PlayerTakeDamage()
    {
        if (!immortal)
        {
            playerLife--;
            if (playerLife <= 0)
            {
                playerAnimationControler.SetTrigger("IsDead");
            }
            immortal = true;
            StartCoroutine(IndicateImmortal());
            yield return new WaitForSeconds(immortalTime);
            immortal = false;
        }
    }

    private void Fire()
    {
        if (Time.realtimeSinceStartup - lastTimeFire > timeToFire)
        {
            if (lookingLeft)
            {
                soundManager.PlayShot();
                GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
                bullet.GetComponent<BulletManager>().GetDirection(Vector2.right);
                Destroy(bullet, 5);
                lastTimeFire = Time.realtimeSinceStartup;
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else
            {
                soundManager.PlayShot();
                GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
                bullet.GetComponent<BulletManager>().GetDirection(Vector2.left);
                Destroy(bullet, 5);
                lastTimeFire = Time.realtimeSinceStartup;
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(positionRaycastJump.position, radiusRaycastJump);
    }
}
