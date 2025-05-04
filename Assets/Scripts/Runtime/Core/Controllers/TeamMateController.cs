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
        public float waitForSeconds = 2f;

        private void Start()
        {
            StartCoroutine(DisappearTeammate());
            waitForSeconds = DifficultyManager.Instance.selectedSettings.waitForSeconds;
            Debug.Log("Difficulty Level"+waitForSeconds);
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
            yield return new WaitForSeconds(waitForSeconds);
            gameObject.SetActive(false);
        }
    }
}