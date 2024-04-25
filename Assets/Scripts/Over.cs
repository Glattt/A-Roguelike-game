using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class Over : MonoBehaviour
{
    public float delay = 3f;

    public AudioClip musicClip;
    private AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.Play();
        Invoke("SwitchScene", delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchScene()
    {
        SceneManager.LoadScene(2);
    }
}
