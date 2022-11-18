using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool isBoss;
    [SerializeField] GameObject crystalPrefab;
    [SerializeField] float speed = 1f;
    public bool isChasing = true;

    protected GameObject player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (isBoss)
        {
            StartCoroutine(BossCameraCoroutine());
        }
    }

    IEnumerator BossCameraCoroutine()
    {
        Time.timeScale = 0;

        Camera.main.GetComponent<PlayerCamera>().target = gameObject;

        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 4;
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 3;
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 4;
        yield return new WaitForSecondsRealtime(1f);
        Camera.main.orthographicSize = 5;
        yield return new WaitForSecondsRealtime(1f);

        Camera.main.GetComponent<PlayerCamera>().target = player;

        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1;
    }

    protected virtual void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 destination = player.transform.position;
        Vector3 source = transform.position;

        Vector3 direction = destination - source;

        if (!isChasing)
        {
            direction = Vector3.left;
        }

        direction.Normalize();
        transform.position += direction * Time.deltaTime * speed;
        transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            player.OnDamage();
        }
    }

    internal void Damage(int damage)
    {
        TitleManager.saveData.goldCoins += UnityEngine.Random.Range(1, 4);
        Instantiate(crystalPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
