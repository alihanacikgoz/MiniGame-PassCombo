using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        #region Singleton

        public static CoreGameSignals Instance { get; private set; }

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        #region CoreGame

        public UnityAction<int> OnGameStartAction = delegate { };
        public UnityAction OnGameEndAction = delegate { };
        public UnityAction OnTeammateSpawnAction = delegate { };
        public UnityAction OnQuitToMainMenuAction = delegate { };
        public UnityAction OnRestartGameAction = delegate { };
        public UnityAction OnGameOverAction = delegate { };
        public UnityAction<DifficultyLevels> OnGettingDifficultyChanged = delegate { };
        #endregion
    }
}