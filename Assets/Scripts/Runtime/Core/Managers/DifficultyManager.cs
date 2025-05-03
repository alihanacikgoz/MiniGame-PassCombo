using System;
using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Core.Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        public DifficultyLevels currentLevel;
        public List<DifficultySettings> difficultyConfigs;

        public static event Action<DifficultySettings> OnDifficultyChanged;

        public void SetDifficulty(DifficultyLevels level)
        {
            currentLevel = level;
            DifficultySettings settings = difficultyConfigs.Find(x => x.level == level);
            if (settings == null)
            {
                OnDifficultyChanged?.Invoke(settings);
            }
            else
            {
                Debug.Log("There is no any feature for the difficulty level selected.");
            }
        }
    }
}