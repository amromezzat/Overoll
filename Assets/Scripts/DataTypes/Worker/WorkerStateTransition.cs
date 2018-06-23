using System.Collections.Generic;

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
            new TransitionBundle(WorkerState.Tutoring, WorkerState.Leader)
        };

        workerTransitionsDic[WorkerStateTrigger.Die] = new List<TransitionBundle>() {
            new TransitionBundle(WorkerState.Leader, WorkerState.Dead, WorkerFSMOutput.LeaderDied),
            new TransitionBundle(WorkerState.Worker, WorkerState.Dead, WorkerFSMOutput.WorkerDied)
        };

        workerTransitionsDic[WorkerStateTrigger.Merge] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Leader, WorkerState.LeaderMerger),
            new TransitionBundle(WorkerState.Worker, WorkerState.MasterMerger)
        };

        workerTransitionsDic[WorkerStateTrigger.SlaveMerge] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Worker, WorkerState.SlaveMerger, WorkerFSMOutput.SeekingMasterMerger)
        };

        workerTransitionsDic[WorkerStateTrigger.Succeed] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Worker, WorkerState.LeaderSeeker, WorkerFSMOutput.LeaderElected)
        };

        workerTransitionsDic[WorkerStateTrigger.Null] = new List<TransitionBundle>();

        workerTransitionsDic[WorkerStateTrigger.StateEnd] = new List<TransitionBundle>() {
            new TransitionBundle(WorkerState.LeaderSeeker, WorkerState.Leader),
            new TransitionBundle(WorkerState.LeaderMerger, WorkerState.Leader, WorkerFSMOutput.LeaderMerged),
            new TransitionBundle(WorkerState.MasterMerger, WorkerState.Worker, WorkerFSMOutput.MasterMerged),
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
