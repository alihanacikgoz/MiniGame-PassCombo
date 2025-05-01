using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Core.Managers
{
    public class TimeManager : MonoBehaviour
    {
        #region Singleton

        public static TimeManager Instance { get; private set; }

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
        
        #region SerializedField Variables
        
        [FoldoutGroup("Timer UI"), SerializeField] private TextMeshProUGUI timerText;
        
        #endregion

        #region Public Variables

        public float timeRemaining = 30f;

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            BallActionsSignals.Instance.OnCorrectPassAction += UpdateTimeRemaining;
        }

        private void OnDisable()
        {
            BallActionsSignals.Instance.OnCorrectPassAction -= UpdateTimeRemaining;
        }

        private void Update()
        {
            Countdown();
        }
        
        #endregion

        #region Custom Methods
        
        private void Countdown()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                Time.timeScale = 0;
                UpdateTimerUI();
                CoreUISignals.Instance.OnOpenPanelAction?.Invoke(UIPanelTypes.GameOverMenu, 2);
            }
        }
        
        private void UpdateTimeRemaining(int? addTime)
        {
            timeRemaining += addTime ?? 1;
        }

        private void UpdateTimerUI()
        {
            timerText.text = $"{timeRemaining:00}:{timeRemaining % 1 * 10:00}";
        }

        #endregion
    }
}