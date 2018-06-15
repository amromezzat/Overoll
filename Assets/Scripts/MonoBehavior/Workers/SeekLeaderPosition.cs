//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SeekLeaderPosition : IWorkerScript
//{

//    public LanesDatabase lanes;
//    public float groundLevel;
//    public float takeLeadDuration;
//    public Transform transform;

//    float seekTimer = 0;
//    Vector3 newPos;

//    float CalculateXDisFrom(float other)
//    {
//        return Mathf.Abs(other - transform.position.x);
//    }

//    public void OnStateEnter(Animator animator)
//    {
//        seekTimer = 0;
//        float closestLaneDis = 100;
//        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
//        {
//            if (CalculateXDisFrom(closestLaneDis) > CalculateXDisFrom(lanes.OnGridLanes[i].laneCenter))
//            {
//                closestLaneDis = lanes.OnGridLanes[i].laneCenter;
//            }
//        }
//        newPos = new Vector3(0, groundLevel, closestLaneDis);
//    }

//    public bool OnStateExecution(Transform transform, float deltaTime)
//    {
//        seekTimer += deltaTime;
//        float completedPortion = seekTimer / takeLeadDuration;
//        transform.position = Vector3.Lerp(transform.position, newPos, completedPortion);
//        if (completedPortion >= 1)
//        {
//            return false;
//        }
//        return true;
//    }

//    public void OnStateExit(Animator animator)
//    {
//        throw new System.NotImplementedException();
//    }
//}
