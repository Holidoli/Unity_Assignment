using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    private Rigidbody2D MarioRB;
    public float bounceForce;
    
    // Start is called before the first frame update
    void Start()
    {
        MarioRB = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            AudioScript.instance.PlayAudioClip(AudioScript.instance.EnemyDieClip, 3);
            MarioRB.velocity = new Vector3(MarioRB.velocity.x, bounceForce, 0f);
        }
    }
}
