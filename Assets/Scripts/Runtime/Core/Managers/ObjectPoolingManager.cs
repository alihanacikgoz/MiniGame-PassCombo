using System;
using System.Collections.Generic;
using Runtime.ScriptableObjects;
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

        private void Start()
        {
            CreatingPoolItems();
        }

        private void CreatingPoolItems()
        {
            poolItems = new List<GameObject>();
            foreach (PoolItem poolItem in teammates)
            {
                for (int i = 0; i < poolItem.amount; i++)
                {
                    GameObject go = Instantiate(poolItem.teammate.teammatePrefab, poolParent.transform);
                    go.SetActive(false);
                    poolItems.Add(go);
                }
            }
        }

        public GameObject Get(string id)
        {
            for (int i = 0; i < poolItems.Count; i++)
            {
                if (!poolItems[i].activeInHierarchy && poolItems[i].CompareTag(id) )
                {
                    
                    return poolItems[i];
                }
            }

            foreach (PoolItem item in teammates)
            {
                if (item.teammate.teammatePrefab.CompareTag(id) && item.amount > 0)
                {
                    GameObject obj = Instantiate(item.teammate.teammatePrefab, poolParent.transform);
                    obj.SetActive(false);
                    poolItems.Add(obj);
                    return obj;
                }
            }
            return null;
        }
    }
}