using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollision : MonoBehaviour {

    public GameData gd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
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
            }
        }
    }
}