using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Runtime.ScriptableObjects;
using Runtime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [Foldout("Spawn Points"), SerializeField]
        private Transform northWestBorder;

        [Foldout("Spawn Points"), SerializeField]
        private Transform southWestBorder;

        [Foldout("Spawn Points"), SerializeField]
        private Transform northEastBorder;

        [Foldout("Spawn Points"), SerializeField]
        private Transform southEastBorder;

        #endregion

        private void OnEnable()
        {
            CoreSignals.Instance.OnTeammateSpawnAction += TeamMateSpawnAction;
        }

        private void TeamMateSpawnAction()
        {
            Debug.Log("Çalıştı");
            Vector2 min = new Vector2(Mathf.Min(northWestBorder.position.x, southWestBorder.position.x), Mathf.Min(northWestBorder.position.y, southWestBorder.position.y));
            Vector2 max = new Vector2(Mathf.Min(northEastBorder.position.x, southEastBorder.position.x), Mathf.Min(northEastBorder.position.y, southEastBorder.position.y));

            float randomX = Random.Range(min.x + 2, max.x + 2);
            float randomY = Random.Range(min.y + 2, max.y + 2);

            Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);

            ObjectPoolingManager.Instance.Get("TeamMate", spawnPoint);
        }
    }
}