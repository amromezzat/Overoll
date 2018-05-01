using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSperatorTest : MonoBehaviour
{
    public WorkerConfig wc;
    public GameObject LaneSperatorObj1;
    public GameObject LaneSperatorObj2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 LaneSepPos = LaneSperatorObj1.transform.position;
        LaneSepPos.x = wc.leader.transform.position.x + 6;
        LaneSperatorObj1.transform.position = LaneSepPos;
        LaneSepPos = LaneSperatorObj2.transform.position;
        LaneSepPos.x = wc.leader.transform.position.x - 6;
        LaneSperatorObj2.transform.position = LaneSepPos;

    }
}
