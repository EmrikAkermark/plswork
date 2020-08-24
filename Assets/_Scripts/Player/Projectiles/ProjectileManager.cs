using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Player.Projectiles
{
    [Serializable]
    public class PoolableProjectile
    {
        public string projTag;
        public GameObject prefab;
        public int initSize;
        public bool shouldExpand;
    }

    public class ProjectileManager : MonoBehaviour
    {
        public static ProjectileManager Instance;

        public List<PoolableProjectile> poolableProjectiles;
        public List<GameObject> pool;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            pool = new List<GameObject>();
            foreach (var poolableProjectile in poolableProjectiles)
            {
                for (int i = 0;
                    i < poolableProjectile.initSize;
                    i++)
                {
                    GameObject obj = (GameObject) Instantiate(poolableProjectile.prefab);
                    obj.SetActive(false);
                    obj.tag = poolableProjectile.projTag;
                    pool.Add(obj);
                }  
            }
        }

        // @WARNING: Can return null
        public GameObject GetWithTag(string projTag)
        {
            // Try getting an inactive object from the pool
            foreach (var obj in pool)
            {
                if (!obj.activeInHierarchy && obj.CompareTag(projTag))
                    return obj;
            }

            // Try growing the pool
            foreach (var poolableProjectile in poolableProjectiles)
            {
                //@TODO: How does Unity handle hashing here?
                if (poolableProjectile.projTag == projTag && poolableProjectile.shouldExpand)
                {
                    GameObject obj = (GameObject) Instantiate(poolableProjectile.prefab);
                    obj.SetActive(false);
                    obj.tag = projTag;
                    pool.Add(obj);
                    return obj;
                }
            }
            // Give up.
            return null;
        }
    }
}
