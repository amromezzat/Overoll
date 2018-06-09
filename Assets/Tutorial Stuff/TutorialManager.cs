using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public WorkersManager wManager;
    GameManager gManager;
    GameData gdata;

    int countdowm;

    Animator tAnimator;

    private void Start()
    {
        gManager = GetComponent<GameManager>();
        gdata = gManager.gameData;
        countdowm = 3;
        tAnimator = wManager.addWorkerBtn.GetComponent<Animator>();

    }

    private void Update()
    {
        if (gdata.CoinCount > wManager.workerPrice && countdowm > 0)
        {
            gManager.GameHalt();                //to pause the game
            tAnimator.SetBool("Play", true);
        }

        if (wManager.boolForTutorial)
        {
            gManager.GameResume();
            tAnimator.SetBool("Play", false);

            countdowm -= 1;
        }
    }

}
