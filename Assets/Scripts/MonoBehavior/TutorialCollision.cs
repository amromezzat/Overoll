using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollision : MonoBehaviour
{

    public GameData gd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            WorkerFSM workerFSM = other.GetComponent<WorkerFSM>();
            if (workerFSM.currentState == WorkerState.Worker || workerFSM.currentState == WorkerState.SlaveMerger)
                return;
            switch (tag)
            {
                case "TutJump":
                    gd.TutorialState = TutorialState.Jump;
                    break;
                case "TutSlide":
                    gd.TutorialState = TutorialState.Slide;
                    break;
                case "TutStrafeLeft":
                    gd.TutorialState = TutorialState.LeftStrafe;
                    break;
                case "TutStrafeRight":
                    gd.TutorialState = TutorialState.RightStrafe;
                    break;
                case "TutAddWorker":
                    gd.TutorialState = TutorialState.AddWorker;
                    break;
                case "TutMerge":
                    gd.TutorialState = TutorialState.MergeWorker;
                    break;
                case "TutCollide":
                    gd.TutorialState = TutorialState.Collide;
                    break;
                case "TutEnd":
                    gd.TutorialState = TutorialState.End;
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}