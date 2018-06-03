using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour,iHalt {

    public GameData gd;
    public TileConfig tc;
    public GameObject startView;
    public GameObject backView;
    public Transform beginTrans;
    public Transform midTrans;
    public Transform EndTrans;
    public float transTime = 0.5f;

    Transform current;
    Transform next;
    float timer = 0;

    private void Awake()
    {
        RegisterListeners();
    }

    public void Begin()
    {
        next = midTrans;
        timer = 0;
        StartCoroutine(SetGameplayView());
    }

    IEnumerator SetGameplayView()
    {
        yield return new WaitForSeconds(transTime);
        timer = 0;
        current = midTrans;
        next = EndTrans;
        backView.SetActive(false);
        yield return new WaitForSeconds(transTime);
        startView.GetComponent<Rigidbody>().velocity = Vector3.back * tc.tileSpeed;
        yield return new WaitForSeconds(transTime);
        Destroy(startView);
        enabled = false;
    }

    public void End()
    {

    }

    public void Halt()
    {
        
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
        
    }

    // Use this for initialization
    void Start () {
        current = beginTrans;
        next = beginTrans;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        float completedPortion = timer / transTime;
        transform.position = Vector3.Lerp(current.position, next.position, completedPortion);
        transform.rotation = Quaternion.Lerp(current.rotation, next.rotation, completedPortion);
    }
}
