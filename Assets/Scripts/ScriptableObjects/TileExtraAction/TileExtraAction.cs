using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "No Action", menuName = "AbstractFields/No Action Tile")]
public class TileExtraAction : ScriptableObject
{
    // Activation position infront of the player
    [SerializeField, Tooltip("Activation position infront of the player")]
    protected float relActivPos;

    public void Begin(TileMover caller)
    {
        caller.StartCoroutine(Action(caller));
    }

    protected virtual IEnumerator Action(TileMover caller)
    {
        yield return null;
    }
}
