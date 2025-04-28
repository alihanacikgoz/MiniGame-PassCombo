using System;
using UnityEngine;

namespace Runtime.Signals
{
    public class PlayerActionsSignals : MonoBehaviour
    {
        #region Singleton

        public static PlayerActionsSignals Instance { get; private set; }

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
        
        #region PlayerActions

        public Action OnPlayerKickForPass = delegate { };
        public Action OnPlayerCelebrate = delegate { };

        #endregion
    }
}