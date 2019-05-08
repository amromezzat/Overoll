using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpotEffect : CollisionEffect
{
    [SerializeField]
    ParticleSystem particleSystem;

    public override void PlayEffect()
    {
        particleSystem.Play();
    }
}
