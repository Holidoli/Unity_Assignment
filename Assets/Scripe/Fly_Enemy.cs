using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Enemy : MonoBehaviour
{
    public int health;

    public Rigidbody2D rb;
    public bool facingRight;
    public float speed;

    public Transform leftPoint;
    public Transform rightPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            rb.velocity = new Vector3(rb.velocity.x, speed, 0f);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, -speed, 0f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "FlipPoint")
        //{
        //    flip();
        //}
        if (collision.gameObject.tag == "Enemy")
        {
            flip();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlipPoint")
        {
            flip();
        }
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    flip();
        //}
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector3 scaleFactor = transform.localScale;
        //scaleFactor.x *= -1;
        transform.localScale = scaleFactor;
    }
}


