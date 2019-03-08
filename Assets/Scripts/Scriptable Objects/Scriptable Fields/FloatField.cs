using UnityEngine;

[CreateAssetMenu(fileName = "float", menuName = "AbstractFields/Float", order = 1)]
public class FloatField : AbstractField<float>
{
    public float defaultValue { get { return base.InitialValue; } }
}
