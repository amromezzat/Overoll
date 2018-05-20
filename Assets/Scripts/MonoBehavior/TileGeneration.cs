using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// generate tiles for each segment.
/// </summary>
public class TileGeneration : MonoBehaviour {


    public  PatternDatabase patternDB;
    public LanesDatabase lanes;
    public Pattern currentPattern;
    public GameStats gamestate;
    public int diff;
    public Pattern currentTile;
    public int indexInCurrentPattern;



    //gamestates->diffuculty


    void Start()
    {
      
        //-----------------------------------------
        diff = gamestate.difficulty;
        indexInCurrentPattern = 0;
        currentPattern = patternDB.PatternDBList[diff][Random.Range(0, 10)];
 
        // Check If the pattern had been displayed
     

    }


    bool IfPatternWasDisplayed()
    {
        if(indexInCurrentPattern== currentPattern.Count)
        {

        }
        return true;

    }

    void GetNextSegment()
    {
        Segment segmentena = currentPattern[indexInCurrentPattern++];
        // i represent thelaneNumber
        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
        {

            if (!segmentena[i])
            {
                continue;

            }

            var obj = ObjectPool.instance.GetFromPool(segmentena[i]);

            Vector3 objpos = obj.transform.position;
            objpos.x = lanes[i].laneCenter;
            obj.transform.position = objpos;
            Debug.Log("coin generated");
        }


    }
}
