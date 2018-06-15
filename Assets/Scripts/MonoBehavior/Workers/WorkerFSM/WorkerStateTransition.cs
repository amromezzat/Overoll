using System.Collections.Generic;

public class WorkerStateTransition
{

    Dictionary<WorkerStateTrigger, List<TransitionBundle>> workerTransitionsDic = new Dictionary<WorkerStateTrigger, List<TransitionBundle>>();

    public WorkerStateTransition()
    {
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
            new TransitionBundle(WorkerState.Worker, WorkerState.SlaveMerger, WorkerFSMOutput.SlaveMerged)
        };

        workerTransitionsDic[WorkerStateTrigger.Succeed] = new List<TransitionBundle>()
        {
            new TransitionBundle(WorkerState.Worker, WorkerState.LeaderSeeker, WorkerFSMOutput.LeaderElected)
        };

        workerTransitionsDic[WorkerStateTrigger.Null] = new List<TransitionBundle>();

        workerTransitionsDic[WorkerStateTrigger.StateEnd] = new List<TransitionBundle>() {
            new TransitionBundle(WorkerState.LeaderSeeker, WorkerState.Leader),
            new TransitionBundle(WorkerState.MasterMerger, WorkerState.Worker),
            new TransitionBundle(WorkerState.SlaveMerger, WorkerState.Worker),
            new TransitionBundle(WorkerState.LeaderMerger, WorkerState.Leader) 
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
