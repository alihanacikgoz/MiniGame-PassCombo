using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.UI.Controllers
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
            CoreUISignals.Instance.OnCloseAllPanelsAction?.Invoke();
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.MainMenu, 0);
        }
        
        public void StartGame()
        {
            CoreGameSignals.Instance.OnGameStartAction?.Invoke(1);
        }
        
        public void AskForQuit()
        {
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.Confirm, 1);
        }

        public void QuitGame()
        {
            CoreGameSignals.Instance.OnGameEndAction?.Invoke();
        }

        #endregion
    }
}