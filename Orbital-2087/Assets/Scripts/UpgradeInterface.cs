﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInterface : MonoBehaviour 
{
    private int fireRateLevel = 0;
    private int damageLevel = 0;
    private int maxHealthLevel = 0;

    private float baseFireRate = 2;
    private float baseDamage = 1;
    private float baseHealth = 100;

    private float fireRateLevelIncrease = .15f;
    private float damageLevelIncrease = .6f;
    private float maxHealthLevelIncrease = 15;

    private float levelCostMultiplier = 2f;

    PlayerHealth playerHealth;
    ShootProjectile playerWeapon;
    ScoreKeeping scoreKeeping;
    EarthHealth earthHealth;

    UpgradeMenu upgradeMenu;
    private float fireRateUpgradeCost, damageUpgradeCost, healthUpgradeCost;
    private float healingCost = 5f; // default healing cost for the earth & player

    void Start()
    {
        GameObject earth = GameObject.FindGameObjectWithTag("Earth");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerHealth = player.GetComponent<PlayerHealth>();
        playerWeapon = player.GetComponent<ShootProjectile>();
        scoreKeeping = player.GetComponent<ScoreKeeping>();
        earthHealth = earth.GetComponent<EarthHealth>();
        upgradeMenu = gameObject.GetComponent<UpgradeMenu>();

        RefreshTexts();
    }

    public float GetPlayerCurrency()
    {
        return scoreKeeping.GetCash();
    }

    public void HealPlayer(float health)
    {
        if(CanAfford(healingCost))
        {
            if(playerHealth.GetHealth() < GetMaxHealth())
            {
                playerHealth.HealPlayer(health);
                DeductCost(healingCost);
                UpdateCashText();
            }
            else
            {
                Debug.Log("You are at max health");
            }
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
        UpdateCurrentHealthText();
    }

    public void HealEarth(float health)
    {
        if(CanAfford(healingCost))
        {
            if(earthHealth.GetHealth() < earthHealth.GetStartingHealth())
            {
                earthHealth.HealEarth(health);
                DeductCost(healingCost);
                UpdateCashText();
            }
            else
            {
                Debug.Log("The earth is at max health");
            }
        }
        else
        {
            Debug.Log("Not enough cash to purchase this");
        }
        UpdateCurrentHealthText();
    }

    public void UpgradeDamage()
    {
        damageUpgradeCost = GetNextLevelCost(damageLevel);
        if(CanAfford(damageUpgradeCost))
        {
            ++damageLevel;
            playerWeapon.SetDamage(GetDamage() + damageLevelIncrease);
            DeductCost(damageUpgradeCost);
            UpdateDamageText();
            UpdateCashText();
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    public void UpgradeFireRate()
    {
        fireRateUpgradeCost = GetNextLevelCost(fireRateLevel);
        if (CanAfford(fireRateUpgradeCost))
        {
            ++fireRateLevel;
            playerWeapon.SetFireRate(GetFireRate() + fireRateLevelIncrease);
            DeductCost(fireRateUpgradeCost);
            UpdateFireRateText();
            UpdateCashText();
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    public void UpgradeHealth()
    {
        healthUpgradeCost = GetNextLevelCost(maxHealthLevel);
        if(CanAfford(healthUpgradeCost))
        {
            ++maxHealthLevel;
            playerHealth.SetMaxHealth(GetMaxHealth() + maxHealthLevelIncrease);
            DeductCost(healthUpgradeCost);
            UpdateHealText();
            UpdateCashText();
        }
        else
        {
            Debug.Log("Not enough cash to purchase upgrade");
        }
    }

    private bool CanAfford(float cost)
    {
        bool canAfford = true;

        if(GetPlayerCurrency() < cost)
        {
            canAfford = false;
            //disable upgrade

        }

        return canAfford;
    }

    // can avoid this method if you just start all levels at 0
    void CalculateLevels()
    {
        fireRateLevel = (int)((GetFireRate() - baseFireRate) / fireRateLevelIncrease);
        damageLevel = (int)((GetDamage() - baseDamage) / damageLevelIncrease);
        maxHealthLevel = (int)((GetMaxHealth() - baseHealth) / maxHealthLevelIncrease);
        Debug.Log(GetMaxHealth());
        Debug.Log(baseHealth);
        Debug.Log(maxHealthLevelIncrease);
    }
    
    void UpdateHealText()
    {
        healthUpgradeCost = GetNextLevelCost(maxHealthLevel);
        upgradeMenu.UpdateMaxHealthPrice(healthUpgradeCost);
        upgradeMenu.UpdateHealthLevel(maxHealthLevel, GetMaxHealth());
        UpdateCurrentHealthText();
    }

    void UpdateDamageText()
    {
        damageUpgradeCost = GetNextLevelCost(damageLevel);
        upgradeMenu.UpdateDamagePrice(damageUpgradeCost);
        upgradeMenu.UpdateDamageLevel(damageLevel, GetDamage());
    }

    void UpdateFireRateText()
    {
        fireRateUpgradeCost = GetNextLevelCost(fireRateLevel);
        upgradeMenu.UpdateFireRatePrice(fireRateUpgradeCost);
        upgradeMenu.UpdateFireRateLevel(fireRateLevel, GetFireRate());
    }

    void UpdateCurrentHealthText()
    {
        float playerCurHealth = playerHealth.GetHealth();
        float playerMaxHealth = playerHealth.GetMaxHealth();
        float earthCurHealth = earthHealth.GetHealth();
        float earthMaxHealth = earthHealth.GetStartingHealth();

        upgradeMenu.UpdateHealthText(playerCurHealth, playerMaxHealth, earthCurHealth, earthMaxHealth);
    }

    public void RefreshTexts()
    {
        UpdateCashText();
        UpdateDamageText();
        UpdateFireRateText();
        UpdateHealText();
        UpdateCurrentHealthText();
    }

    void UpdateCashText()
    {
        upgradeMenu.UpdateCashText(GetPlayerCurrency());
    }

    public float GetFireRate()
    {
        return playerWeapon.GetFireRate();
    }

    public float GetDamage()
    {
        return playerWeapon.GetDamage();
    }

    public float GetMaxHealth()
    {
        return playerHealth.GetMaxHealth();
    }

    public float GetNextLevelCost(int currentLevel)
    {
        return (currentLevel + 1) * levelCostMultiplier;
    }

    public void DeductCost(float cost)
    {
        scoreKeeping.SetCash(scoreKeeping.GetCash() - cost);
    }
}
