using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMusic : MonoBehaviour
{

    public AudioClip clip;

    private AudioSource radio;
    
    void Start()
    {
        radio = GetComponent<AudioSource>();

        radio.clip = clip;
        radio.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (!radio.isPlaying)
        {
            radio.clip = clip;
            radio.Play();
        }
    }
}
