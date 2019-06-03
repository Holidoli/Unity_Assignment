using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float fireRate;
    public Rigidbody2D projectilePrefab;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public bool shootLeft;
    public GameObject target = null;
    float timeSinceLastFire = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!target)
            target = GameObject.FindWithTag("Player");
    }

    void shootDirectionCheck()
    {
        if (target.transform.position.x < transform.position.x)
            shootLeft = true;
        else
            shootLeft = false;
    }

    void fireProjectile()
    {
        shootDirectionCheck();

        if(shootLeft)
        {
            Instantiate(projectilePrefab, leftSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            Instantiate(projectilePrefab, rightSpawnPoint.position, Quaternion.Euler(new Vector3(0, 180.0f, 0)));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Time.time > timeSinceLastFire + fireRate)
            {
                fireProjectile();

                timeSinceLastFire = Time.time;
            }
        }
    }
}
