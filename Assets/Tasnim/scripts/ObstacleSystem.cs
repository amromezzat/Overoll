using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ObstacleSystem : ScriptableObject {

 public    List<EnumValue> Obstacles;


  public  Dictionary<string, EnumValue> ListOfObstacles= new Dictionary<string, EnumValue>();

   void OnEnable()
    {
        for (int i = 0; i < Obstacles.Count; i++)
        {
            ListOfObstacles.Add(Obstacles[i].name, Obstacles[i]);
        }
       

    }


}


