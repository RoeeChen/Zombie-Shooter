﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon {

    private bool fireActive = false;
    protected float rangeRatioIncrease = 0f;
    protected float rangeRatioToIncrease = 0.01f;

    void Start()
    {
        weaponName = "Rifle";
        cooldownTime = 0.1f;
        ammoOnStack = 50;
        shotsAmount = 1;
        reloadTime = 2;
        SetAmmo(128, 32);
        rangeRatio = 0.08f;
    }

    public override void Fire()
    {
        fireActive = true;
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && ammoOnStack >= shotsAmount && fireActive && playerAttack.GetWeapon().Equals(this))
        {
            if (!onCoolddown)
            {
                AudioSource.PlayClipAtPoint(gunfire, this.transform.position);
                Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0.5f), transform.rotation).CreateBullet(this ,new Vector2(rangeRatio + rangeRatioIncrease, rangeRatio + rangeRatioIncrease), true);
                AmmoChange(-shotsAmount);
                rangeRatioIncrease += 0.02f;
                Cooldwon();
            }
        }
        else
        {
            fireActive = false;
            rangeRatioIncrease = 0f;
        }
    }
}
