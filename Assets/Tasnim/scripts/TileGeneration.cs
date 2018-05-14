using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour {

    public Lanes lanes;
    ObjectPool PoolOfObstacles;
 


	// Use this for initialization
	void Start () {
        PoolOfObstacles = GetComponent<ObjectPool>();
        
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}


     void TileGenerator(Segment segment)
    {
        // i represent thelaneNumber
        for (int i = 0; i < 5; i++)
        {
            if (!segment[i])
            {
                continue;

            }

            var obj = PoolOfObstacles.GetFromPool(segment[i]);
            Vector3 objpos = obj.transform.position;
             objpos.x = lanes[i].laneCenter;
        }
        

    }


  //  void RetunTileToPool()
    //{

    //}
}
