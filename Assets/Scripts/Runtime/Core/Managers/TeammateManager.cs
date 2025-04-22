using System.Collections.Generic;
using NaughtyAttributes;
using Runtime.ScriptableObjects;
using UnityEngine;

namespace Runtime.Core.Managers
{
    public class TeammateManager : MonoBehaviour
    {
        #region Singleton

        public static TeammateManager Instance { get; private set; }

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

        

        #endregion
    }
}
