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

        #region Unity Callbacks

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            CoreGameSignals.Instance.OnGameStartAction += StartGame;
            CoreGameSignals.Instance.OnGameEndAction += QuitGame;
        }

        private void Unsubscribe()
        {
            CoreGameSignals.Instance.OnGameStartAction -= StartGame;
            CoreGameSignals.Instance.OnGameEndAction -= QuitGame;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void StartGame(int sceneIndex)
        {
            StartCoroutine(LoadAsyncScene(sceneIndex));
        }

        private void QuitGame()
        {
            Debug.Log("SceneController: QuitGame Called");
            Application.Quit();
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