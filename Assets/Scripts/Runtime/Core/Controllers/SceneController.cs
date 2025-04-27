using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Singleton
    
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void StartGame(int sceneIndex)
    {
        StartCoroutine(LoadAsyncScene(sceneIndex));
    }

    IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!asyncOperation.isDone)
        {
            Debug.Log(asyncOperation.progress);
            yield return null;
        }
    }
}
