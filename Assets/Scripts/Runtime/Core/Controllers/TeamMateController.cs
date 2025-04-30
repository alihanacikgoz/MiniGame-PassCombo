using System;
using System.Collections;
using Runtime.Core.Managers;
using Runtime.ScriptableObjects;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Controllers
{
    public class TeamMateController : MonoBehaviour
    {
        public PoolItem teammateOptions;
        
        private void Start()
        {
            StartCoroutine(DisappearTeammate());
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Football"))
            {
                gameObject.SetActive(false);
                BallActionsSignals.Instance.OnCorrectPassAction?.Invoke(teammateOptions.pointsWillGive);
            }
        }

        IEnumerator DisappearTeammate()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}