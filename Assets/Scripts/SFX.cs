using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField]
    private AudioClip laser = null;
    [SerializeField]
    private AudioClip explosion = null;
    [SerializeField]
    private AudioClip powerUp = null;

    private AudioSource audioSource = null;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogError("AudioSource is equal to null");
        }
    }

    public void PlayLaserClip()
    {
        audioSource.PlayOneShot(laser);
    }
    public void PlayExplosionClip()
    {
        audioSource.clip = explosion;
        audioSource.PlayOneShot(explosion);
    }
    public void PlayPowerUpClip()
    {
        audioSource.clip = powerUp;
        audioSource.PlayOneShot(powerUp);
    }

}
