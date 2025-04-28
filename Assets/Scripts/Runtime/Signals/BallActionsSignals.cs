using System;
using UnityEngine;

namespace Runtime.Signals
{
    public class BallActionsSignals : MonoBehaviour
    {
        #region Singleton

        public static BallActionsSignals Instance { get; private set; }

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
        
        #region Pass

        public Action<int?> OnCorrectPassAction = delegate { };
        public Action<int?> OnWrongPassAction = delegate { };

        #endregion
    }
}