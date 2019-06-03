using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileLifeTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileLifeTime);
        projectileSpeed = 5.0f;

        rb = GetComponent<Rigidbody2D>();

        if (transform.localRotation.y==0)
        {
            rb.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.right * projectileSpeed, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
