using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class MenuController : MonoBehaviour
    {
        #region Singleton

        public static MenuController Instance { get; private set; }

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
        
        #region Costom Methods

        public void ActivateOptionsMenu()
        {
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.Options, 0);
        }

        public void BackToMainMenu()
        {
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.MainMenu, 0);
        }

        #endregion
    }
}