using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        #region Singleton
        
        public static DifficultyManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            CoreGameSignals.Instance.OnGettingDifficultyChanged += SetDifficulty;
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        
        public List<DifficultySettings> difficultyConfigs;
        private DifficultySettings settings;
        public DifficultySettings selectedSettings;

        private void SetDifficulty(DifficultyLevels level)
        {
            selectedSettings = SettingDifficultyLevel(level);
        }

        public DifficultySettings SettingDifficultyLevel(DifficultyLevels level)
        {
            settings = difficultyConfigs.Find(x => x.level == level);
            if (settings != null)
            {
                Debug.Log("DifficultyManager Settings update to " + settings.level );
                return settings;
            }
            else
            {
                settings = difficultyConfigs[0];
            }
            return null;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnGettingDifficultyChanged -= SetDifficulty;
        }
    }
}