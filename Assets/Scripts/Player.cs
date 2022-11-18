using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] BaseWeapon[] weapons;
    [SerializeField] GameObject levelUpMenu;

    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isInvincible;

    internal int playerHP;
    [SerializeField] internal int playerMaxHP = 3;

    [SerializeField] PlayerCamera playerCamera;

    // Make a material field
    Material material;

    public float GetHPRatio()
    {
        return (float)playerHP / playerMaxHP;
    }

    int currentExp;
    int expToLevel = 5;
    int currentLevel;

    internal Action<int, int> OnExpGained;

    public void LevelUpKatana()
    {
        weapons[0].LevelUp();
        Time.timeScale = 1;
        levelUpMenu.SetActive(false);
    }

    internal void AddExp()
    {
        currentExp++;

        if (currentExp >= expToLevel)
        {
            currentExp = 0;
            expToLevel += 5;
            currentLevel++;

            // I want to pick a random weapon and level it up
            int maxWeapons = weapons.Length; 

            // for 4 weapons, the indices are 0, 1, 2, 3
            int randomWeaponIndex = UnityEngine.Random.Range(0, maxWeapons);
            weapons[randomWeaponIndex].LevelUp();

            Time.timeScale = 0;
            levelUpMenu.SetActive(true);
        }

        OnExpGained?.Invoke(currentExp, expToLevel);
    }

    private void Start()
    {
        weapons[1].LevelUp();

        playerHP = playerMaxHP;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerCamera.size = 2000;

        // Assign the material from the sprite renderer to your field
        material = spriteRenderer.material;
    }

    internal void OnDamage()
    {
        if (!isInvincible)
        {
            if (playerCamera)
            {
                playerCamera.Shake(); 
            }

            StartCoroutine(InvincibilityCoroutine());

            if (--playerHP <= 0)
            {
                // increase the player's death count
                TitleManager.saveData.deathCount++;
                SceneManager.LoadScene("Title");
            }
        }
    }

    public void DamageCalculation1() { }
    public void DamageCalculation2() { }
    public void DamageCalculation3() { }
    public void DamageCalculation4() { }
    public void DamageCalculation5() { }
    public void DamageCalculation6() { }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        //spriteRenderer.color = Color.red;
        material.SetFloat("_Flash", 0.5f);
        yield return new WaitForSeconds(0.1f);
        //spriteRenderer.color = Color.white;
        material.SetFloat("_Flash", 0);

        isInvincible = false;
    }

    void Update()
    {
        isInvincible = false;
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        float delta = speed * Time.deltaTime;
        transform.position += new Vector3(inputX, inputY) * delta;

        if (inputX != 0)
        {
            transform.localScale = new Vector3(inputX > 0 ? -1 : 1, 1, 1);
        }

        animator.SetBool("IsRunning", inputX != 0 || inputY != 0);
    }
}
