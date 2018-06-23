using System.Collections;

interface IChangeSpeed
{
    void SpeedUp();
    void SlowDown();

    //keep slowing down after initial slow down
    //until speed returns to normal;
    IEnumerator KillSpeed();
}
