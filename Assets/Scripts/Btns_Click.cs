using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Btns_Click : MonoBehaviour
{
    private int selectMenu =0; 
    public int nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        getBtn(0).transform.localPosition = new Vector3(200f, -25, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            selectMenu += 1;
            Position();

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            selectMenu -= 1;
            Position();

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (selectMenu == 0)
            {
                SceneManager.LoadScene(nextLevel);

            }
            else if(selectMenu == 1)
            {
                SceneManager.LoadScene(2);
            }
            else if(selectMenu == 2)
            {
                Application.Quit();
            }
        }
    }

    public GameObject getBtn(int num)
    {
        GameObject btn = GameObject.Find("btPlay"); ;
        if (num == 0)
        {
             btn = GameObject.Find("btPlay");
        }
        if (num == 1)
        {
             btn = GameObject.Find("btRec");
        }
        if (num == 2)
        {
             btn = GameObject.Find("btEx");
        }
        return btn;
    }

    public void Position()
    {

        if (selectMenu > 2)
        {
            selectMenu = 0;
        }
        if (selectMenu < 0)
        {
            selectMenu = 2;
        }
        
        if(selectMenu == 0) 
        {
            clearPos();
            getBtn(0).transform.localPosition = new Vector3(200f, -25f, 0);
        }
        else if (selectMenu == 1)
        {
            clearPos();
            getBtn(1).transform.localPosition = new Vector3(200f, -145f, 0);
        }
        else if (selectMenu == 2)
        {
            clearPos();
            getBtn(2).transform.localPosition = new Vector3(200f, -265f, 0);
        }
    }
    
    public void clearPos()
    {
        getBtn(0).transform.localPosition = new Vector3(-20f, -25f, 0);
        getBtn(1).transform.localPosition = new Vector3(-20f, -145f, 0);
        getBtn(2).transform.localPosition = new Vector3(-20f, -265f, 0);

    }
}
