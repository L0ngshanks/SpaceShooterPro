using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float destroyPointY = -5f;
    [Header("2 = Shields")]
    [Header("1 = Speed")]
    [Header("0 = Triple Shot")]
    [Header("Type = {0,1,2}")]
    [SerializeField]
    private int powerUpID = 0;
    //PowerUp ID
    // 0 = TripleShot
    // 1 = Speed
    // 2 = Shield

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(-9.25f, 9.25f), 8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < destroyPointY)
        { 
            DestroyPowerUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if(player != null)
            {
                switch(powerUpID)
                {
                    case 0:
                        player.EnableTripleShot();
                        break;
                    case 1:
                        player.EnableSpeedBoost();
                        break;
                    case 2:
                        player.EnablePlayerShield();
                        break;
                    case 3:
                        player.AddAmmo();
                        break;
                    default:
                        Debug.LogWarning("PowerUp ID Default value.");
                        break;
                }

                Destroy(this.gameObject);
            }
        }
    }

    void DestroyPowerUp()
    {
        Destroy(this.gameObject);
    }
}
