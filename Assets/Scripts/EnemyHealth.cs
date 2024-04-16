using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public double health;
    public bool isHeart;
    private float timer = 0f;

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
            isHeart = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
