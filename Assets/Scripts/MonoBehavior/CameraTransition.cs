using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraTransition : MonoBehaviour, IHalt
{

    public GameData gd;
    public TileConfig tc;
    public GameObject startView;
    public GameObject backView;
    public GameObject ground;
    public Transform beginTrans;
    public Transform EndTrans;
    public float transTime = 2f;

    Transform current;
    Transform next;
    PlayableDirector playableDirector;
    float timer = 0;
    bool onHalt = true;
    Rigidbody startViewRB;

    private void Awake()
    {
        RegisterListeners();
        playableDirector = startView.GetComponent<PlayableDirector>();
        startViewRB = startView.GetComponent<Rigidbody>();
    }

    public void Begin()
    {
        timer = 0;
        onHalt = false;
        next = EndTrans;
        playableDirector.enabled = false;
        startViewRB.velocity = Vector3.back * gd.Speed;
        ground.SetActive(false);
    }

    public void End()
    {

    }

    public void Halt()
    {
        onHalt = true;
        startViewRB.velocity = Vector3.zero;
    }

        public void RegisterListeners()
    {
        gd.OnStart.AddListener(Begin);
        gd.onPause.AddListener(Halt);
        gd.OnResume.AddListener(Resume);
        gd.onEnd.AddListener(End);
    }

    public void Resume()
    {
        onHalt = false;
        startViewRB.velocity = Vector3.back * gd.Speed;
    }

    // Use this for initialization
    void Start()
    {
        current = beginTrans;
        next = beginTrans;
    }

    // Update is called once per frame
    void Update()
    {
        if (onHalt)
        {
            return;
        }
        timer += Time.deltaTime;
        float completedPortion = timer / transTime;
        float sinPortion = Mathf.Sin(completedPortion * Mathf.PI / (transTime * 2));
        transform.position = Vector3.Lerp(current.position, next.position, sinPortion);
        transform.rotation = Quaternion.Lerp(current.rotation, next.rotation, sinPortion);
        if (completedPortion >= transTime)
        {
            Destroy(startView);
            enabled = false;
        }
    }

    private void OnDisable()
    {
        gd.OnStart.RemoveListener(Begin);
        gd.onPause.RemoveListener(Halt);
        gd.OnResume.RemoveListener(Resume);
        gd.onEnd.RemoveListener(End);
    }
}
