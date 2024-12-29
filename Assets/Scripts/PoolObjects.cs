using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();
    public GameObject objectToPool;
    public int amountToPool;
    public bool allowCreation = false;

    private void Awake()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            pooledObjects.Add(CreateObject());
        }
    }

    public GameObject CreateObject(bool active = false)
    {
        GameObject obj;
        obj = Instantiate(objectToPool);
        obj.transform.SetParent(gameObject.transform);
        obj.SetActive(active);
        return obj;
        
    }
    public GameObject GetObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (allowCreation)
        {
            GameObject obj = CreateObject(true);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}



