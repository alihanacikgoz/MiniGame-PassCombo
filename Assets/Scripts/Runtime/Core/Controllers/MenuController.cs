using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class MenuController : MonoBehaviour
    {
        
        #region Costom Methods

        public void ActivateOptionsMenu()
        {
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.Options, 0);
        }

        public void BackToMainMenu()
        {
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.MainMenu, 0);
        }

        public void StartGame()
        {
            Debug.Log("MenuController StartGame Function Called");
            CoreGameSignals.Instance.OnGameStartAction?.Invoke(1);
        }

        #endregion
    }
}