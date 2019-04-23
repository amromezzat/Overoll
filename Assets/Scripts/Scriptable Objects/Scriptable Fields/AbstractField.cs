using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExt<T> : UnityEvent<T> { }

public abstract class AbstractField<T> : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    protected T InitialValue;
    public T OldValue { get; protected set; }
    public UnityEvent<T> onValueChanged = new UnityEventExt<T>();
    [SerializeField]
    protected T runtimeValue;
    public virtual T Value
    {
        get
        {
            return runtimeValue;
        }
        set
        {
            if(name == "Speed")
            {
                Debug.Log("lll");
            }
            if (HasValueChanged(value))
            {
                OldValue = runtimeValue;
                runtimeValue = value;
                ValueHasChanged(value);
            }
        }
    }

    protected void ValueHasChanged(T value)
    {
        onValueChanged.Invoke(value);
    }

    public static implicit operator T(AbstractField<T> abstractField)
    {
        return abstractField.Value;
    }

    /// <summary>
    /// Reference types implementing IEquatable doesn't use boxing/unboxing in equality comparison
    /// https://stackoverflow.com/a/488301
    /// </summary>
    /// <param name="newValue"></param>
    /// <returns></returns>
    protected virtual bool HasValueChanged(T newValue)
    {
        return !EqualityComparer<T>.Default.Equals(runtimeValue, newValue);
    }

    public virtual void OnBeforeSerialize()
    { }

    public virtual void OnAfterDeserialize()
    {
        runtimeValue = InitialValue;
    }

    public virtual T SetValueToInitial()
    {
        return runtimeValue = InitialValue;
    }

    public virtual T ReturnValueToOld()
    {
        return runtimeValue = OldValue;
    }
}
