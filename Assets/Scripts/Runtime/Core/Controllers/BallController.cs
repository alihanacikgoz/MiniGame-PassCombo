using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private int pointsWillDeduct;
        private void OnBecameInvisible()
        {
            BallActionsSignals.Instance.OnWrongPassAction?.Invoke(pointsWillDeduct);
        }
    }
}