using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generate pattern segment by segment
/// </summary>
public class TileGeneration : MonoBehaviour
{
    public PatternsDatabase patternDB;//[]->difficulty, [][]->pattern
    public LanesDatabase lanes;
    public GameData gd;
    public TileConfig tc;
    public InteractablesDatabase interactDB;

    Transform lastSegTrans;
    Pattern currentPattern;
    int currentSegmentIndex;
    int currentPatternIndex = -1;

    private void Awake()
    {
        InitPattern();
        lastSegTrans = transform;
    }

    void InitPattern()
    {
        currentSegmentIndex = 0;
        if (!gd.tutorialActive && gd.difficulty == 0)
            gd.difficulty++;
        //get a random pattern
        currentPatternIndex++;
        if (currentPatternIndex == patternDB[gd.difficulty].Count)
        {
            currentPatternIndex = 0;
            gd.difficulty++;
        }
        currentPattern = patternDB[gd.difficulty][currentPatternIndex];
    }

    private void Update()
    {
        if (ObjectPooler.instance.segmentActiveCount < tc.activeTilesNum)
        {
            GetNextSegment();
        }
    }

    void GetNextSegment()
    {
        Segment currentSegment = currentPattern[currentSegmentIndex++];

        if (currentSegmentIndex == currentPattern.Count)
        {
            if (gd.difficulty == 0)
            {
                gd.difficulty++;
            }
            InitPattern();
        }
        ObjectPooler.instance.segmentActiveCount++;

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
                tile = ObjectPooler.instance.GetFromPool(interactDB.Tile);
                objpos.y = tile.transform.position.y;
                tile.transform.position = objpos;
            }

            if (i == 4)
            {
                lastSegTrans = tile.transform;
                tile.GetComponent<TileReturner>().inActiveSegment = true;
            }
        }
    }
}
