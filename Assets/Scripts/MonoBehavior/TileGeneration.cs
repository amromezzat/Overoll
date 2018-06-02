using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generate pattern segment by segment
/// </summary>
public class TileGeneration : MonoBehaviour
{
    public PatternDatabase patternDB;//[]->difficulty, [][]->pattern
    public LanesDatabase lanes;
    public GameData gd;
    public TileConfig tc;
    public PoolableType tileType;

    Transform lastSegTrans;
    Pattern currentPattern;
    int currentSegmentIndex;

    private void Awake()
    {
        tc.produceNextSegment.AddListener(GetNextSegment);
        InitPattern();
        lastSegTrans = transform;
    }

    void InitPattern()
    {
        currentSegmentIndex = 0;
        //get a random pattern
        currentPattern = patternDB[gd.difficulty][Random.Range(0, patternDB[gd.difficulty].Count)];
    }

    void GetNextSegment()
    {

        Segment currentSegment = currentPattern[currentSegmentIndex++];

        if (currentSegmentIndex == currentPattern.Count)
        {
            InitPattern();
        }

        //generate on available lanes
        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
        {
            GameObject tile = ObjectPooler.instance.GetFromPool(currentSegment[i]);

            Vector3 objpos = tile.transform.position;
            objpos.x = lanes[i].laneCenter;
            objpos.z = lastSegTrans.position.z + 1;
            tile.transform.position = objpos;

            if (!currentSegment[i].containTiles)
            {
                tile = ObjectPooler.instance.GetFromPool(tileType);
                objpos.y = tile.transform.position.y;
                tile.transform.position = objpos;
            }

            if (i == 4)
                lastSegTrans = tile.transform;
        }
    }
}
