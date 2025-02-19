using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : Singleton<Audio>
{
    private AudioSource audio;
    protected override void Awake()
    {
        base.Awake();
        audio = gameObject.GetComponent<AudioSource>();
    }

    public void Play()
    {
        audio.Play();
    }
}
