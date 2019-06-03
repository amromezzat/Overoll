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

    private int sceneInd;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnCurrentSceneLoaded;
    }

    private void OnCurrentSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        sceneInd = 1;
        StartCoroutine(LoadAsynchronously(sceneInd));
        SceneManager.sceneLoaded -= OnCurrentSceneLoaded;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return new WaitForSeconds(1.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Debug.Log(progress);
            slider.value = progress;
            progressText.text = string.Format("{0}%", Mathf.Round(progress * 100f));
            yield return null;
        }
    }
}