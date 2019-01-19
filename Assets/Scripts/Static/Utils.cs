using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float GetTransformEnd(this Transform transform)
    {
        float maxEnd = transform.position.z;
        MeshRenderer meshRenderer = transform.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            maxEnd = meshRenderer.bounds.max.z;
        }

        foreach (Transform childTransform in transform)
        {
            float childMaxEnd = childTransform.GetTransformEnd();

            if (maxEnd < childMaxEnd)
            {
                maxEnd = childMaxEnd;
            }
        }

        return maxEnd;
    }

}
