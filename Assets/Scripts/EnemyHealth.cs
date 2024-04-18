using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public double health;
    public bool isHeart;
    private AddRoom room;

    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<AddRoom>();
        if (room == null)
        {
            Debug.LogError("AddRoom component not found on the object.");
        }
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
            room.enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
