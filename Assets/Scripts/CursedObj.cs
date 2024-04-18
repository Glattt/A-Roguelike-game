using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedObj : MonoBehaviour
{
    public double health;
    public bool isHeart;
    public bool isDead=false;

    private Animator anim;
    private float timer = 0f;
    private float timer1 = 0f;

    public GameObject cross;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health>0)
        {
            if (!isHeart)
            {
                state = CursedStates.CursedIdle;
            }
            else
            {
                state = CursedStates.CursedHurt;
                timer1 += Time.deltaTime;
                if (timer1 >= 0.20f)
                {
                    isHeart = false;
                }
            }
        }
        else
        {   
            if (!isDead)
            {
                state = CursedStates.CursedDead;
                timer += Time.deltaTime;
                if (timer >= 0.80f)
                {
                    isDead = true;
                    state = CursedStates.CursedIdle;
                    cross.SetActive(true);
                }
            }
            else
            {
                return;
            }

        }

    }

    public void ChangeHealth(double healthValue)
    {
        
        if (health > 0)
        {
            isHeart = true;
            health += healthValue;
        }
    }
    private CursedStates state
    {
        get { return (CursedStates)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
}

public enum CursedStates
{
    CursedIdle,
    CursedHurt,
    CursedDead
}
