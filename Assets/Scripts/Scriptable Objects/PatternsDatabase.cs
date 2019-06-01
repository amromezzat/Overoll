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

[CreateAssetMenu(fileName = "PatternDatabase", menuName = "Database/Patterns")]
public class PatternsDatabase : ScriptableObject
{
    [SerializeField]
    List<Difficulty> PatternDBList;

    public PatternsDatabase()
    {
        PatternDBList = new List<Difficulty>();
    }

    public int Count
    {
        get
        {
            return PatternDBList.Count;
        }
    }

    public Difficulty this[int index]
    {
        get
        {
            return PatternDBList[index];
        }
        set
        {
            PatternDBList[index] = value;
        }
    }

    public void Add(Difficulty diff)
    {
        PatternDBList.Add(diff);
    }

    public void Remove(Difficulty diff)
    {
        PatternDBList.Remove(diff);
    }

    public void RemoveAt(int index)
    {
        PatternDBList.RemoveAt(index);
    }

    public void RemoveRange(int index, int count)
    {
        PatternDBList.RemoveRange(index, count);
    }

}
