using UnityEngine;

[CreateAssetMenu (fileName ="Float",menuName ="Variable/Float",order =2)]
public class FloatVariable : ScriptableObject
{
    //[HideInInspector]
    public float value;

    public void SetValue(float val)
    {
        value = val;
    }
 
}
