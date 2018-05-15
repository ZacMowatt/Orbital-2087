﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostTimerController : MonoBehaviour 
{
    public Slider fireRateTimer;
    public Slider shieldTimer;
    public Slider speedTimer;

    private float fireRateDuration = 0;
    private float shieldDuration = 0;    
    private float speedDuration = 0;

    void Start()
    {
        fireRateTimer.gameObject.SetActive(false);
        shieldTimer.gameObject.SetActive(false);
        speedTimer.gameObject.SetActive(false);
    }

    public void SetFireRateTimer(float duration)
    {
        fireRateDuration = duration;
    }

    public void SetShieldTimer(float duration)
    {
        shieldDuration += duration;
    }

    public void SetSpeedDuration(float duration)
    {
        speedDuration += duration;
    }

    public void UpdateFireRateTimer(float timeLeft)
    {
        fireRateTimer.value = timeLeft / fireRateDuration;

        if(timeLeft <= 0)
        {
            fireRateDuration = 0;
            fireRateTimer.gameObject.SetActive(false);
        }
        else
        {
            fireRateTimer.gameObject.SetActive(true);
        }
    }

    public void UpdateShieldTimer(float timeLeft)
    {
        shieldTimer.value = timeLeft / shieldDuration;

        if(timeLeft <= 0)
        {
            shieldDuration = 0;
            shieldTimer.gameObject.SetActive(false);
        }
        else
        {
            shieldTimer.gameObject.SetActive(true);
        }
        
    }

    public void UpdateSpeedTimer(float timeLeft)
    {
        speedTimer.value = timeLeft / speedDuration;

        if(timeLeft <= 0)
        {
            speedDuration = 0;
            speedTimer.gameObject.SetActive(false);
        }
        else
        {
            speedTimer.gameObject.SetActive(true);
        }
    }
}