using NaughtyAttributes;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Managers
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

        #endregion

        private void OnEnable()
        {
            CoreSignals.Instance.OnCorrectPassAction += AddScore;
            CoreSignals.Instance.OnWrongPassAction += DeductScore;
        }

        private void DeductScore(int? points)
        {
            score-=points ?? 1;
        }

        private void AddScore(int? points)
        {
            score+= points ?? 1;
        }

        private void OnDisable()
        {
            CoreSignals.Instance.OnCorrectPassAction -= AddScore;
            CoreSignals.Instance.OnWrongPassAction -= DeductScore;
        }
    }
}