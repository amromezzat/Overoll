using UnityEngine;

/// <summary>
/// This Script is responsible of changing in speed of the game.
/// </summary>
public class SpeedManager : MonoBehaviour
{
    static SpeedManager instance;
    public static SpeedManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SpeedManager>();

            return instance;
        }
    }

    public float gameSpeed = 5;

    public FloatField speed;

    public void ResetSpeed()
    {
        speed.Value = gameSpeed;
    }
}
