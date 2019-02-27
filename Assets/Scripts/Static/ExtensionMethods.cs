using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum = System.Enum;
using Random = System.Random;

public static class ExtensionMethods
{
    public static T RandomEnumValue<T>() where T : struct, System.IConvertible
    {
        System.Array values = Enum.GetValues(typeof(T));
        Random random = new Random();
        return (T)values.GetValue(random.Next(values.Length));
    }
}
