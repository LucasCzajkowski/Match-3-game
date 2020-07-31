using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds Instance;
    public AudioSource soundSwitch;
    public AudioSource soundExplode;
    public AudioSource music;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        music.Play();
    }

    public void PlaySoundSwitch()
    {
        soundSwitch.Play();
    }
    public void PlaySoundExplode()
    {
        soundExplode.Play();
    }


}
