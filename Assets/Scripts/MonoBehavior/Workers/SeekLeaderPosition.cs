using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekLeaderPosition : MonoBehaviour, iHalt
{

    public GameData gd;
    public WorkerConfig wc;
    public LanesDatabase ld;

    float seekTimer = 0;
    Vector3 newPos;

    public void Begin()
    {
    }

    public void End()
    {
        enabled = false;
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
    private void OnEnable()
    {
        seekTimer = 0;
        float closestLaneDis = 100;
        for (int i = 0; i < ld.OnGridLanes.Count; i++)
        {
            if (CalculateXDisFrom(closestLaneDis) > CalculateXDisFrom(ld.OnGridLanes[i].laneCenter))
            {
                closestLaneDis = ld.OnGridLanes[i].laneCenter;
            }
        }
        newPos = new Vector3(0, wc.groundLevel, closestLaneDis);
    }

    private void OnDisable()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gd.gameState == GameState.Gameplay)
            seekTimer += Time.deltaTime;
        float completedPortion = seekTimer / wc.takeLeadDuration;
        transform.position = Vector3.Lerp(transform.position, newPos, completedPortion);
        if (completedPortion >= 1)
        {
            enabled = false;
        }
    }

    float CalculateXDisFrom(float other)
    {
        return Mathf.Abs(other - transform.position.x);
    }
}
