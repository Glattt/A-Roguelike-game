using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource audioSource => GetComponent<AudioSource>();
    // Start is called before the first frame update
    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false)
    {
        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, 1f);
        }
        else
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
