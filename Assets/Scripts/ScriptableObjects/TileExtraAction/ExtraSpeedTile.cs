using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Extra Speed", menuName = "AbstractFields/Extra Speed Tile")]
public class ExtraSpeedTile : TileExtraAction
{
    [SerializeField]
    float ExtraVelocity;

    /// <summary>
    /// Take extra speed when reaching workers;
    /// object stays static relative to other objects
    /// until it is close enough to workers
    /// </summary>
    protected override IEnumerator Action(TileMover caller)
    {
        float waitingTime = (caller.transform.position.z - relActivPos) / SpeedManager.Instance.speed.Value;
        yield return new WaitForSeconds(waitingTime);
        caller.Anim.SetTrigger("Rotate Spool");
        caller.rb.velocity += Vector3.back * ExtraVelocity;
        caller.Anim.speed = 1;
    }
}
