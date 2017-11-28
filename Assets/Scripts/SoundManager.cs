using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource myFirstAudioSource;
    [SerializeField] private AudioSource mySecondAudioSource;

    [Header("EnemysSounds")]
    [SerializeField]
    private AudioClip zombiesAttack;
    [SerializeField] private AudioClip zombiesDeath;
    [SerializeField] private AudioClip knifeThrow;
    [SerializeField] private AudioClip bossDeath;

    [Header("PlayerSounds")]
    [SerializeField]
    private AudioClip[] playerStepSound;
    [SerializeField] private AudioClip shot;

    [Header("EnvironmentSounds")]
    [SerializeField]
    private AudioClip doorOpen;
    [SerializeField] private AudioClip getPowerUp;

    //Constants
    private const float zombieAttackVolume = 0.7f;
    private const float zombieDeathVolume = 0.3f;
    private const float playerStepVolume = 0.9f;
    private const float shotVolume = 0.6f;
    private const float doorPowerUpVolume = 1f;
    private const float throwKnifeVolume = 1f;
    private const float bossDeathVolume = 1f;

    public void PlayZombiesAttack()
    {
        mySecondAudioSource.clip = zombiesAttack;
        mySecondAudioSource.volume = zombieAttackVolume;
        mySecondAudioSource.Play();
    }

    public void PlayZombiesDeath()
    {
        mySecondAudioSource.clip = zombiesDeath;
        mySecondAudioSource.volume = zombieDeathVolume;
        mySecondAudioSource.Play();
    }

    public void PlayPlayerStep()
    {
        int indexSoundRandom = Random.Range(0, playerStepSound.Length);
        myFirstAudioSource.clip = playerStepSound[indexSoundRandom];
        myFirstAudioSource.volume = playerStepVolume;
        myFirstAudioSource.Play();
    }

    public void PlayShot()
    {
        myFirstAudioSource.clip = shot;
        myFirstAudioSource.volume = shotVolume;
        myFirstAudioSource.Play();
    }

    public void PlayKnifeThrow()
    {
        mySecondAudioSource.clip = knifeThrow;
        mySecondAudioSource.volume = throwKnifeVolume;
        mySecondAudioSource.Play();
    }


    public void PlayBossDeath()
    {
        mySecondAudioSource.clip = bossDeath;
        mySecondAudioSource.volume = bossDeathVolume;
        mySecondAudioSource.PlayOneShot(bossDeath);
    }

    public void PlayDoorOpen()
    {
        mySecondAudioSource.clip = doorOpen;
        mySecondAudioSource.volume = doorPowerUpVolume;
        mySecondAudioSource.Play();
    }

    public void PlayPowerUp()
    {
        mySecondAudioSource.clip = getPowerUp;
        mySecondAudioSource.volume = doorPowerUpVolume;
        mySecondAudioSource.Play();
    }
}
