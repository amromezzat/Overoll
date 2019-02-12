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
using System.Linq;

[CreateAssetMenu(fileName = "PoolDatabase", menuName = "Database/Pool")]
public class PoolDatabase : ScriptableObject
{
    public List<PoolableObj> poolableList;

    public int Count
    {
        get
        {
            if (poolableList == null)
                return 0;
            return poolableList.Count;
        }
        set
        {
            while (value < poolableList.Count)
                poolableList.RemoveAt(poolableList.Count - 1);
            while (value > poolableList.Count)
                poolableList.Add(poolableList[poolableList.Count - 1]);
        }
    }

    public PoolableObj this[int index]
    {
        get
        {
            return poolableList[index];
        }
        set
        {
            poolableList[index] = value;
        }
    }

    public PoolableObj this[PoolableType type]
    {
        get
        {
            return poolableList.FirstOrDefault(po => po.type == type);
        }
        set
        {
            int poIndex = poolableList.FindIndex(po => po.type == type);
            poolableList[poIndex] = value;
        }
    }

    public void Add(PoolableObj item)
    {
        if (!poolableList.Any(po => po.type == item.type))
            poolableList.Add(item);
    }

    public bool Remove(PoolableObj item)
    {
        return poolableList.Remove(item);
    }

    public void RemoveAt(int index)
    {
        poolableList.RemoveAt(index);
    }

    public bool Contains(PoolableType type)
    {
        if (poolableList.Any(po => po.type == type))
            return true;
        return false;
    }

    public void RemoveAll()
    {
        poolableList.RemoveAll(_=>true);
    }
}
