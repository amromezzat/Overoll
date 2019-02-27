using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Press
{
    None,
    Upper,
    Lower,
    Duel
}

[CreateAssetMenu(fileName = "Press", menuName = "AbstractFields/Press Tile")]
public class PressTile : TileExtraAction
{
    float lastCallTime;
    TileMover firstCaller;
    Press press;

    private void OnEnable()
    {
        lastCallTime = Time.realtimeSinceStartup;
    }

    protected override IEnumerator Action(TileMover caller)
    {
        // Link callers in a certain time window
        float passedTime = Time.realtimeSinceStartup - lastCallTime;
        if(passedTime > 1)
        {
            firstCaller = caller;
            lastCallTime = Time.realtimeSinceStartup;
        }

        // Wait for the object to be close to the player
        float waitingTime = (caller.transform.position.z - relActivPos) / SpeedManager.Instance.speed.Value;
        yield return new WaitForSeconds(waitingTime);

        // Set press value based on the order of the call
        if (firstCaller == caller)
            press = ExtensionMethods.RandomEnumValue<Press>();
        else
            SetSecondPress();

        caller.Anim.SetTrigger(press.ToString() + "Press");
    }

    void SetSecondPress()
    {
        switch (press)
        {
            case Press.None:
                press = Press.Duel;
                break;
            case Press.Upper:
                press = Press.Upper;
                break;
            case Press.Lower:
                press = Press.Lower;
                break;
            case Press.Duel:
                press = Press.None;
                break;
        }
    }
}
