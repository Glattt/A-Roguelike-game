using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform player;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        state = EnemStates.EnemyRun;

    }

    private EnemStates state
    {
        get { return (EnemStates)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

}
public enum EnemStates
{
    EnemyIdle,
    EnemyRun
}
