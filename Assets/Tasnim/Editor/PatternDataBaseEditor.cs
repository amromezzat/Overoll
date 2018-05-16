using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PatternDB))]
public class PatternDataBaseEditor : Editor {





    public PatternDB PatternDataBase;
    //Variables to Edit 
    public PatternSO Difficulty;
    public PatternSO Lenght;
    //----------------------
    public Segment segment;





}
