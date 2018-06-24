public enum WorkerState
{
    Dead,
    Leader,
    LeaderSeeker,
    Worker,
    LeaderMerger,
    MasterMerger,
    SlaveMerger,
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
