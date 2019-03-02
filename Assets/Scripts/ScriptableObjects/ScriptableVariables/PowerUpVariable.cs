using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Types/PowerUp")]
public class PowerUpVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public bool InAct;
    public float DefaultTime;
    public float Time;

    [HideInInspector]
    public UnityEvent BeginAction;
    [HideInInspector]
    public UnityEvent EndAction;

    public void StartPowerUP()
    {
        InAct = true;
        Time = DefaultTime;

        if (Time > 0)
        {
            BeginAction.Invoke();
            PowerUpManager.Instance.StartCoroutine(DecreaseTime());
        }
    }

    public void ResetPowerUp()
    {
        EndAction.Invoke();
        InAct = false;
    }

    IEnumerator DecreaseTime()
    {
        while (Time > 0)
        {
            yield return new WaitForSeconds(1);
            Time--;
        }
        EndAction.Invoke();
        InAct = false;
    }

    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        InAct = false;
        Time = 0;
    }
}
