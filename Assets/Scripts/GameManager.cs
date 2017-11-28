using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    private PlayerControler playerControler;
    private BossManager bossManager;
    private const int WaitDieAnimation = 2;
    // Use this for initialization
    void Start()
    {
        playerControler = FindObjectOfType<PlayerControler>();
        bossManager = FindObjectOfType<BossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControler.playerLife <= 0)
        {
            StartCoroutine(WaitZombieAnimationDie());
        }
        if (bossManager.life <= 0)
        {
            StartCoroutine(WaitBossAnimationDie());
        }
    }

    private IEnumerator WaitZombieAnimationDie()
    {
        yield return new WaitForSeconds(WaitDieAnimation);
        SceneManager.LoadScene("LooseScene");
    }

    private IEnumerator WaitBossAnimationDie()
    {
        yield return new WaitForSeconds(WaitDieAnimation);
        SceneManager.LoadScene("VictoryScene");
    }




}
