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

public class WorkerReturner : ObjectReturner
{
    public WorkerConfig wc;
    bool dying = false;

    private void Update()
    {
        if (dying)
        {
            Vector3 groundPos = transform.position;
            // If the worker died jumping return it to tiles position
            groundPos.y = Mathf.Lerp(groundPos.y, wc.groundLevel, 6 * Time.deltaTime);
            transform.position = groundPos;
            if (transform.position.y <= wc.groundLevel)
            {
                dying = false;
            }
        }
    }

    /// <summary>
    /// Wait enough for the worker to get out of the camera frustam
    /// before returning to pool
    /// </summary>
    /// <param name="returnTime"></param>
    /// <returns>WaitForSeconds</returns>
    public override IEnumerator ReturnToPool(float returnTime)
    {
        dying = true;
        yield return new WaitForSeconds(returnTime);
        dying = false;
        ObjectPooler.instance.ReturnToPool(poolableType, gameObject);
    }
}
