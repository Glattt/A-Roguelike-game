using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Over : MonoBehaviour
{
    public float delay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SwitchScene", delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }
}
