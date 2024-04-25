using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    private SpriteRenderer lines;
    GameObject linesObject;

    // Start is called before the first frame update
    void Start()
    {
        linesObject = GameObject.Find("spr_CH1_scanlines_0");
        cam = Camera.main.GetComponent<Camera>();
        //lines = GetComponent<>
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            Vector3 newPosition = other.transform.position + playerChangePos;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f);
            bool objectWithTagR = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("r"))
                {
                    objectWithTagR = true;
                    break;
                }
            }

            if (objectWithTagR)
            {
                other.transform.position = newPosition;
                cam.transform.position += cameraChangePos;
                linesObject.transform.position += cameraChangePos;
            }
            else
            {
                other.transform.position = new Vector3(0, 0, 1);
                cam.transform.position = new Vector2(0,0);
            }
        }
    }
}
