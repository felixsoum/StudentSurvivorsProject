using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : BaseWeapon
{
    SpriteRenderer spriteRenderer = null;
    BoxCollider2D boxCollider2D = null;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(KatanaCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        //if (!enemy)
        if (enemy != null)
        {
            enemy.Damage(level);
        }
    }

    IEnumerator KatanaCoroutine()
    {
        while (true)
        {
            transform.localScale = Vector3.one * level;
            spriteRenderer.enabled = true;
            boxCollider2D.enabled = true;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            yield return new WaitForSeconds(2f);
        }
    }
}
