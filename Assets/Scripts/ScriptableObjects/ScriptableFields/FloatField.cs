using UnityEngine;

[CreateAssetMenu(fileName = "float", menuName = "AbstractFields/Float", order = 1)]
public class FloatField : AbstractField<float>
{
    public float defaultValue { get { return base.Value; } }
    public float oldValue;

    public override float Value
    {
        get
        {
            return base.Value;
        }

        set
        {
            if(HasValueChanged(value))
            oldValue = runtimeValue;
            runtimeValue = value;
            ValueHasChanged(value);
        }
    }
}
