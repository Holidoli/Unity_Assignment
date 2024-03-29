﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Control : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;

    private Rigidbody2D myrigidbody;

    //private UI _Ui;

    [SerializeField]
    private AudioClip jump_SF;

    [SerializeField]
    private float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;
    public bool isFacingLeft;

    private Animator myAnim;

    private bool Attacking1;
    private bool Attacking2;
    private bool stop = false;
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public float projectileForce;
    public bool CanFireBall;

    public static int health;
    public bool Win = false;

    public bool characterDisabled;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        Projectile_Check();
        //_Ui = GameObject.Find("UI").GetComponent<UI>();
        characterDisabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Win || health <= 0)
        {
            characterDisabled = true;
        }

        if (!characterDisabled)
        {

            float moveValue = Input.GetAxisRaw("Horizontal");

            Player_Movement();
            Player_Jump();
            handleInput();

            myAnim.SetFloat("Speed", Mathf.Abs(myrigidbody.velocity.x));
            myAnim.SetBool("Ground", isGrounded);

            if (((isFacingLeft && moveValue > 0) || (!isFacingLeft && moveValue < 0)) && !Pause_Game.GameIsPaused)
            {
                //print(isFacingLeft + " " + moveValue);
                flip();
                Player_Movement();
                Player_Jump();

            }
        }

        if (Win)
        {
            ReachGoal();
        }

    }
    void Player_Movement()
    {
        //if (Pause_Game.GameIsPaused)
        //    return;

        if (stop == true)
            return;


        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myrigidbody.velocity = new Vector3(MoveSpeed, myrigidbody.velocity.y, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myrigidbody.velocity = new Vector3(-MoveSpeed, myrigidbody.velocity.y, 0f);
        }
        else
        {
            myrigidbody.velocity = new Vector3(0f, myrigidbody.velocity.y, 0f);
        }
    }

    void Player_Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            AudioSource.PlayClipAtPoint(jump_SF, Camera.main.transform.position, 1f);
            myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, jumpSpeed, 0f);
        }   
    }

    void flip()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x *= -1;

        transform.localScale = scaleFactor;
    }

    private void handleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(CanFireBall== true)
            {
                AudioScript.instance.PlayAudioClip(AudioScript.instance.FireballClip, 1);
                fire();
            }
        }
    }


    private void Projectile_Check()
    {
        if (!projectileSpawnPoint)
            Debug.LogError("Missing projectileSpawn");
        if (!projectilePrefab)
            Debug.LogError("Missing projectilePrefab");
        if (projectileForce == 0)
        {
            projectileForce = 7.0f;
            Debug.Log("projectileForce was not set.Defaulting to " + projectileForce);
        }
    }

    private void fire()
    {
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        if (isFacingLeft == true)
        {
            temp.speed = -projectileForce;
        }
        else
        {
            temp.speed = projectileForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Power_UP")
        {
            CanFireBall = true;
        }

        if (collision.gameObject.tag == "Enemy_Projectile")
        {
            Destroy(collision.gameObject);
            print(Time.time + " " + health);
            health -= 1;
            if (health <= 0)
            {
                UI.Coin = 0;
                Destroy(gameObject);
            }
            else
            {
                GameManager.instance.PlayerDeath();
                SceneManager.LoadScene("Level1");
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            print(Time.time + " " + health);
            health -= 1;
            myAnim.SetBool("Dead", true);
            if (health <= 0)
            {
                myAnim.SetBool("Dead", true);
                UI.Coin = 0;
                GameManager.instance.PlayerDeath();

            }
            else
            {

                GameManager.instance.PlayerRestart();
            }

        }

        if (collision.gameObject.tag == "KillBox")
        {
            print(Time.time + " " + health);
            health -= 1;
            if (health <= 0)
            {
                myAnim.SetTrigger("enemy_Collision");
                UI.Coin = 0;
                GameManager.instance.PlayerDeath();

            }
            else
            {
                myAnim.SetTrigger("enemy_Collision");
                GameManager.instance.PlayerRestart();
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Win = true;
            GameManager.instance.Player_Win();
        }
        
        if(collision.gameObject.tag == "Door")
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);


        }

    }


    public void ReachGoal()
    {
        
        if(isGrounded)
        {
            myAnim.SetFloat("Speed", 1);
            myAnim.SetBool("Ground", true);
            myrigidbody.velocity = new Vector2(MoveSpeed / 3, 0);
            
        }
        else if (!isGrounded)
        {
            myrigidbody.velocity = new Vector2(0, -MoveSpeed / 3);
        }
        
        
    }

}
