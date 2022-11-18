using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public int damage = 1;
    void Update()
    {
        transform.position += transform.right * 5 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Damage(1);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
