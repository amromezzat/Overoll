using UnityEngine;

/// <summary>
/// This Script is responsible of changing in speed of the game.
/// </summary>
public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance;

    [SerializeField]
    float gameSpeed = 5;

    public FloatField speed;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ResetSpeed()
    {
        speed.Value = gameSpeed;
    }
}
