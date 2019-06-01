/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// generate pattern segment by segment
/// </summary>
public class TileGeneration : MonoBehaviour
{
    public Pattern tutorialPattern;
    public Pattern emptyPattern;
    public PatternsDatabase patternDB;//[]->difficulty, [][]->pattern
    // Active lanes in game
    public LanesDatabase lanes;
    // Tile data
    public TileConfig tc;
    // Database carrying available tiles
    public InteractablesDatabase interactDB;

    // Position of the upcoming segment
    Transform lastSegTrans;
    Pattern currentPattern;

    // Current Segment in the pattern
    int currentSegmentIndex;
    int currentPatternIndex = -1;

    const float difficultyRunTime = 120;

    // Increase difficulty after certain time frame
    IEnumerator IncreaseDifficulty()
    {
        yield return new WaitWhile(() => TutorialManager.Instance.Active);

        while (GameManager.Instance.difficulty.Value < patternDB.Count)
        {
            float difficultyTimer = difficultyRunTime;
            while (difficultyTimer > 0)
            {
                difficultyTimer -= 0.1f;
                yield return new WaitForSeconds(0.1f);
                yield return new WaitWhile(() => GameManager.Instance.gameState == GameState.Pause);
            }

            GameManager.Instance.difficulty.Value++;
        }
    }

    private void Start()
    {
        lastSegTrans = transform;
        StartCoroutine(IncreaseDifficulty());

        GenerateEmptyPattern();

        if (TutorialManager.Instance.Active)
            currentPattern = tutorialPattern;
        else
            GetNextPattern();
    }

    void GenerateEmptyPattern()
    {
        currentPattern = emptyPattern;

        for (int i = 0; i < emptyPattern.Count; i++)
            GetNextSegment();

        currentSegmentIndex = 0;
    }

    void GetNextPattern()
    {
        currentSegmentIndex = 0;

        currentPatternIndex = Random.Range(0, patternDB[GameManager.Instance.difficulty.Value].Count - 1);

        currentPattern = patternDB[GameManager.Instance.difficulty.Value][currentPatternIndex];
        Debug.Log(currentPattern.name);
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
        Segment currentSegment = currentPattern[currentSegmentIndex];

        currentSegmentIndex++;

        if (currentSegmentIndex == currentPattern.Count)
        {
            GetNextPattern();
        }

        ObjectPooler.instance.segmentActiveCount++;

        ActivateTile(interactDB.LeftLineFrame, lanes.frameLines[0].laneCenter);
        GameObject activeTileGameobject = ActivateTile(interactDB.RightLineFrame, lanes.frameLines[1].laneCenter);
        activeTileGameobject.GetComponent<TileReturner>().inActiveSegment = true;
        lastSegTrans = activeTileGameobject.transform;

        //generate on available lanes
        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
        {
            ActivateTile(currentSegment[i], lanes[i].laneCenter);

            if (!currentSegment[i].containTiles)
            {
                ActivateTile(interactDB.Tile, lanes[i].laneCenter);
            }
        }
    }

    GameObject ActivateTile(PoolableType type, float xPos)
    {
        GameObject tile = ObjectPooler.instance.GetFromPool(type);
        tile.transform.position = new Vector3(xPos, tile.transform.position.y, lastSegTrans.position.z + 1);
        tile.SetActive(true);
        return tile;
    }
}
