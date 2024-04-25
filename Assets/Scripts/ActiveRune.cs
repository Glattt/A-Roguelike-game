using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRune : MonoBehaviour
{
    public Sprite newSprite;
    public Exit exit;
    public int num;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        //exit = GetComponent<Exit>();
        exit = exit = GameObject.FindGameObjectWithTag("Pent").GetComponent<Exit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Collided with the player!");
                GetComponent<SpriteRenderer>().sprite = newSprite;
                exit.runesCollected[num] = true;
                exit.runesCount++;
                isActive = true;
                exit.LightUp();
            }
        }
    }
}
