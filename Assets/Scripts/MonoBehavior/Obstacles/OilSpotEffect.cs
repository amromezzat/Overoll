using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpotEffect : CollisionEffect
{
    [SerializeField]
    ParticleSystem particleEffect;

    public override void PlayEffect()
    {
        particleEffect.Play();
    }
}
