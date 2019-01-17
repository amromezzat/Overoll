using UnityEngine;

/// <summary>
/// This Script is responsible of changing in speed of the game.
/// </summary>
public class SpeedManager : MonoBehaviour
{
    public FloatVariable speed;
    public float defaultSpeed;
    public float oldSpeed;

    public static SpeedManager Instance;

    private void OnEnable()
    {
        if (Instance = null)
        {
            Instance = this;
        }

        speed.SetValue(defaultSpeed);
        //his mosh de hat3mel bug enny aft7 el game mn gded yrag3ly el speed l el default?
    }

    public float GetSpeedValue()
    {
        return speed.value;
    }

    public void SetSpeedValue(float val)
    {
        oldSpeed = speed.value;
        speed.SetValue(val);
    }


}
