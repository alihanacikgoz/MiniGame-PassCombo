using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoBehaviour
    {
        #region Singleton

        public static CoreUISignals Instance { get; private set; }

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

        #region CoreUISignals

        public UnityAction<UIPanelTypes, int> OnOpenPanelAction = delegate { };
        public UnityAction<int> OnClosePanelAction = delegate { };
        public UnityAction OnCloseAllPanelsAction = delegate { };
        public UnityAction OnButtonClickAction = delegate { };

        #endregion
    }
}