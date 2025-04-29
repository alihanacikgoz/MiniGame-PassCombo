using System;
using System.Collections;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.Core.Controllers
{
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

        private void OnEnable()
        {
            CoreGameSignals.Instance.OnGameStartAction += StartGame;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGameStartAction -= StartGame;
        }

        private void StartGame(int sceneIndex)
        {
            Debug.Log("SceneController StartGame Function Called");
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
}
