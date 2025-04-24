using System;
using System.Collections;
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
        
        [Foldout("Spawn Points"), SerializeField] private Transform northWestBorder;
        [Foldout("Spawn Points"), SerializeField] private Transform southWestBorder;
        [Foldout("Spawn Points"), SerializeField] private Transform northEastBorder;
        [Foldout("Spawn Points"), SerializeField] private Transform southEastBorder;
        
        [Foldout("Spawn Mechanics"), SerializeField] private float minDistanceFromOthers = 1.5f;
        [Foldout("Spawn Mechanics"), SerializeField] private float minDistanceFromPlayer = 2f;
        [Foldout("Spawn Mechanics"), SerializeField] private LayerMask teammateLayer;
        [Foldout("Spawn Mechanics"), SerializeField] private LayerMask playerLayer;

        #endregion


        private void Start()
        {
            StartCoroutine(SpawnTeamMate());
        }

        private void TeamMateSpawnAction()
        {
            float minX = Mathf.Min(northWestBorder.position.x, southWestBorder.position.x);
            float maxX = Mathf.Max(northEastBorder.position.x, southEastBorder.position.x);

            float minY = Mathf.Min(southWestBorder.position.y, southEastBorder.position.y);
            float maxY = Mathf.Max(northWestBorder.position.y, northEastBorder.position.y);

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            int maxAttempts = 10;

            for (int i = 0; i < maxAttempts; i++)
            {
                Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);

                if (IsSpawnPointValid(spawnPoint))
                {
                    ObjectPoolingManager.Instance.Get("TeamMate", spawnPoint);
                    break;
                }
            }

            
        }

        private bool IsSpawnPointValid(Vector2 spawnPoint)
        {
            Collider2D[] closeToOthers = Physics2D.OverlapCircleAll(spawnPoint, minDistanceFromOthers, teammateLayer);
            if (closeToOthers.Length > 0)
                return false;
            
            Collider2D[] closeToPlayer = Physics2D.OverlapCircleAll(spawnPoint, minDistanceFromPlayer, playerLayer);
            if (closeToPlayer.Length > 0)
                return false;
            
            return true;
        }

        IEnumerator SpawnTeamMate()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                TeamMateSpawnAction();
            }
        }
    }
}