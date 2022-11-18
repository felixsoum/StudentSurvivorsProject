using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSpawner : BaseWeapon
{
    [SerializeField] GameObject scythePrefab;
    [SerializeField] SimpleObjectPool scythePool;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < level * 20f; i++)
            {
                float randomAngle = Random.Range(0, 360f);
                Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
                //Instantiate(scythePrefab, transform.position, rotation);

                GameObject scythe = scythePool.Get();
                scythe.transform.position = transform.position;
                scythe.transform.rotation = rotation;
                scythe.SetActive(true);
            }
        }
    }
}
