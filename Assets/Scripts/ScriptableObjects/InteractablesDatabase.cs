using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class InteractablesDatabase : ScriptableObject {

 public    List<TileType> Obstacles;


  public  Dictionary<string, TileType> ListOfObstacles= new Dictionary<string, TileType>();


   void OnEnable()
    {
        for (int i = 0; i < Obstacles.Count; i++)
        {
            ListOfObstacles.Add(Obstacles[i].name, Obstacles[i]);
           

         }
       
     }
   

}


