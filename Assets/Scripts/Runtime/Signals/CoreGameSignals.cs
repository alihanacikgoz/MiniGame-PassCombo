using System;
using UnityEngine;

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

        public Action OnGameStartAction = delegate { };
        public Action OnGameEndAction = delegate { };
        public Action OnTeammateSpawnAction = delegate { };

        #endregion
    }
}