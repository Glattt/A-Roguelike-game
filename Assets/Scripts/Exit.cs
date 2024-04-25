using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private Animator anim;
    public SpriteRenderer[] runes;
    public bool[] runesLighted;
    public bool[] runesCollected;
    public Sprite[] runesLight;
    public GameObject[] objRunes;
    public int runesCount;

    public double health;
    public bool isHeart;

    private float timer1 = 0f;
    List<int> usedInd = new List<int>();
    GameObject[] spawnPoints;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        int r;
        r = Random.Range(0, 3);
        List<int> usedIndexes = new List<int>();
        for (int i = 0; i < r; i++)
        {
            int randRune;
            do
            {
                randRune = Random.Range(0, 4);
            } while (usedIndexes.Contains(randRune));
            usedIndexes.Add(randRune);

            //runes[randRune].sprite = runesLight[randRune];
            runesLighted[randRune] = true;
            runesCollected[randRune] = true;
            runesCount++;

        }

        LightUp();
        Invoke("SpawnRune", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (runesCount == 5)
        {
            if (health > 0)
            {
                if (!isHeart)
                {
                    state = PentStates.PentIdle;
                }
                else
                {
                    state = PentStates.PentHurt;
                    timer1 += Time.deltaTime;
                    if (timer1 >= 0.20f)
                    {
                        isHeart = false;
                    }
                }
            }
            else
            {
                player.GetComponent<Player>().PlayCount(300);
                player.GetComponent<Player>().SaveResult(false);
                SceneManager.LoadScene(2);
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

    public void LightUp()
    {
        for (int i = 0; i < 5; i++)
        {
            if (runesCollected[i])
            {
                runes[i].sprite = runesLight[i];
            }
        }
    }

    int randSpw;

    public void SpawnRune()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("runeSpawn");
        for (int i = 0; i < 5-runesCount; i++)
        {
            do
            {
                randSpw = Random.Range(0, spawnPoints.Length);
            } while (usedInd.Contains(randSpw));
            usedInd.Add(randSpw);
            for (int i1 = 0; i1 < runesLighted.Length; i1++)
            {
                if (!runesLighted[i1])
                {
                    //Debug.Log(objRunes[i1]);
                    //Debug.Log(spawnPoints[randSpw]);
                    GameObject newRune = Instantiate(objRunes[i1], spawnPoints[randSpw].transform.position, spawnPoints[randSpw].transform.rotation);
                    runesLighted[i1] = true;
                    ActiveRune activeScript = newRune.GetComponent<ActiveRune>();
                    if (activeScript != null)
                    {
                        activeScript.num = i1;
                    }
                    break;
                }
            }
        }
    }

    private PentStates state
    {
        get { return (PentStates)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
}

public enum PentStates
{
    PentIdle,
    PentHurt
}

