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

using System.Collections.Generic;

/// <summary>
/// Transitions Data
/// </summary>
public class WorkerStateTransition
{
    Dictionary<WorkerStateTrigger, List<TransitionBundle>> workerTransitionsDic = new Dictionary<WorkerStateTrigger, List<TransitionBundle>>();

    public WorkerStateTransition()
    {
        workerTransitionsDic[WorkerStateTrigger.StartTutoring] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Halted, WorkerState.Tutoring),
            new TransitionBundle(WorkerState.Leader, WorkerState.Tutoring)
        };
        workerTransitionsDic[WorkerStateTrigger.EndTutoring] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Tutoring, WorkerState.Leader, WorkerFSMOutput.TutEnded)
        };

        workerTransitionsDic[WorkerStateTrigger.Die] = new List<TransitionBundle>() {
            new TransitionBundle(WorkerState.Leader, WorkerState.Dead, WorkerFSMOutput.LeaderDied),
            new TransitionBundle(WorkerState.LeaderSeeker, WorkerState.Dead, WorkerFSMOutput.LeaderDied),
            new TransitionBundle(WorkerState.LeaderMerger, WorkerState.Dead, WorkerFSMOutput.LeaderDied),
            new TransitionBundle(WorkerState.Worker, WorkerState.Dead, WorkerFSMOutput.WorkerDied)
        };

        workerTransitionsDic[WorkerStateTrigger.Merge] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Leader, WorkerState.LeaderMerger),
            new TransitionBundle(WorkerState.Worker, WorkerState.MasterMerger),
            new TransitionBundle(WorkerState.LeaderSeeker, WorkerState.SeekerMerger),
        };

        workerTransitionsDic[WorkerStateTrigger.SlaveMerge] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Worker, WorkerState.SlaveMerger, WorkerFSMOutput.SeekingMasterMerger)
        };

        workerTransitionsDic[WorkerStateTrigger.Succeed] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Worker, WorkerState.LeaderSeeker, WorkerFSMOutput.LeaderElected),
            new TransitionBundle(WorkerState.MasterMerger, WorkerState.SeekerMerger, WorkerFSMOutput.LeaderElected)
        };

        workerTransitionsDic[WorkerStateTrigger.Null] = new List<TransitionBundle>();

        workerTransitionsDic[WorkerStateTrigger.StateEnd] = new List<TransitionBundle>() {
            new TransitionBundle(WorkerState.LeaderSeeker, WorkerState.Leader),
            new TransitionBundle(WorkerState.SeekerMerger, WorkerState.LeaderSeeker, WorkerFSMOutput.MergingDone),
            new TransitionBundle(WorkerState.LeaderMerger, WorkerState.Leader, WorkerFSMOutput.MergingDone),
            new TransitionBundle(WorkerState.MasterMerger, WorkerState.Worker, WorkerFSMOutput.MergingDone),
            new TransitionBundle(WorkerState.SlaveMerger, WorkerState.Worker),
            new TransitionBundle(WorkerState.Tutoring, WorkerState.Tutoring, WorkerFSMOutput.TutRightInput)
        };
    }

    public TransitionBundle ChangeState(WorkerStateTrigger trigger, WorkerState currentState)
    {
        foreach (TransitionBundle bundle in workerTransitionsDic[trigger])
        {
            if (bundle.Source == currentState)
            {
                return bundle;
            }
        }
        return new TransitionBundle(currentState, currentState);
    }
}
