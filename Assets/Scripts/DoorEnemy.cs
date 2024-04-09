using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnemy : MonoBehaviour
{
    public double health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(double healthValue)
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
