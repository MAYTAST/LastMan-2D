using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{

    private Dictionary<string, List<GameObject>> pooledObjects;
    private Dictionary<string, Transform> poolParents;

    protected override void Awake()
    {
        base.Awake();

        pooledObjects = new Dictionary<string, List<GameObject>>();
        poolParents = new Dictionary<string, Transform>();
    }

    /// <summary>
    /// Use this method to check whether the pool already exsist or not
    /// </summary>
    /// <param name="prefab">prefab of which the pool we are checking</param>
    /// <returns></returns>
    public bool PoolExsist(GameObject prefab)
    {
        string key = prefab.name;

        return pooledObjects.ContainsKey(key);
    }

    /// <summary>
    /// Use this method to check whether the pool already exsist or not
    /// </summary>
    /// <param name="key">string key is required (could be prefab name)</param>
    /// <returns></returns>
    public bool PoolExsist(string key)
    {
        return poolParents.ContainsKey(key);
    }


    /// <summary>
    /// Should be called in start method of the script that is using it
    /// </summary>
    /// <param name="prefab">prefab that need to be initialized</param>
    /// <param name="size">size of the pool</param>
    /// <param name="parent">parent of the pooled objects</param>
    public void InitializePool(GameObject prefab, int size, Transform parent)
    {
        string key = prefab.name;

        if (!PoolExsist(key))
        {
            pooledObjects.Add(key, new List<GameObject>());
            poolParents.Add(key, parent);
        }

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            pooledObjects[key].Add(obj);
        }
    }

    /// <summary>
    /// Use to get the pooled object of a particular prefab.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(GameObject prefab)
    {
        string key = prefab.name;

        if (pooledObjects.ContainsKey(key))
        {
            foreach (GameObject obj in pooledObjects[key])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }
        }

        Debug.LogWarning("No available objects in the pool for: " + key);
        return null;
    }

    /// <summary>
    /// Disable the objects and set other parameters to default for fututre use.
    /// </summary>
    /// <param name="obj">game object to deactivate</param>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolParents[obj.name]);
    }

}
