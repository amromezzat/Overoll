using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Pattern" , menuName = "Config/Pattern Editor/Pattern")]
public class PatternSO : ScriptableObject
{
    public int difficulty;
    public float length;
    public float sectionLength;
   //public List<ObjectSO>;
    [SerializeField]
    public List<Segment> segmentList = new List<Segment>();
}
