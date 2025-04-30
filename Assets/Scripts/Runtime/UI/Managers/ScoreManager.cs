using NaughtyAttributes;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Singleton

        public static ScoreManager Instance { get; private set; }

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

        [Foldout("Player Score"), SerializeField] private int score;
        [Foldout("Player Score"), SerializeField] private TextMeshProUGUI scoreText;

        #endregion

        private void OnEnable()
        {
            BallActionsSignals.Instance.OnCorrectPassAction += AddScore;
            BallActionsSignals.Instance.OnWrongPassAction += DeductScore;
        }

        private void DeductScore(int? points)
        {
            score-=points ?? 1;
            scoreText.text = score.ToString();

        }

        private void AddScore(int? points)
        {
            score+= points ?? 1;
            scoreText.text = score.ToString();
        }

        private void OnDisable()
        {
            BallActionsSignals.Instance.OnCorrectPassAction -= AddScore;
            BallActionsSignals.Instance.OnWrongPassAction -= DeductScore;
        }
    }
}