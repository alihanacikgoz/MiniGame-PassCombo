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

        private void Start()
        {
            CoreSignals.Instance.OnTeammateSpawnAction += TeamMateSpawnAction;
        }

        private void TeamMateSpawnAction()
        {
            float minX = Mathf.Min(northWestBorder.position.x, southWestBorder.position.x);
            float maxX = Mathf.Max(northEastBorder.position.x, southEastBorder.position.x);

            float minY = Mathf.Min(southWestBorder.position.y, southEastBorder.position.y);
            float maxY = Mathf.Max(northWestBorder.position.y, northEastBorder.position.y);

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);
            
            //ObjectPoolingManager.Instance.Get("TeamMate", spawnPoint);
            ObjectPoolingManager.Instance.Get("TeamMate", spawnPoint);
        }
        
    }
}