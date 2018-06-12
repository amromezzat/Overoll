using UnityEngine;

[System.Flags]
public enum FollowType
{
    Leader = 1,
    LeaderFollow = 2,
    MergeMaster = 4,
    MergeSlave = 8,
    Dying
}



public class WorkerFollowState : MonoBehaviour
{

    public FollowType followType = FollowType.LeaderFollow;
    static FollowType Mergeable = FollowType.MergeMaster | FollowType.MergeSlave;

    public bool leader
    {
        get
        {
            return (FollowType.Leader & followType) > 0;
        }
    }

    public bool follower
    {
        get
        {
            return (FollowType.LeaderFollow & followType) > 0;
        }
    }

    public bool merging
    {
        get
        {
            return ((FollowType.MergeMaster | FollowType.MergeSlave) & followType) > 0;
        }
    }

    public bool leaderMerge
    {
        get
        {
            return ((FollowType.Leader | FollowType.MergeMaster) & followType) == (FollowType.Leader | FollowType.MergeMaster);
        }
    }

    public bool followerMergeMaster
    {
        get
        {
            return ((FollowType.LeaderFollow | FollowType.MergeMaster) & followType) == (FollowType.LeaderFollow | FollowType.MergeMaster);
        }
    }

    public bool followerMergeSlave
    {
        get
        {
            return ((FollowType.LeaderFollow | FollowType.MergeSlave) & followType) == (FollowType.LeaderFollow | FollowType.MergeMaster);
        }
    }

    public bool alive
    {
        get
        {
            return followType != FollowType.Dying;
        }
    }

    public bool CanMerge(WorkerFollowState otherWorker)
    {
        if ((otherWorker.followType | followType) == Mergeable)
        {
            return true;
        }
        return false;
    }

}
