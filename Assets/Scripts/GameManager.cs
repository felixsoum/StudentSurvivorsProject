using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject merman;
    [SerializeField] GameObject zombie;
    [SerializeField] GameObject vampire;
    [SerializeField] GameObject giant;
    [SerializeField] GameObject player;

    int spawnCounter = 1;

    private void Update()
    {
        int seconds = (int)Time.time;
        timerText.text = "00:" + seconds;
    }

    private void Start()
    {
        if (TitleManager.saveData == null)
        {
            TitleManager.saveData = new SaveData();
        }

        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SpawnEnemies(giant, 2);
        //SpawnEnemies(zombie, 5);
        //yield return new WaitForSeconds(5f);
        //SpawnEnemies(merman, 5, isChasing: false);
        //yield return new WaitForSeconds(5f);
        //SpawnEnemies(vampire, 1);
        //yield return new WaitForSeconds(15f);
        //SpawnEnemies(zombie, 5);
        //SpawnEnemies(merman, 5);
        //yield return new WaitForSeconds(5f);

        //while (true)
        //{
        //    SpawnEnemies(merman, 10 * spawnCounter++);
        //    yield return new WaitForSeconds(10f);
        //}
    }

    void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies, bool isChasing = true)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * 8;
            spawnPosition += player.transform.position;
            GameObject go = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Enemy enemy = go.GetComponent<Enemy>();
            enemy.isChasing = isChasing;
        }
    }
}
