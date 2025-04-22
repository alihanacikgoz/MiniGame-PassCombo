using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            CoreSignals.Instance.OnTeammateSpawnAction?.Invoke();
        }
    }
}