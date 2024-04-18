using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public GameObject[] doors;

    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    public GameObject cross;

    private RoomVariants variants;
    private bool spawned;
    private bool wallsDestroyed;

    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("r").GetComponent<RoomVariants>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !spawned)
        {
            Debug.Log("спавн");
            spawned = true;
            if (enemySpawners.Length == 1)
            {
                int rand = Random.Range(0, 2);
                if (rand ==0)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, enemySpawners[0].position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }
                else if (rand == 1)
                {
                    Instantiate(cross, enemySpawners[0].position, Quaternion.identity);
                }
            }
            else
            {
                foreach (Transform spawner in enemySpawners)
                {
                    int rand = Random.Range(0, 11);
                    if (rand < 8)
                    {
                        GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                        GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                        enemy.transform.parent = transform;
                        enemies.Add(enemy);
                    }
                    else if (rand == 8)
                    {
                        Instantiate(cross, spawner.position, Quaternion.identity);
                    }
                }
            }
            StartCoroutine(CheckEnemies());
        }
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyDoors();
    }

    public void DestroyDoors()
    {
        foreach (GameObject door in doors)
        {
            if(door != null)
            {
                Destroy(door);
            }
        }
        wallsDestroyed = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (wallsDestroyed && collision.CompareTag("DoorEnemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
