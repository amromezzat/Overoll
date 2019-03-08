using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectReturner))]
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    PowerUpVariable powerUpData;

    ObjectReturner objectReturner;

    private void Awake()
    {
        objectReturner = GetComponent<ObjectReturner>();
    }

    private void OnEnable()
    {
        StartCoroutine(objectReturner.ReturnToPool(powerUpData.Time));
    }
}
