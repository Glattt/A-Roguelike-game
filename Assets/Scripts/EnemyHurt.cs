using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : Sounds
{
    public EnemyHealth health;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        health = this.GetComponentInParent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isHeart)
        {
            timer += Time.deltaTime;
            if (timer >= 0.20f)
            {
                PlaySound(sounds[0]);
                timer = 0f;
            }
        }
    }
}
