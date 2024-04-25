using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Transform player;
    private Animator anim;
    private float timer;
    private float timer1;
    private bool isHeart;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeart)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                player.GetComponent<Player>().PlayCount(-10);
                player.GetComponent<Player>().ChangeHealth(-1);
                timer = 0f;
            }


            timer1 += Time.deltaTime;
            if (timer1 < 0.10f)
            {
                state = StarStates.StarHurt;
            }
            else if (timer1 >= 0.10f && timer1 < 0.20f)
            {
                state = StarStates.StarIdle;
            }
            else
            {
                timer1 = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isHeart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHeart = false;
        state = StarStates.StarIdle;
    }

    private StarStates state
    {
        get { return (StarStates)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
}

public enum StarStates
{
    StarIdle,
    StarHurt
}
