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
    public GameState gs;
    public TileConfig tc;

    Pattern currentPattern;
    int currentSegmentIndex;

    private void OnEnable()
    {
        tc.produceNextSegment.AddListener(GetNextSegment);
    }

    void Start()
    {
        InitPattern();
        GetNextSegment();
    }

    void InitPattern()
    {
        currentSegmentIndex = 0;
        //get a random pattern
        currentPattern = patternDB[gs.difficulty][Random.Range(0, patternDB[gs.difficulty].Count)];
    }

    void GetNextSegment()
    {
        Segment currentSegment = currentPattern[currentSegmentIndex++];
        if(currentSegmentIndex == currentPattern.Count)
        {
            InitPattern();
        }

        //generate on available lanes
        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
        {
            if (!currentSegment[i])
            {
                continue;
            }
            GameObject tile = ObjectPool.instance.GetFromPool(currentSegment[i]);
            Vector3 objpos = tile.transform.position;
            objpos.x = lanes[i].laneCenter;
            tile.transform.position = objpos;
        }
    }
}
