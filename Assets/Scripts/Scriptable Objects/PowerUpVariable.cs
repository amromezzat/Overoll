using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Types/PowerUp")]
public class PowerUpVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public bool InAct;
    public float DefaultTime;
    public float Time;
    public float ScaledTime
    {
        get
        {
            return Time / DefaultTime;
        }
    }

    [HideInInspector]
    public bool paused = false;

    [HideInInspector]
    public UnityEvent BeginAction;
    [HideInInspector]
    public UnityEvent EndAction;

    [ContextMenu("Start Power-up")]
    public void StartPowerUp()
    {
        InAct = true;
        Time = DefaultTime;

        if (Time > 0)
        {
            BeginAction.Invoke();
            PowerUpManager.Instance.StartCoroutine(DecreaseTime());
        }
    }

    [ContextMenu("End Power-up")]
    public void ResetPowerUp()
    {
        EndAction.Invoke();
        InAct = false;
    }

    IEnumerator DecreaseTime()
    {
        while (Time > 0)
        {
            yield return new WaitWhile(() => paused);
            yield return new WaitForSeconds(0.25f);
            Time = Time - 0.25f;
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
