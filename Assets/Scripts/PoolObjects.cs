using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private GameObject objectToPool;
    public int amountToPool;
    public bool allowCreation = false;
    private double limitToReturn = 0;


    public void SetLimitToReturn(double value)
    {
        if (value > amountToPool) {
            return;
        }

        limitToReturn = value;

    }
    public void InitPool(int amount)
    {
        amountToPool = amount;

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
        for (int i = 0; i < limitToReturn; i++)
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



