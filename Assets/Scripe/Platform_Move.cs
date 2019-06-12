using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    public Transform pos1, pos2;
    public Transform startPos;
    Vector3 nextPos;
    public Rigidbody2D rb;
    public bool facingRight;
    public float speed;
    private bool moving;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if( transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "FlipPoint")
    //    {
    //        flip();
    //    }
    //    //if (collision.gameObject.tag == "Enemy")
    //    //{
    //    //    flip();
    //    //}
    //}
    //void flip()
    //{
    //    facingRight = !facingRight;
    //    Vector3 scaleFactor = transform.localScale;
    //    //scaleFactor.x *= -1;
    //    transform.localScale = scaleFactor;
    //}
}

