using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody2D rb;
    private Vector2 moveDirection = new Vector2(1,0);
    private float timer;
    private Transform player;
    //public double health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Untagged"))
        {
            FlipDirection();
        }*/
        moveDirection = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    public void FlipDirection()
    {
        //transform.localScale = new Vector3(moveDirection.x, 1f, 1f);
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

    /*public void ChangeHealth(double healthValue)
    {
        health += healthValue;
        if (health > 0)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
}