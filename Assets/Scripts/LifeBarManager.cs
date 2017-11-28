using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeBarManager : MonoBehaviour
{
    //Player
    private float currentPlayerHP;
    private float maxPlayerHP = 10;
    private float percentPlayerLife;
    [SerializeField] private Image lifePlayerBar;
    private PlayerControler playerControler;

    //Boss
    private float currentBossHP;
    private float maxBossHP = 30;
    private float percentBossLife;
    [SerializeField] private Image lifeBossBar;
    private BossManager bossManager;

    private void Start()
    {
        playerControler = FindObjectOfType<PlayerControler>();
        bossManager = FindObjectOfType<BossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player
        currentPlayerHP = playerControler.playerLife;
        percentPlayerLife = currentPlayerHP / maxPlayerHP;
        //Boss
        currentBossHP = bossManager.life;
        percentBossLife = currentBossHP / maxBossHP;
    }

    void OnGUI()
    {
        lifePlayerBar.fillAmount = percentPlayerLife;
        lifeBossBar.fillAmount = percentBossLife;
    }

}
