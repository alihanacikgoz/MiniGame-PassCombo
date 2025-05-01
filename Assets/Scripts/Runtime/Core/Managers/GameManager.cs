using System;
using Runtime.Signals;
using Runtime.Singletons;
using UnityEngine;

namespace Runtime.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region SerializedField Variables

        [SerializeField] private GameObject singleton;

        #endregion

        private void Awake()
        {
            singleton.AddComponent<PlayerControlsSingleton>();
        }

        private void Start()
        {
            CoreGameSignals.Instance.OnTeammateSpawnAction?.Invoke();
        }
    }
}