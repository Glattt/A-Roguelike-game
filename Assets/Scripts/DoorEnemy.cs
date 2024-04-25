using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnemy : MonoBehaviour
{
    public double health;
    public bool isWall = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject block;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {
            Instantiate(block, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isWall = true;
            Debug.Log("wall");
        }
    }

    public void ChangeHealth(double healthValue)
    {
        if (!isWall)
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
        }
    }
}
