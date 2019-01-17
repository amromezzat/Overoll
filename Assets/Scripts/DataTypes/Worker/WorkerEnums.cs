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

public enum WorkerState
{
    Dead,
    Leader,
    // When leader dies, the elected leader until it reaches leader position
    LeaderSeeker,
    Worker,
    // When the no. of workers is 4 and the 5th worker is the leader.
    LeaderMerger,
    // When merge starts, normal worker awaits other 4 workers to merge with him
    MasterMerger,
    // Wehn merge starts, normal workers follows a master merger until he collides with him
    SlaveMerger,
    // When leader dies and master merger is elected to be a leader
    SeekerMerger,
    Dying,
    Tutoring,
    Halted
}

public enum WorkerStateTrigger
{
    Null,
    Die,
    Merge,
    SlaveMerge,
    Succeed,
    StartTutoring,
    EndTutoring,
    StateEnd
}

public enum WorkerFSMOutput
{
    Null,
    LeaderDied,
    WorkerDied,
    WorkerRevived,
    SeekingMasterMerger,
    MergingDone,
    LeaderElected,
    TutRightInput,
    TutEnded
}
