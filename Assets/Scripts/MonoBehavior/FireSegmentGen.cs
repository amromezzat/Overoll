using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSegmentGen : MonoBehaviour
{

    public TileConfig tc;
    Rigidbody rb;
    Vector3 parentPos;


    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        parentPos = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.z < parentPos.z - 1)
        {
            rb.transform.position = parentPos;
            tc.produceNextSegment.Invoke();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
