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
/// List carrying items that are generated in the lane
/// </summary>
[CreateAssetMenu(fileName = "InteractablesDatabase", menuName = "Database/Interactables")]
public class InteractablesDatabase : ScriptableObject
{
    public List<PoolableType> poolableTypeList;
    public PoolableType Tile
    {
        get
        {
            return poolableTypeList[0];
        }
    }

    public PoolableType RightLineFrame
    {
        get
        {
            return poolableTypeList[2];
        }
    }

    public PoolableType LeftLineFrame
    {
        get
        {
            return poolableTypeList[1];
        }
    }

    [HideInInspector]
    public List<string> interactablesNames;

    public int Count
    {
        get
        {
            return poolableTypeList.Count;
        }
    }

    public PoolableType this[string name]
    {
        get
        {
            return poolableTypeList[interactablesNames.IndexOf(name)];
        }
        set
        {
            int index = interactablesNames.IndexOf(name);
            poolableTypeList[index] = value;
            interactablesNames[index] = value.name;
        }
    }

    public PoolableType this[int index]
    {
        get
        {
            return poolableTypeList[index];
        }
    }



    public void Add(PoolableType tile)
    {
        if (!poolableTypeList.Contains(tile))
            poolableTypeList.Add(tile);
        interactablesNames.Add(tile.name);
    }

    public bool Remove(PoolableType tile)
    {
        interactablesNames.Remove(tile.name);
        return poolableTypeList.Remove(tile);
    }

    public void RemoveAt(int index)
    {
        poolableTypeList.RemoveAt(index);
        interactablesNames.RemoveAt(index);
    }

    public void SwapByIndex(int num1, int num2)
    {
        PoolableType poolableTemp = poolableTypeList[num1];
        poolableTypeList[num1] = poolableTypeList[num2];
        poolableTypeList[num2] = poolableTemp;

        string interactableNameTemp = interactablesNames[num1];
        interactablesNames[num1] = interactablesNames[num2];
        interactablesNames[num2] = interactableNameTemp;
    }

}


