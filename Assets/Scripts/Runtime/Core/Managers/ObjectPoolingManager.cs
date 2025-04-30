using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Runtime.Core.Controllers;
using Runtime.ScriptableObjects;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Core.Managers
{
    [Serializable]
    public class PoolItem
    {
        public TeammateObjects teammate;
        public int amount;
        public bool trueToPass;
        public int pointsWillGive;
        public bool isRare;
    }

    public class ObjectPoolingManager : MonoBehaviour
    {
        #region Singleton

        public static ObjectPoolingManager Instance { get; private set; }

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

        public List<PoolItem> teammates;
        public List<GameObject> poolItems;
        public Transform poolParent;
        public float OFFSET;

        private GameObject go;

        [Foldout("Player"), SerializeField] private Transform playerTransform;

        private void OnEnable()
        {
            CoreGameSignals.Instance.OnTeammateSpawnAction += CreatingPoolItems;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnTeammateSpawnAction -= CreatingPoolItems;
        }


        private void CreatingPoolItems()
        {
            poolItems = new List<GameObject>();


            foreach (PoolItem poolItem in teammates)
            {
                for (int i = 0; i < poolItem.amount; i++)
                {
                    go = Instantiate(poolItem.teammate.teammatePrefab, poolParent.transform);

                    go.GetComponent<TeamMateController>().teammateOptions = poolItem;
                    go.SetActive(false);
                    poolItems.Add(go);
                }
            }
        }

        public GameObject Get(string id, Vector3 spawnPoint)
        {
            for (int i = 0; i < poolItems.Count; i++)
            {
                if (!poolItems[i].activeInHierarchy && poolItems[i].CompareTag(id))
                {
                    poolItems[i].transform.position = spawnPoint;

                    // ROTATION EKLENDİ
                    Vector3 directionToPlayer = playerTransform.position - spawnPoint;
                    float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                    poolItems[i].transform.rotation = Quaternion.Euler(0f, 0f, angle + OFFSET);

                    poolItems[i].SetActive(true);
                    return poolItems[i];
                }
            }

            foreach (PoolItem item in teammates)
            {
                if (item.teammate.teammatePrefab.CompareTag(id))
                {
                    GameObject obj = Instantiate(item.teammate.teammatePrefab, poolParent.transform);
                    obj.transform.position = spawnPoint;

                    // ROTATION EKLENDİ
                    Vector3 directionToPlayer = playerTransform.position - spawnPoint;
                    float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                    obj.transform.rotation = Quaternion.Euler(0f, 0f, angle + OFFSET);

                    obj.SetActive(false);
                    poolItems.Add(obj);
                    return obj;
                }
            }

            return null;
        }
    }
}