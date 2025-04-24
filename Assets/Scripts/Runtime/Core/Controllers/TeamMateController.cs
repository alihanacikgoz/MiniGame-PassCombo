using System;
using Runtime.Core.Managers;
using Runtime.ScriptableObjects;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class TeamMateController : MonoBehaviour
    {
        public PoolItem teammateOptions;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Football"))
            {
                gameObject.SetActive(false);
                CoreSignals.Instance.OnCorrectPassAction?.Invoke(teammateOptions.pointsWillGive);
            }
        }
    }
}