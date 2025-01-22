using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolObjects : MonoBehaviour
{
    [SerializeField] public List<GameObject> PooledObjects = new List<GameObject>();
    private GameObject ObjectToPool;
    private int AmountToPool;
    public bool AllowCreation = false;
    private double LimitToReturn;
    
    public void InitPool(GameObject prefab, int amount, int limit) {
        ObjectToPool = prefab;
        AmountToPool = amount;
        LimitToReturn = limit;

        for (int i = 0; i < AmountToPool; i++)
        {
            PooledObjects.Add(CreateObject());
        }
    }

    public void SetLimitToReturn(double value)
    {
        if (value > AmountToPool) {
            return;
        }

        LimitToReturn = value;

    }
    
    public GameObject CreateObject(bool active = false)
    {
        GameObject obj;
        obj = Instantiate(ObjectToPool);
        obj.transform.SetParent(gameObject.transform);
        obj.SetActive(active);
        return obj;
        
    }
    public GameObject GetObject()
    {
        for (int i = 0; i < LimitToReturn; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }

        if (AllowCreation)
        {
            GameObject obj = CreateObject(true);
            PooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}



