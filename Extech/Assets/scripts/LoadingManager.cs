using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider loadingBar;

    private void Awake()
    {
        loadingScreen.SetActive(false);
    }

    public void loadLevel(int sceneID)
    {
        StartCoroutine(LoadScene(sceneID));
    }

    IEnumerator LoadScene(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            Debug.Log("loading");
            loadingBar.value = operation.progress;
            yield return null;
        }

    }
}
