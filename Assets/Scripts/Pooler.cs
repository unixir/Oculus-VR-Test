using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The parent class responsible for pooling any game object. 
/// </summary>
public class Pooler : MonoBehaviour
{
    internal Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// Returns the pool count of the object with the key if key is found or returns 0
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int PoolCount(string key)
    {
        if (pools.ContainsKey(key))
            return pools[key].Count;
        else return 0;
    }

    internal virtual void FillPool(string type, GameObject prefab, int count)
    {
        if (!pools.ContainsKey(type))
            pools[type] = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(prefab, transform);
            newObject.name = type +" " + i;
            newObject.SetActive(false);
            pools[type].Enqueue(newObject);
        }
    }
    internal virtual void FillPool(string type, GameObject prefab, int count, Transform parent)
    {
        if (!pools.ContainsKey(type))
            pools[type] = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(prefab, parent);
            newObject.name = type + " " + i;
            newObject.SetActive(false);
            pools[type].Enqueue(newObject);
        }
    }
    internal virtual GameObject InstantiateFromPool(string type, Vector3 pos, Quaternion rot, Transform parentT = null)
    {
        if (pools[type].Count > 0)
        {
            GameObject go = pools[type].Dequeue();
            go.transform.SetPositionAndRotation(pos, rot);
            go.transform.SetParent(parentT);
            go.SetActive(true);
            return go;
        }
        else return null;
    }
    internal virtual void ReturnToPool(string type, GameObject go)
    {
        go.transform.SetParent(transform);
        go.SetActive(false);
        pools[type].Enqueue(go);
    }

    internal void ClearAndDestroyPool()
    {
        foreach(KeyValuePair<string, Queue<GameObject>> pair in pools)
        {
            foreach(GameObject _obj in pair.Value)
            {
                Destroy(_obj);
            }
        }
        pools.Clear();
    }
}

/// <summary>
/// The interface that can implemented by any object that is going to be pooled.
/// </summary>
public interface IPooledObject
{
    void ReturnToPool();
}