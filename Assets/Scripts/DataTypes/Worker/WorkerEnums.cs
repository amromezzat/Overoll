public enum WorkerState
{
    Dead,
    Leader,
    LeaderSeeker,
    Worker,
    LeaderMerger,
    MasterMerger,
    SlaveMerger,
    Dying,
    Halted
}

public enum WorkerStateTrigger
{
    Null,
    Die,
    Merge,
    SlaveMerge,
    Succeed,
    StateEnd
}

public enum WorkerFSMOutput
{
    Null,
    LeaderDied,
    WorkerDied,
    WorkerRevived,
    SeekingMasterMerger,
    LeaderMerged,
    MasterMerged,
    LeaderElected
}
