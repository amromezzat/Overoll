using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// generate tiles for each segment.
/// </summary>
public class TileGeneration : MonoBehaviour {

    public PatternDatabase PatternDatabase;
    public LanesDatabase lanes;
 
    //gamestates->diffuculty

	
	void Start () {
        var firstsegment = PatternDatabase.PatternDBList[0][0].segmentList[0];
        TileGenerator(firstsegment);

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

           var obj = ObjectPool.instance.GetFromPool(segment[i]);
            
            Vector3 objpos = obj.transform.position;
            objpos.x = lanes[i].laneCenter;
            obj.transform.position = objpos;
            Debug.Log("coin generated");
        }
        

    }


  //  void RetunTileToPool()
    //{

    //}
}
