using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public GameState gameState;
    public Button playBtn;
    public Button settingsBtn;
    public Button storeBtn;

    public Animator settingAnim;
    public Animator storeAnim;

    //public Canvas mainMenu

    public void PlayMoveBtn () {
        settingAnim.SetBool("SetBtnIsOut", false);
        storeAnim.SetBool("StoreBtnIsOut", false);

        
    }

    public void SettingsBtn()
    {

    }
}
