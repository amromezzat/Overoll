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

public class WorkerWithoutVestCollide : WorkerCollide
{
    WorkerFSM mState;
    MeshChange mMeshChange;

    public WorkerWithoutVestCollide(Animator animator, Rigidbody rb, WorkerFSM state, MeshChange mesh) : base(animator, rb)
    {
        mState = state;
        mMeshChange = mesh;
    }

    public override WorkerStateTrigger Collide(Collider collider, ref int health)
    {
        IObstacle collidableObstacle = collider.GetComponent<IObstacle>();
        // When a worker hits an obstacle it decreases his health by its health
        // and vice versa, if the worker loses all his health he dies
        if (collidableObstacle != null)
        {
            int obsHealth = collidableObstacle.Gethealth();
            int preCollisionWH = health;
            health -= obsHealth;

            collidableObstacle.ReactToCollision(preCollisionWH);

            if (health <= 0)
            {
                collidableObstacle.PlayEffect(animator, rb, VestState.WithoutVest);
                return WorkerStateTrigger.Die;
            }
            else
            {
                int workersHealthFrac = health % 5;
                health -= workersHealthFrac;
                mState.level = health / 5;
                WorkersManager.Instance.Descend(mState);

                mMeshChange.ChangeHelmet(mState.level);
                mMeshChange.ChangeOveroll(mState.level);

                Vector2 pos;
                for (int i = 1; i <= workersHealthFrac; i++)
                {
                    pos.x = Random.Range(mState.transform.position.x - 0.05f, mState.transform.position.x + 0.05f);
                    pos.y = Random.Range(mState.transform.position.z - 0.1f, mState.transform.position.z - 0.05f);
                    WorkersManager.Instance.AddWorker(pos);
                }
            }
        }
        return WorkerStateTrigger.Null;
    }
}
