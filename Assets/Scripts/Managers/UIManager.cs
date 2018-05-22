using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public GameState gameState;
    public Button playBtn;
    public Button settingsBtn;
    public Button storeBtn;


	void Start () {
        
	}
	

	void Update () {
		
	}

    public void PlayMoveBtn () {
        var setBtn = settingsBtn.gameObject.transform.position;
    }
}
