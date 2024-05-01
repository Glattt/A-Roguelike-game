using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Record_Btns : MonoBehaviour
{
    int died;
    private int selectMenu = 0;
    // Start is called before the first frame update
    void Start()
    {
        died = PlayerPrefs.GetInt("Died");
        if (died == 0)
        {
            getBtn(1).SetActive(true);
        }
        else
        {
            getBtn(1).SetActive(false);
        }
        getBtn(0).transform.localPosition = new Vector3(-112f, -375f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (died==0)
            {
                selectMenu += 1;
            }
            Position();

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (died == 0)
            {
                selectMenu -= 1;
            }
            Position();

        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetInt("Died", 1);
            if (selectMenu == 0)
            {
                PlayerPrefs.SetInt("NowRecord", 0);
                SceneManager.LoadScene(0);

            }
            else if (selectMenu == 1)
            {
                SceneManager.LoadScene(1);
                Debug.Log("каунт1");
                Debug.Log(PlayerPrefs.GetInt("NowRecord"));
            }
        }
    }

    public GameObject getBtn(int num)
    {
        GameObject btn = GameObject.Find("BackBtn");
        if (num == 0)
        {
            btn = GameObject.Find("BackBtn");
        }
        if (num == 1)
        {
            btn = GameObject.Find("ContinueBtn");
        }
        return btn;
    }

    public void Position()
    {
        if (selectMenu > 1)
        {
            selectMenu = 0;
        }
        if (selectMenu < 0)
        {
            selectMenu = 1;
        }

        if (died == 0)
        {
            if (selectMenu == 0)
            {
                clearPos();
                getBtn(0).transform.localPosition = new Vector3(-115f, -375f, 0);
            }
            else if (selectMenu == 1)
            {
                clearPos();
                getBtn(1).transform.localPosition = new Vector3(-115f, -445f, 0);
            }
        }
        else
        {
            getBtn(0).transform.localPosition = new Vector3(-112f, -375f, 0);
        }
    }

    public void clearPos()
    {
        getBtn(0).transform.localPosition = new Vector3(-270f, -375f, 0);
        getBtn(1).transform.localPosition = new Vector3(-270f, -445f, 0);
    }
}
