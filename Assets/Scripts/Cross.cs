using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : Sounds
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Player>().PlayCount(-20);
            player.GetComponent<Player>().ChangeHealth(1);
            PlaySound(sounds[0], destroyed: true, volume: 1f);
            Destroy(gameObject);
        }
    }
}
