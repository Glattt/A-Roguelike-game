using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody2D rb;
    private Vector2 moveDirection = new Vector2(1,0);
    private float timer;
    private float timer1 = 0f;
    private Transform player;
    public EnemyHealth health;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        health = this.GetComponent<EnemyHealth>();
    }

    void Update()
    {
        rb.velocity = moveDirection * speed;
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            FlipDirection();
            timer = 0f;
        }

        if (!health.isHeart)
        {
            state = EneRandStates.EnemyRandRun;
        }
        else
        {
            state = EneRandStates.EnemyRandHurt;
            timer1 += Time.deltaTime;
            if (timer1 >= 0.20f)
            {
                health.isHeart = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    public void FlipDirection()
    {
        moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Player>().ChangeHealth(-1);
        }
        if (other.CompareTag("Untagged"))
        {
            moveDirection = new Vector2(-moveDirection.x, -moveDirection.y);
        }
        if (other.CompareTag("wall"))
        {
            moveDirection = new Vector2(-moveDirection.x, -moveDirection.y);
        }
    }

    private EneRandStates state
    {
        get { return (EneRandStates)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
}

public enum EneRandStates
{
    EnemyRandIdle,
    EnemyRandRun,
    EnemyRandHurt
}