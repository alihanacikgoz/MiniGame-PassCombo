using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private int pointsWillDeduct;
        private void OnBecameInvisible()
        {
            CoreSignals.Instance.OnWrongPassAction?.Invoke(pointsWillDeduct);
        }
    }
}