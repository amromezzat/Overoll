using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public VideoPlayer videoLogo;
    public RawImage rawImage;

    private int sceneInd;

    private void Start()
    {
        sceneInd = 1;
        StartCoroutine(LoadAsynchronously(sceneInd));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        videoLogo.Prepare();

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        while (!videoLogo.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        rawImage.texture = videoLogo.texture;
        videoLogo.Play();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Debug.Log(progress);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}