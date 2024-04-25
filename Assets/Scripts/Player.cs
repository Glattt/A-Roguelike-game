using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private Transform enemy;

    [SerializeField] float obstacleRayDistance;
    public GameObject obstacleRayObject;
    Vector2 characterDirection;
    public float coneAngle = 30f;
    public LayerMask enemyLayer;

    [SerializeField] private float delayTime;
    private float delay;

    public GameObject letter;
    private bool isLetterActive = false;

    public int count = 0;
    public Text nowCount;

    // Start is called before the first frame update
    void Start()
    {
        run = true;
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        try
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        }
        catch 
        {
            Debug.Log("No enemies");
        }
        characterDirection = Vector2.down;
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
                characterDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.speed = 1;
                state = States.down;
                characterDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                sprite.flipX = false;
                anim.speed = 1;
                state = States.left;
                characterDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                sprite.flipX = true;
                anim.speed = 1;
                state = States.left;
                characterDirection = Vector2.right;
            }
            else
            {
                anim.speed = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isLetterActive)
            {
                CloseLetter();
            }
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
        if (Input.GetKey(KeyCode.Space))
        {
            IsEnemyDetected();
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
        /*if (health <5 && other.CompareTag("Heal") && health>0 && other.CompareTag("Player"))
        {
            ChangeHealth(1);
            Destroy(other.gameObject);
        }*/
        if (other.CompareTag("lore"))
        {
            OpenLetter();
            Destroy(other.gameObject);
        }
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
        if (health > 0)
        {
            return;
        }
        else
        {
            PlayCount(-100);
            //SceneManager.LoadScene(3);

            SaveResult(true);
        }
    }

    public void IsEnemyDetected()
    {
        RaycastHit2D hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, new Vector2(characterDirection.x, characterDirection.y), obstacleRayDistance, enemyLayer);
        Vector2 left = Quaternion.Euler(0, 0, -coneAngle) * new Vector2(characterDirection.x, characterDirection.y);
        Vector2 right = Quaternion.Euler(0, 0, coneAngle) * new Vector2(characterDirection.x, characterDirection.y);
        RaycastHit2D hitObstacleleft = Physics2D.Raycast(obstacleRayObject.transform.position, left, obstacleRayDistance, enemyLayer);
        RaycastHit2D hitObstacleright = Physics2D.Raycast(obstacleRayObject.transform.position, right, obstacleRayDistance, enemyLayer);

        if (hitObstacle.collider != null || hitObstacleleft.collider != null || hitObstacleright.collider != null)
        {
            Debug.DrawRay(obstacleRayObject.transform.position, hitObstacle.distance * new Vector2(characterDirection.x, characterDirection.y), Color.red);
            Debug.DrawRay(obstacleRayObject.transform.position, hitObstacle.distance * left, Color.red);
            Debug.DrawRay(obstacleRayObject.transform.position, hitObstacle.distance * right, Color.red);
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            if (delay <= 0)
            {
                if (hitObstacle.collider != null && hitObstacle.collider.CompareTag("Enemy"))
                {
                    hitObstacle.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
                else if (hitObstacleleft.collider != null && hitObstacleleft.collider.CompareTag("Enemy"))
                {
                    hitObstacleleft.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
                else if (hitObstacleright.collider != null && hitObstacleright.collider.CompareTag("Enemy"))
                {
                    hitObstacleright.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }

                if (hitObstacle.collider != null && hitObstacle.collider.CompareTag("cursed"))
                {
                    hitObstacle.collider.GetComponent<CursedObj>().ChangeHealth(-1);
                }
                else if (hitObstacleleft.collider != null && hitObstacleleft.collider.CompareTag("cursed"))
                {
                    hitObstacleleft.collider.GetComponent<CursedObj>().ChangeHealth(-1);
                }
                else if (hitObstacleright.collider != null && hitObstacleright.collider.CompareTag("cursed"))
                {
                    hitObstacleright.collider.GetComponent<CursedObj>().ChangeHealth(-1);
                }

                if (hitObstacle.collider != null && hitObstacle.collider.CompareTag("Pent"))
                {
                    hitObstacle.collider.GetComponent<Exit>().ChangeHealth(-1);
                }
                else if (hitObstacleleft.collider != null && hitObstacleleft.collider.CompareTag("Pent"))
                {
                    hitObstacleleft.collider.GetComponent<Exit>().ChangeHealth(-1);
                }
                else if (hitObstacleright.collider != null && hitObstacleright.collider.CompareTag("Pent"))
                {
                    hitObstacleright.collider.GetComponent<Exit>().ChangeHealth(-1);
                }

                delay = delayTime;

            }
        }
        else
        {
            //Debug.Log("NO Enemy");
            Debug.DrawRay(obstacleRayObject.transform.position, obstacleRayDistance * new Vector2(characterDirection.x, characterDirection.y), Color.green);
            Debug.DrawRay(obstacleRayObject.transform.position, hitObstacleleft.distance * left, Color.green);
            Debug.DrawRay(obstacleRayObject.transform.position, hitObstacleright.distance * right, Color.green);
        }
    }

    public void OpenLetter()
    {
        letter.SetActive(true);
        isLetterActive = true;
        Time.timeScale = 0f;
    }

    public void CloseLetter()
    {
        Destroy(letter);
        isLetterActive = false;
        Time.timeScale = 1f;
    }

    public void PlayCount(int a)
    {
        count += a;
        nowCount.text = count.ToString();
        Debug.Log(count.ToString());
    }


    public void SaveResult(bool died)
    {
        // Get the list of record counts from PlayerPrefs
        List<int> records = new List<int>();
        if (PlayerPrefs.HasKey("Records"))
        {
            string recordsString = PlayerPrefs.GetString("Records");
            string[] recordArray = recordsString.Split(',');
            foreach (string record in recordArray)
            {
                records.Add(int.Parse(record));
            }
        }

        // Add the current count to the list of records
        records.Add(count);

        // Sort the records in descending order
        records = records.OrderByDescending(r => r).ToList();

        // Keep only the top 10 records
        if (records.Count > 10)
        {
            records = records.GetRange(0, 10);
        }
        // Save the updated list of records to PlayerPrefs
        string newRecordsString = string.Join(",", records);
        PlayerPrefs.SetString("Records", newRecordsString);

        Debug.Log(newRecordsString);

        // Load the main menu scene
        //SceneManager.LoadScene(2);
        if (died)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("r"))
        {
            transform.position = new Vector2(0, 0);
        }
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
