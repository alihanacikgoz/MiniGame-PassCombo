using System;
using UnityEngine;

namespace Runtime.Signals
{
    public class CoreSignals : MonoBehaviour
    {
        #region Singleton

        public static CoreSignals Instance { get; private set; }

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        #region Pass

        public Action<int?> OnCorrectPassAction = delegate { };
        public Action<int?> OnWrongPassAction = delegate { };

        #endregion

        #region CoreGame

        public Action OnGameStartAction = delegate { };
        public Action OnGameEndAction = delegate { };
        public Action OnTeammateSpawnAction = delegate { };

        #endregion

        #region PlayerActions

        public Action OnPlayerKickForPass = delegate { };
        public Action OnPlayerCelebrate = delegate { };

        #endregion

    }
}