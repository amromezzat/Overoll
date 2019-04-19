using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChange : MonoBehaviour
{
    [SerializeField]
    public SkinnedMeshRenderer myHelmet;
    [SerializeField]
    public SkinnedMeshRenderer myOverall;

    public List<Mesh> helmetList = new List<Mesh>();
    public List<Mesh> OverallList = new List<Mesh>();

    private void OnEnable()
    {
        myHelmet.sharedMesh = helmetList[0];
        myOverall.sharedMesh = OverallList[0];
    }

    public void ChangeHelmet(int level)
    {
        myHelmet.sharedMesh = helmetList[level];
    }

    public void ChangeOveroll(int level)
    {
        myOverall.sharedMesh = OverallList[level];
    }       
}
