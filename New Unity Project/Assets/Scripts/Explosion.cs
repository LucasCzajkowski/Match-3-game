using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    ParticleSystem ps;

    

    private void Awake()
    {
       ps =  gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        ps.Clear();
        ps.Play();
    }

    private void Update()
    {
        if(ps.isPlaying == false)
        {
            gameObject.SetActive(false);
        }
    }
}
