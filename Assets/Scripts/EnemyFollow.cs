using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform player;
    [HideInInspector] public EnemyHealth health;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isMovingTowardsPlayer = true;
    public float resumeMovementDistance = 1f;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        health = this.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isHeart)
        {
            state = EnemStates.EnemyRun;
        }
        else
        {
            state = EnemStates.EnemyHurt;
            timer += Time.deltaTime;
            if (timer >= 0.20f)
            {
                health.isHeart = false;
            }
        }
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveChar(movement);
    }
    private void MoveChar(Vector2 direction)
    {
        if (isMovingTowardsPlayer)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
        if (Vector2.Distance(transform.position, player.position) > resumeMovementDistance)
        {
            isMovingTowardsPlayer = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMovingTowardsPlayer = false;
            player.GetComponent<Player>().ChangeHealth(-1);
        }
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
    EnemyRun,
    EnemyHurt
}
