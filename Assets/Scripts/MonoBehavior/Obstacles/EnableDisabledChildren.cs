using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileReturner))]
public class EnableDisabledChildren : MonoBehaviour
{
    [SerializeField]
    List<DisableableObstacleCollision> children;

    private void OnEnable()
    {
        foreach (DisableableObstacleCollision child in children)
            child.gameObject.SetActive(true);
    }
}
