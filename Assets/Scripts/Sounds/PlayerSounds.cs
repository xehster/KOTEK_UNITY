using System.Collections;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource walkingSource;
    [SerializeField] private AudioSource talkingSource;
    [SerializeField] private AudioSource weaponSource;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip meleeSound;
    [SerializeField] private AudioClip meleeHitSound;
    private bool isAlreadyWalking;
    [SerializeField] private float speedConfig = 1f;
    public void Walking(bool isWalking, float speed)
    {
        walkingSource.clip = walkingSound;
        walkingSource.loop = true;
        walkingSource.pitch = speedConfig * speed;
        if (isWalking)
        {
            if (!isAlreadyWalking)
            {
                isAlreadyWalking = true;
                walkingSource.Play();
            }
        }
        else
        {
            walkingSource.Stop();
            isAlreadyWalking = false;
        }
    }

    public void PlayCollectSound()
    {
        talkingSource.clip = collectSound;
        talkingSource.loop = false;
        talkingSource.Play();
    }

    public void Death()
    {
        talkingSource.clip = deathSound;
        talkingSource.Play();
    }

    public void Melee()
    {
        weaponSource.clip = meleeSound;
        weaponSource.loop = false;
        weaponSource.Play();
    }

    public void Shoot()
    {
        weaponSource.clip = shootSound;
        weaponSource.loop = false;
        weaponSource.Play();
    }

    public void Jump()
    {
        talkingSource.clip = jumpSound;
        talkingSource.loop = false;
        if (!talkingSource.isPlaying)
        {
            talkingSource.Play();
        }
    }
}
