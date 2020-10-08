using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable
    [SerializeField]
    private float laserSpeed = 0.0f;
    [SerializeField]
    private float laserMaxHeight = 8.0f;
    private bool isEnemyLaser = false;
    // Update is called once per frame
    void Update()
    {
        if (!isEnemyLaser)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
            transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
            if (transform.position.y > laserMaxHeight)
            {
                if(this.transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }

    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * laserSpeed * Time.deltaTime);
        if (transform.position.y < laserMaxHeight * -1)
        {
            if (this.transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }

    public void AssignEnemyLaser()
    {
        isEnemyLaser = true;
    }

    public bool IsEnemyLaser()
    {
        return isEnemyLaser;
    }
}
