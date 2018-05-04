using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Player_SO playerManager;

    public void Awake()
    {
    
        playerManager.isalive = true;
    }  
}
