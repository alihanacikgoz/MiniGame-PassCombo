using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region SerializeField Variables

        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #region Singleton
        
        public static UIPanelController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        #endregion
        
        #region Unity Callbacks

        private void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0 )
            {
                OnOpenPanel(UIPanelTypes.MainMenu,0);
            }
        }

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.OnClosePanelAction += OnClosePanel;
            CoreUISignals.Instance.OnOpenPanelAction += OnOpenPanel;
            CoreUISignals.Instance.OnCloseAllPanelsAction += OnCloseAllPanels;
        }

        [Button]
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) return;
#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
            }
        }

        [Button]
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screen/{panelType}Panel"), layers[value]);
        }

        [Button]
        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
#if UNITY_EDITOR
            DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
            Destroy(layers[value].GetChild(0).gameObject);
#endif
        }

        private void UnsubscribeEvents()
        {
            CoreUISignals.Instance.OnClosePanelAction -= OnClosePanel;
            CoreUISignals.Instance.OnOpenPanelAction -= OnOpenPanel;
            CoreUISignals.Instance.OnCloseAllPanelsAction -= OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}