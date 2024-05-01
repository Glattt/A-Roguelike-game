using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtExit : MonoBehaviour
{
    private int selectMenu = 0;
    public GameObject pauseMenuUI;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (selectMenu == 0)
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(0);

                }
                else if (selectMenu == 1)
                {
                    Time.timeScale = 1f;
                    isPaused = false;
                    pauseMenuUI.SetActive(false);
                }
            }
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
        }
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
