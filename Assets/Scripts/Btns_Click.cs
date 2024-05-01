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

    public AudioClip musicBtn;
    private AudioSource musicSource;
    private BtExit1 Ex;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        Ex = this.GetComponent<BtExit1>();
        getBtn(0).transform.localPosition = new Vector3(200f, -25, 0);
        PlayerPrefs.SetInt("NowRecord", 0);
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicBtn;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Ex.isPaused)
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
            if (Input.GetKeyUp(KeyCode.Return))
            {
                musicSource.Play();
                if (selectMenu == 0)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                else if (selectMenu == 1)
                {
                    SceneManager.LoadScene(2);
                }
                else if (selectMenu == 2 && pauseMenuUI.activeSelf == false)
                {
                    Ex.isPaused = true;
                }
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
        musicSource.Play();
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
