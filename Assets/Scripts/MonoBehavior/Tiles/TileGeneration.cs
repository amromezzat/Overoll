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
    public PatternsDatabase patternDB;//[]->difficulty, [][]->pattern
    public LanesDatabase lanes;
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

        if (GameManager.Instance.difficulty.Value > 0)
        {
            GameManager.Instance.difficulty.Value = (GameManager.Instance.difficulty.Value + 1) % availableDifficulties + 1;
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

        if (!TutorialManager.Instance.tutorialActive && GameManager.Instance.difficulty.Value == 0)
            GameManager.Instance.difficulty.Value++;

        //get a random pattern
        currentPatternIndex++;

        if (currentPatternIndex == patternDB[GameManager.Instance.difficulty.Value].Count)
        {
            currentPatternIndex = 0;
            GameManager.Instance.difficulty.Value++;
        }

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
        currentSegmentIndex = (currentSegmentIndex + 1) % currentPattern.Count;
        Segment currentSegment = currentPattern[currentSegmentIndex];

        if (currentSegmentIndex == currentPattern.Count)
        {
            if (GameManager.Instance.difficulty.Value == 0)
            {
                GameManager.Instance.difficulty.Value++;
            }
            InitPattern();
        }
        ObjectPooler.instance.segmentActiveCount++;


        GameObject tile = ObjectPooler.instance.GetFromPool(interactDB.LeftLineFrame);
        Vector3 objpos = tile.transform.position;
        objpos.x = lanes.frameLines[0].laneCenter;
        objpos.z = lastSegTrans.position.z + 1f;
        tile.transform.position = objpos;
        tile.SetActive(true);

        tile = ObjectPooler.instance.GetFromPool(interactDB.RightLineFrame);
        objpos = tile.transform.position;
        objpos.x = lanes.frameLines[1].laneCenter;
        objpos.z = lastSegTrans.position.z + 1f;
        tile.transform.position = objpos;
        tile.SetActive(true);

        //generate on available lanes
        for (int i = 0; i < lanes.OnGridLanes.Count; i++)
        {
            tile = ObjectPooler.instance.GetFromPool(currentSegment[i]);
            objpos = tile.transform.position;
            objpos.x = lanes[i].laneCenter;
            objpos.z = lastSegTrans.position.z + 1f;
            tile.transform.position = objpos;
            tile.SetActive(true);

            if (!currentSegment[i].containTiles)
            {
                tile = ObjectPooler.instance.GetFromPool(interactDB.Tile);
                objpos.y = tile.transform.position.y;
                tile.transform.position = objpos;
                tile.SetActive(true);
            }
        }

        lastSegTrans = tile.transform;
        tile.GetComponent<TileReturner>().inActiveSegment = true;
    }
}
