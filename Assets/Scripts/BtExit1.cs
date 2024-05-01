using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtExit1 : MonoBehaviour
{
    private int selectMenu = 0;
    public GameObject pauseMenuUI;
    public bool isPaused=false;
    double timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    bool nu = false;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isPaused)
        {
            pauseMenuUI.SetActive(true);
            if (!nu)
            {
                Zero();
            }
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
            if (Input.GetKeyUp(KeyCode.Return) && timer>1f)
            {
                if (selectMenu == 0)
                {
                    Debug.Log("ex");
                    Application.Quit();
                }
                else if (selectMenu == 1)
                {
                    //pauseMenuUI.SetActive(false);
                    isPaused = false;
                    nu = false;
                }
            }
        }
        else
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void Zero()
    {
        timer = 0;
        nu = true;
    }
    public GameObject getBtn(int num)
    {
        GameObject btn = GameObject.Find("yes");
        if (num == 0)
        {
            btn = GameObject.Find("yes");
        }
        if (num == 1)
        {
            btn = GameObject.Find("no");
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

            if (selectMenu == 0)
            {
                clearPos();
                getBtn(0).transform.localPosition = new Vector3(0f, -114f, -1f);
            }
            else if (selectMenu == 1)
            {
                clearPos();
                getBtn(1).transform.localPosition = new Vector3(0f, -336f, -1f);
            }

    }

    public void clearPos()
    {
        getBtn(0).transform.localPosition = new Vector3(-200f, -114f, -1f);
        getBtn(1).transform.localPosition = new Vector3(-200f, -336f, -1f);
    }

}
