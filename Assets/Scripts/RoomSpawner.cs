using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    // Start is called before the first frame update
    void Start()
    { 
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("r");
        if (!spawned && allObjects.Length<20)
        {
            if (direction == Direction.Top)
            {
                rand = Random.Range(0,variants.topRooms.Length);
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Bottom)
            {
                rand = Random.Range(0, variants.bottomRooms.Length);
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.leftRooms.Length);
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.rightRooms.Length);
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
            }
            spawned = true;
            //Debug.Log(allObjects.Length);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector2 roomPointPosition = other.transform.position;
        Vector2 targetPosition = new Vector2(0f, 0f);
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned || other.CompareTag("r")|| roomPointPosition == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    public enum Direction
    {
        Top, Bottom, Left, Right, None
    }

}
