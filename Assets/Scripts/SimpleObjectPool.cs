using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    List<GameObject> pooledObjects = new List<GameObject>();
    int objectIndex;

    private void Awake()
    {
        for (int i = 0; i < 1000; i++)
        {
            pooledObjects.Add(Instantiate(objectPrefab));
        }
    }

    public GameObject Get()
    {
        objectIndex %= pooledObjects.Count;
        return pooledObjects[objectIndex++];
    }
}
