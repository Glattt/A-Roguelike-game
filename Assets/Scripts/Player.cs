using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;
    public int numOfHearts;

    public bool run;
    States nowstate = new States();


    public Image[] hearts;
    public Sprite fullHealth;
    public Sprite nofullHealth;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        run = true;
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                anim.speed = 1;
                state = States.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.speed = 1;
                state = States.down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                sprite.flipX = false;
                anim.speed = 1;
                state = States.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                sprite.flipX = true;
                anim.speed = 1;
                state = States.left;
            }
            else
            {
                //state = States.idle;
                anim.speed = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            run = false;
            nowstate = state;
            moveVelocity = moveInput.normalized * 0;
            if (state == States.down) 
            {
                anim.speed = 1;
                state = States.crossdown;
            }
            else if (state == States.up)
            {
                anim.speed = 1;
                state = States.crossup;
            }
            else if (state == States.left)
            {
                anim.speed = 1;
                state = States.crossleft;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            state = nowstate;
            run=true;
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHealth;
            }
            else {
                hearts[i].sprite = nofullHealth;
            }
        }
    }

    private States state
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (health <5 && other.CompareTag("Heal"))
        {
            ChangeHealth(1);
            Destroy(other.gameObject);
        }
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
    }
}

public enum States
{
    idle,
    down,
    up,
    left,
    crossdown,
    crossup,
    crossright,
    crossleft
}
