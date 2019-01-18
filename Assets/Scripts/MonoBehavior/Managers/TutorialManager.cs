using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IChangeSpeed
{
    public static TutorialManager Instance;

    public GameData gd;
    public WorkerConfig wc;

    public GameObject pauseBtn;

    public GameObject addWorkerBtn;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject doubleTap;
    public GameObject buyWorkersText;
    public GameObject MergeText;
    public GameObject CollideText;
    public GameObject EndText;
    public GameObject ScoreText;
    public GameObject GoldText;

    Animator addBtnAnimator;
    IEnumerator slowingCoroutine;

    const float earlierListenTime = 0.2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
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
        //gd.Speed *= gd.slowingRate;
        SpeedManager.Instance.speed.Value *= gd.slowingRate;
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
                doubleTap.SetActive(true);
                addBtnAnimator.SetBool("Play", true);
                GoldText.SetActive(true);
                break;
            case TutorialState.MergeWorker:
                StartCoroutine(StartMergeTutorial());
                break;
            case TutorialState.Collide:
                StartCoroutine(CollideTut());
                break;
            case TutorialState.End:
                EndText.SetActive(true);
                StartCoroutine(EndTutorial());
                PlayerPrefs.SetInt("PlayedTutorial", 1);
                break;
        }
    }

    IEnumerator StartMergeTutorial()
    {
        buyWorkersText.SetActive(true);
        wc.leader.ChangeState(WorkerStateTrigger.EndTutoring);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 3; i++)
        {
            wc.onAddWorker.Invoke();
        }
        gd.onSpeedUp.Invoke();
    }

    IEnumerator CollideTut()
    {
        MergeText.SetActive(true);
        yield return new WaitForSeconds(1);
        gd.onSpeedUp.Invoke();
        yield return new WaitForSeconds(1);
        MergeText.SetActive(false);
        CollideText.SetActive(true);
        yield return new WaitForSeconds(2);
        CollideText.SetActive(false);
    }

    void TutStart()
    {
        //if (gd.tutorialActive && gd.difficulty == 0)
        if (gd.tutorialActive && GameManager.Instance.difficulty.Value == 0)
        {
            pauseBtn.SetActive(false);
            addWorkerBtn.SetActive(false);
            StartCoroutine(BecomeATutor());
            ScoreText.SetActive(false);
            GoldText.SetActive(false);
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
        //gd.Speed = gd.defaultSpeed;
        SpeedManager.Instance.speed.Value = SpeedManager.Instance.speed.defaultValue;

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
                addWorkerBtn.SetActive(true);
                rightArrow.SetActive(false);
                break;
            case TutorialState.AddWorker:
                doubleTap.SetActive(false);
                addBtnAnimator.SetBool("Play", false);
                break;
            case TutorialState.MergeWorker:
                buyWorkersText.SetActive(false);
                break;
        }
        gd.TutorialState = TutorialState.Null;
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1);
        gd.onSpeedUp.Invoke();
        ScoreText.SetActive(true);
        gd.tutorialActive = false;
        yield return new WaitForSeconds(1);
        EndText.SetActive(false);
        //gd.Speed = gd.defaultSpeed;
        SpeedManager.Instance.SetSpeedValue(SpeedManager.Instance.speed.defaultValue);
        gd.onSlowDown.RemoveListener(SlowDown);
        gd.onSpeedUp.RemoveListener(SpeedUp);
        gd.OnStart.RemoveListener(TutStart);
        gd.tutorialActive = false;
        pauseBtn.SetActive(true);
        gameObject.SetActive(false);
    }

    public IEnumerator KillSpeed()
    {
        while (true)
        {
            //faster by 0.1 than the other listeners
            //to set velocity before they get it
            yield return new WaitForSeconds(gd.slowingRate - earlierListenTime);

            //gd.Speed = gd.Speed * gd.slowingRatio;
            SpeedManager.Instance.SetSpeedValue(SpeedManager.Instance.speed.Value * gd.slowingRatio);

            yield return new WaitForSeconds(earlierListenTime);
        }
    }
}
