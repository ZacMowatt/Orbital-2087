﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoost : MonoBehaviour 
{
    private const float DURATION = 10f;
    private const float MULTIPLIER = 2;
    private const float MOVE_SPEED = 30;

    ShootProjectile playerWeapon;

    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootProjectile>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.timeScale / MOVE_SPEED);
    }

    public void Activate()
    {
        playerWeapon.ActivateFireRateBoost(DURATION, MULTIPLIER);
    }
}