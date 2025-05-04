using Runtime.Core.Managers;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.MainMenu, 0);
                    break;
                case 1:
                    CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.PauseMenu, 0);
                    break;
                default:
                    break;
            }
            
        }
        
        public void StartGame()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            CoreUISignals.Instance.OnCloseAllPanelsAction?.Invoke();
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

        public void OnButtonClick()
        {
            CoreUISignals.Instance.OnButtonClickAction?.Invoke();
        }

        public void Pause()
        {
            CoreUISignals.Instance.OnCloseAllPanelsAction?.Invoke();
            CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.PauseMenu, 0);
            Time.timeScale = 0;
        }
        
        public void Resume()
        {
            CoreUISignals.Instance.OnCloseAllPanelsAction?.Invoke();
            Time.timeScale = 1;
        }

        public void QuitToMainMenu()
        {
            CoreUISignals.Instance.OnCloseAllPanelsAction?.Invoke();
            if (Time.timeScale == 0)
                Time.timeScale = 1;
            CoreGameSignals.Instance.OnQuitToMainMenuAction?.Invoke();
        }

        public void EasyDifficulty()
        {
            CoreGameSignals.Instance.OnGettingDifficultyChanged(DifficultyLevels.Easy);
        }
        
        public void MediumDifficulty()
        {
            CoreGameSignals.Instance.OnGettingDifficultyChanged(DifficultyLevels.Medium);
        }
        
        public void HardDifficulty()
        {
            CoreGameSignals.Instance.OnGettingDifficultyChanged(DifficultyLevels.Hard);
        }

        #endregion
    }
}