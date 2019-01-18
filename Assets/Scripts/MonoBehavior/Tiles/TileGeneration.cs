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

    const float difficultyRunTime = 120;
    const int availableDifficulties = 4;

    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(difficultyRunTime);
        //if (gd.difficulty > 0)
        //{
        //    gd.difficulty = (gd.difficulty + 1) % availableDifficulties + 1;
        //    gd.difficulty = gd.difficulty == 0 ? 1 : gd.difficulty;
        //}
        if (GameManager.Instance.difficulty.Value > 0)
        {
            GameManager.Instance.difficulty.Value = (GameManager.Instance.difficulty.Value + 1) % availableDifficulties + 1;
            //check the line below again 
            GameManager.Instance.difficulty.Value = GameManager.Instance.difficulty.Value == 0 ? 1 : GameManager.Instance.difficulty.Value;
        }
    }

    private void Start()
    {
        InitPattern();
        lastSegTrans = transform;
        StartCoroutine(IncreaseDifficulty());
    }

    void InitPattern()
    {
        currentSegmentIndex = 0;
        //if (!gd.tutorialActive && gd.difficulty == 0)
        //    gd.difficulty++;
        if (!gd.tutorialActive && GameManager.Instance.difficulty.Value ==0)
            GameManager.Instance.difficulty.Value ++;
        //get a random pattern
        currentPatternIndex++;
        //if (currentPatternIndex == patternDB[gd.difficulty].Count)
        //{
        //    currentPatternIndex = 0;
        //    gd.difficulty++;
        //}
        if (currentPatternIndex == patternDB[GameManager.Instance.difficulty.Value].Count)
        {
            currentPatternIndex = 0;
            GameManager.Instance.difficulty.Value++;
        }
        //currentPattern = patternDB[gd.difficulty][currentPatternIndex];
        currentPattern = patternDB[GameManager.Instance.difficulty.Value][currentPatternIndex];
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
            //if (gd.difficulty == 0)
            //{
            //    gd.difficulty++;
            //}
            if (GameManager.Instance.difficulty.Value == 0)
            {
                GameManager.Instance.difficulty.Value++;
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
