using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IChangeSpeed
{
    public GameData gd;
    public WorkerConfig wc;

    public GameObject pauseBtn;

    public GameObject addWorkerBtn;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    Animator addBtnAnimator;
    IEnumerator slowingCoroutine;

    const float earlierListenTime = 0.2f;

    private void Awake()
    {
        addBtnAnimator = addWorkerBtn.GetComponent<Animator>();
        gd.onSlowDown.AddListener(SlowDown);
        gd.onSpeedUp.AddListener(SpeedUp);
        gd.OnStart.AddListener(TutStart);
        slowingCoroutine = KillSpeed();
    }

    public void SlowDown()
    {
        StartCoroutine(slowingCoroutine);
        gd.Speed *= gd.slowingRate;
        switch (gd.TutorialState)
        {
            case TutorialState.Jump:
                upArrow.SetActive(true);
                break;
            case TutorialState.Slide:
                downArrow.SetActive(true);
                break;
            case TutorialState.LeftStrafe:
                leftArrow.SetActive(true);
                break;
            case TutorialState.RightStrafe:
                rightArrow.SetActive(true);
                break;
            case TutorialState.AddWorker:
                addWorkerBtn.SetActive(true);
                addBtnAnimator.SetBool("Play", true);
                break;
        }
    }

    void TutStart()
    {
        if (gd.tutorialActive && gd.difficulty == 0)
        {
            pauseBtn.SetActive(false);
            addWorkerBtn.SetActive(false);
            StartCoroutine(BecomeATutor());
        }
        else
        {
            StartCoroutine(EndTutorial());
        }
    }

    IEnumerator BecomeATutor()
    {
        yield return new WaitForSeconds(0.5f);
        wc.leader.ChangeState(WorkerStateTrigger.StartTutoring);
    }

    public void SpeedUp()
    {
        StopCoroutine(slowingCoroutine);
        gd.Speed = gd.defaultSpeed;
        switch (gd.TutorialState)
        {
            case TutorialState.Jump:
                upArrow.SetActive(false);
                break;
            case TutorialState.Slide:
                downArrow.SetActive(false);
                break;
            case TutorialState.LeftStrafe:
                leftArrow.SetActive(false);
                break;
            case TutorialState.RightStrafe:
                rightArrow.SetActive(false);
                StartCoroutine(EndTutorial());
                break;
            case TutorialState.AddWorker:
                StartCoroutine(EndTutorial());
                addBtnAnimator.SetBool("Play", false);
                break;
        }
        gd.TutorialState = TutorialState.Null;
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(wc.strafeDuration);
        gd.onSlowDown.RemoveListener(SlowDown);
        gd.onSpeedUp.RemoveListener(SpeedUp);
        gd.OnStart.RemoveListener(TutStart);
        pauseBtn.SetActive(true);
        addWorkerBtn.SetActive(true);
        wc.leader.ChangeState(WorkerStateTrigger.EndTutoring);
        enabled = false;
    }

    public IEnumerator KillSpeed()
    {
        while (true)
        {
            //faster by 0.1 than the other listeners
            //to set velocity before they get it
            yield return new WaitForSeconds(gd.slowingRate - earlierListenTime);
            gd.Speed = gd.Speed * gd.slowingRatio;
            yield return new WaitForSeconds(earlierListenTime);
        }
    }
}
