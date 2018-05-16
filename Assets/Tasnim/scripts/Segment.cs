using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents segment which consists of list of TileType
/// </summary>

public class Segment 
{
  //   public List<TileType> segment= new List<TileType>(5);

    TileType Lane1;
    TileType Lane2;
    TileType Lane3;
    TileType Lane4;
    TileType Lane5;

    public TileType this [int i ]
    {

        get
        {
            switch (i)
            {
                case 1:
                    return Lane1;
                    

                case 2:
                    return Lane2;
                   

                case 3:
                    return Lane3;
                   

                case 4:
                    return Lane4;
                   
                case 5:
                    return Lane5;
                    
                default:
                    return null;

            }

            
        }

        set
        {
            switch (i)
            {
                case 1:
                    Lane1 = value;
                    break;

                case 2:
                    Lane2 = value;
                    break;

                case 3:
                    Lane3 = value;
                    break;

                case 4:
                    Lane4 = value;
                    break;
                case 5:
                    Lane5 = value;
                    break;

                //default:
                //    Lane1 = null;
                //    Lane2 = null;
                //    Lane3 = null;
                //    Lane4 = null;
                //    Lane5 = null;
            }
          
        }
    }
}

