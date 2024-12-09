using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PoolController : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string poolTag;
            public GameObject objPrefab;
            public int poolSize;
            public int increaseAmount;
        }

        public List<Pool> poolList;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Awake()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in poolList)
            {
                Queue<GameObject> objPool = new Queue<GameObject>();

                for (int i = 0; i < pool.poolSize; i++)
                {
                    GameObject obj = Instantiate(pool.objPrefab);
                    obj.SetActive(false);
                    objPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.poolTag, objPool);
            }
        }

        public GameObject OnGetObjFromPool(string poolTag)
        {
            if (!poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogError($"Pool with tag {poolTag} does not exist.");
                return null;
            }

            if (poolDictionary[poolTag].Count == 0)
            {
                IncreasePoolSize(poolTag);
            }

            GameObject obj = poolDictionary[poolTag].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void OnObjReturnToPool(string poolTag, GameObject obj)
        {
            if (!poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogError($"Pool with tag {poolTag} does not exist.");
                return;
            }

            obj.SetActive(false);
            poolDictionary[poolTag].Enqueue(obj);
        }

        private void IncreasePoolSize(string poolTag)
        {
            Pool pool = poolList.Find(p => p.poolTag == poolTag);

            if (pool == null)
            {
                Debug.LogError($"No pool found with tag {poolTag}.");
                return;
            }

            for (int i = 0; i < pool.increaseAmount; i++)
            {
                GameObject obj = Instantiate(pool.objPrefab);
                obj.SetActive(false);
                poolDictionary[poolTag].Enqueue(obj);
            }
        }
    }
}
