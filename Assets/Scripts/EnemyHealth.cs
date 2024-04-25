using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Sounds
{
    public double health;
    public bool isHeart;
    private Transform player;
    private AddRoom room;

    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<AddRoom>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        PlaySound(sounds[0],0.2f);
        health += healthValue;
        if (health > 0)
        {
            isHeart = true;
        }
        else
        {
            try
            {
                player.GetComponent<Player>().PlayCount(40);
            }
            catch { Debug.LogError("No Player"); }
            PlaySound(sounds[1], destroyed: true);
            room.enemies.Remove(this.gameObject);
            Destroy(this.gameObject);

        }
    }
}
