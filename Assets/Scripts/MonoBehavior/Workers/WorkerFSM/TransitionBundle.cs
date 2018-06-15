public class TransitionBundle
{
    public WorkerState Source
    {
        get; private set;
    }

    public WorkerState Destination
    {
        get; private set;
    }

    public WorkerFSMOutput Output
    {
        get; private set;
    }

    public TransitionBundle(WorkerState source, WorkerState destination)
    {
        Source = source;
        Destination = destination;
    }

    public TransitionBundle(WorkerState source, WorkerState destination,WorkerFSMOutput output )
    {
        Source = source;
        Destination = destination;
        Output = output;
    }
}