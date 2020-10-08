﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private int score = 0;

    [Header("Player Damage")]
    [SerializeField]
    private GameObject leftEngine = null;
    [SerializeField]
    private GameObject rightEngine = null;
    [SerializeField]
    private GameObject explosion = null;

    [Header("Play Field Bounds")]
    [SerializeField]
    private float rightX = 11.0f;
    [SerializeField]
    private float leftX = -11.0f;
    [SerializeField]
    private float lowerY = -3.6f;

    [Header("Projectile")]
    [SerializeField]
    private GameObject laserProjectile = null;
    [SerializeField]
    private GameObject tripleShotProjectile = null;
    [SerializeField]
    private float laserOffset = 0.8f;
    [SerializeField]
    private float fireRate = 0.15f;
    private float fireNext = -1.0f;

    [Header("Power Up")]
    [SerializeField]
    private float tripleShotLifeSpan = 5f;
    [SerializeField]
    private float speedBoostLifeSpan = 5f;
    [SerializeField]
    private float speedBoost = 5f;
    [SerializeField]
    GameObject playerShield = null;
    [SerializeField]
    private float shieldLifeSpawn = 5f;

    [Header("Audio")]
    [SerializeField]
    private GameObject sfx = null;

    private bool isTripleShotActive = false;
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;
    //0 = Left, 1 = Right
    private int leftBurning = 0;
    private int rightBurning = 2;

    private SpawnManager spawnManager = null;
    private UI_Manager ui_manager = null;


    [Header("Input Debugging")]
    public float horizonalInput = 0.0f;
    public float verticalInput = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //take current position and assign it a start position (0,0,0)
        transform.position = Vector3.zero;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(spawnManager == null)
        {
            Debug.LogError("SpawnManager is equal to NULL");
        }

        ui_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if(ui_manager == null)
        {
            Debug.LogError("ui_manager is equal to NULL");
        }

        if (playerShield == null)
        {
            Debug.LogError("PlayerShield is equal to NULL");
        }

        if(sfx == null)
        {
            Debug.LogError("SFX is equal to NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > fireNext)
        {
            ShootLaser();
        }
    }

    void PlayerBounds()
    {
        Vector3 currentPosition = transform.position;

        transform.position = new Vector3(currentPosition.x, Mathf.Clamp(currentPosition.y, lowerY, 0), 0);

        if (currentPosition.x > rightX || currentPosition.x < leftX)
        {
            transform.position = new Vector3(currentPosition.x * -1, currentPosition.y, 0);
        }
    }

    void CalculateMovement()
    {
        //transform.Translate(Time.deltaTime,0, 0);

        horizonalInput = Input.GetAxis("Horizontal");
        //transform.Translate((Vector3.right * horizonalInput) * movementSpeed * Time.deltaTime);

        verticalInput = Input.GetAxis("Vertical");
        //transform.Translate((Vector3.up * verticalInput) * movementSpeed * Time.deltaTime);

        if (movementSpeed > 5f && !isSpeedBoostActive)
        {
            movementSpeed = 5f;
        }

        transform.Translate(new Vector3(horizonalInput, verticalInput, 0) * movementSpeed * Time.deltaTime);

        PlayerBounds();
    }

    void ShootLaser()
    {
        fireNext = Time.time + fireRate;

        if (!isTripleShotActive)
        {
            if (laserProjectile != null)
            {
                Instantiate(laserProjectile, transform.position + new Vector3(0, laserOffset, 0), Quaternion.identity);
            }
        }
        else
        {
            if(tripleShotProjectile != null)
            {
                Instantiate(tripleShotProjectile, transform.position + new Vector3(0, laserOffset, 0), Quaternion.identity);
            }
        }

        sfx.GetComponent<SFX>().PlayLaserClip();
    }
    public void Damage()
    {
        if(isShieldActive)
        {
            if(playerShield != null)
            {
                playerShield.SetActive(false);
                isShieldActive = false;
                return;
            }
        }

        lives -= 1;

        Mathf.Clamp(lives, 0f, 3f);

        EnableDamage();
        ui_manager.UpdateLives(lives);

        CheckLife();
    }
    void CheckLife()
    {
        if (lives < 1)
        {
            if(spawnManager != null)
            {
                spawnManager.OnPlayerDeath();
            }

            ui_manager.UpdateGameOverText(false);

            Instantiate(explosion, transform.position, Quaternion.identity);

            sfx.GetComponent<SFX>().PlayExplosionClip();

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, .2f);
        }
    }

    public void EnableTripleShot()
    {
        isTripleShotActive = true;

        sfx.GetComponent<SFX>().PlayPowerUpClip();
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(tripleShotLifeSpan);
        isTripleShotActive = false;
    }

    public void EnableSpeedBoost()
    {
        isSpeedBoostActive = true;
        if(movementSpeed == 5f)
        {
            movementSpeed += speedBoost;
        }
        sfx.GetComponent<SFX>().PlayPowerUpClip();
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(speedBoostLifeSpan);
        isSpeedBoostActive = false;
    }

    public void EnablePlayerShield()
    {
        isShieldActive = true;
        playerShield.SetActive(true);
        sfx.GetComponent<SFX>().PlayPowerUpClip();
        StartCoroutine(PlayerShieldPowerDownRoutine());
    }

    IEnumerator PlayerShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(shieldLifeSpawn);
        isShieldActive = false;
        playerShield.SetActive(false);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        ui_manager.UpdatePlayerScore(score);
    }

    void EnableDamage()
    {
        int engine = UnityEngine.Random.Range(leftBurning, rightBurning);
        
        if(engine == 0)
        {
            leftEngine.SetActive(true);
            leftBurning = 1;
        }
        else
        {
            rightEngine.SetActive(true);
            rightBurning = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            if (collision.GetComponent<Laser>().IsEnemyLaser())
            {
                Damage();
                Destroy(collision.gameObject);
            }
        }
    }
}