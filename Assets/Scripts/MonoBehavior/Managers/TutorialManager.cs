using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorialManager : MonoBehaviour, IChangeSpeed
{
    public static TutorialManager Instance;

    [SerializeField]
    private TutorialState tutorialState = TutorialState.Null;

    public bool tutorialActive = true;

    //public GameData gd;
    public WorkerConfig wc;

    public GameObject pauseBtn;

    [HideInInspector]
    public UnityEvent onSpeedUp = new UnityEvent();
    [HideInInspector]
    public UnityEvent onSlowDown = new UnityEvent();

    public float slowingRatio = 0.1f;
    public float slowingRate = 0.5f;

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

    public TutorialState TutorialState
    {
        get
        {
            return tutorialState;
        }

        set
        {
            tutorialState = value;

            if (tutorialActive && value != TutorialState.Null)
            {
                onSlowDown.Invoke();
            }
        }
    }

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
        //onSlowDown.AddListener(SlowDown);
        //onSpeedUp.AddListener(SpeedUp);
        slowingCoroutine = KillSpeed();
    }

    private void Start()
    {
        GameManager.Instance.OnStart.AddListener(TutStart);
    }

    public void SlowDown()
    {
        StartCoroutine(slowingCoroutine);
        //gd.Speed *= gd.slowingRate;
        SpeedManager.Instance.speed.Value *= slowingRate;
        switch (TutorialState)
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
        onSpeedUp.Invoke();
    }

    IEnumerator CollideTut()
    {
        MergeText.SetActive(true);
        yield return new WaitForSeconds(1);
        onSpeedUp.Invoke();
        yield return new WaitForSeconds(1);
        MergeText.SetActive(false);
        CollideText.SetActive(true);
        yield return new WaitForSeconds(2);
        CollideText.SetActive(false);
    }

    void TutStart()
    {
        //if (gd.tutorialActive && gd.difficulty == 0)
        if (tutorialActive && GameManager.Instance.difficulty.Value == 0)
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

        switch (TutorialState)
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
        TutorialState = TutorialState.Null;
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1);
        onSpeedUp.Invoke();
        ScoreText.SetActive(true);
        tutorialActive = false;
        yield return new WaitForSeconds(1);
        EndText.SetActive(false);
        //gd.Speed = gd.defaultSpeed;
        SpeedManager.Instance.speed.SetValueToInitial();
        //SpeedManager.Instance.SetSpeedValue(SpeedManager.Instance.speed.defaultValue);
        onSlowDown.RemoveListener(SlowDown);
        onSpeedUp.RemoveListener(SpeedUp);
        GameManager.Instance.OnStart.RemoveListener(TutStart);
        tutorialActive = false;
        pauseBtn.SetActive(true);
        gameObject.SetActive(false);
    }

    public IEnumerator KillSpeed()
    {
        while (true)
        {
            //faster by 0.1 than the other listeners
            //to set velocity before they get it
            yield return new WaitForSeconds(slowingRate - earlierListenTime);

            //gd.Speed = gd.Speed * gd.slowingRatio;
            SpeedManager.Instance.speed.Value = SpeedManager.Instance.speed.Value * slowingRatio;

            yield return new WaitForSeconds(earlierListenTime);
        }
    }
}
