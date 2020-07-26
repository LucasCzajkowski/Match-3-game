using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    ParticleSystem ps;
    private void Start()
    {
       ps =  gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if(ps.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
