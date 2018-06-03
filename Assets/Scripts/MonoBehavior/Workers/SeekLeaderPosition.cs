using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekLeaderPosition : MonoBehaviour, iHalt {
    public GameData gd;

    float seekTime = 1;
    float seekTimer = 0;

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
    }

    private void OnDisable()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if(gd.gameState == GameState.Gameplay)
            seekTimer += Time.deltaTime;
        float completedPortion = seekTimer / seekTime;
        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, 0, completedPortion);
        transform.position = newPos;
        if (completedPortion == 1)
        {
            enabled = false;
        }
	}
}
