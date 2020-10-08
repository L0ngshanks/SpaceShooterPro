using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float rotationAngle = 1f;
    [SerializeField]
    private GameObject explosion = null;

    private SpawnManager spawnManager = null;
    private SFX sfx = null;

    private float rotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(spawnManager == null)
        {
            Debug.LogError("SpawnManager is equal to NULL");
        }

        sfx = GameObject.Find("SFX").GetComponent<SFX>();
        if(sfx == null)
        {
            Debug.LogError("SFX is equal to NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotation = rotationAngle * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            spawnManager.SpawnGameObjects();

            sfx.PlayExplosionClip();

            Destroy(collision.gameObject);
            Destroy(this.gameObject, .20f);

        }
    }
}
