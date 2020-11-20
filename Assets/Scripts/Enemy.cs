using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float enemySpeed = 4.0f;
    [SerializeField]
    private float lowerYBoundary = -6.0f;

    [SerializeField]
    private GameObject laser = null;

    private SFX sfx = null;
    Animator anim;

    private float respawnY = 7f;
    private Player player = null;

    private float fireRate = 3f;
    private float canFire = -1f;
    void Start()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-9.25f, 9.25f), respawnY, 0);

        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player does not exist");
        }

        if (laser == null)
        {
            Debug.LogError("Laser is equal to NULL");
        }

        anim = gameObject.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is equal to NULL");
        }

        sfx = GameObject.Find("SFX").GetComponent<SFX>();
        if (sfx == null)
        {
            Debug.LogError("SFX is equal to NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > canFire)
        {
            fireRate = UnityEngine.Random.Range(3f, 8f);
            canFire = Time.time + fireRate;

            GameObject enemyLaser = Instantiate(laser, transform.position, Quaternion.identity);

            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i < lasers.Length; ++i)
            {
                lasers[i].AssignEnemyLaser();
            }
            
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        if (transform.position.y < lowerYBoundary)
        {
            float randomX = UnityEngine.Random.Range(-9.25f, 9.25f);
            transform.position = new Vector3(randomX, respawnY, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            if (player != null)
            {
                player.IncreaseScore(UnityEngine.Random.Range(5, 10));
            }

            if (!other.GetComponent<Laser>().IsEnemyLaser())
            {
                anim.SetTrigger("OnEnemyDeath");
                enemySpeed = 0f;
                sfx.PlayExplosionClip();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                Destroy(other.gameObject);
                Destroy(this.gameObject, 2.8f);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            anim.SetTrigger("OnEnemyDeath");
            enemySpeed = 0f;

            sfx.PlayExplosionClip();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Destroy(this.gameObject, 2.8f);
        }
    }
}
