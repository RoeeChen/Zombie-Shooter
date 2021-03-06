﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public Bullet bullet;
    protected PlayerAttack playerAttack;
    public AudioClip gunfire;
    protected int shotsAmount;
    public AudioClip reloadClip;
    private bool playing;
    public Sprite sprite;
    public AudioClip noAmmoClip;
    protected string weaponName;

    protected bool onCoolddown = false;
    protected float cooldownTime;
    bool reloading = false;
    protected float reloadTime;
    protected float rangeRatio;

    public string GetName()
    {
        return this.weaponName;
    }


    void PlayNoAmmoClip()
    {
        AudioSource.PlayClipAtPoint(noAmmoClip, this.transform.position);
    }

    public bool IsReloading()
    {
        return this.reloading;
    }

    public AudioSource GetGunFireAudioSource()
    {
        return this.playerAttack.gunFireAudioSource;
    }


    protected int ammoOnStack, stackMax, ammoMax, ammoLeft;

    public bool CanFire()
    {
        return ammoOnStack >= shotsAmount;
    }

    protected void CooldownOver()
    {
        onCoolddown = false;
    }

    protected void Cooldwon()
    {
        onCoolddown = true;
        Invoke("CooldownOver", cooldownTime);
    }

    public virtual void Fire()
    {

    }

    public void SetPlayerAttack(PlayerAttack playerAttack)
    {
        this.playerAttack = playerAttack;
    }

    public void UpdateAmmoText()
    {
        if (reloading)
            InventoryWeapon.GetText(this).text = "Reloading...";
        else InventoryWeapon.GetText(this).text = ammoOnStack + "/" + ammoLeft;
    }

    public void WaitBeforeReloadAll()
    {
        if (ammoLeft == 0)
            PlayNoAmmoClip();
        if (ammoOnStack < stackMax && ammoLeft > 0)
        {

            reloading = true;
            UpdateAmmoText();
            PlayReloadClip();
            Invoke("ReloadAll", reloadTime);
        }

        else
        {
            UpdateAmmoText();
        }
    }

    public void CancelReload()
    {
        ReloadAll();
    }


    void ReloadAll()
    {
            int bulletsToReload = stackMax - ammoOnStack;
            if (bulletsToReload <= ammoLeft)
            {
                ammoOnStack = stackMax;
                ammoLeft -= bulletsToReload;
            }
            else
            {
                ammoOnStack += ammoLeft;
                ammoLeft = 0;
            }
            reloading = false;
            UpdateAmmoText();
    }

    public void AddToAmmo(int ammo)
    {
        if (IsInvoking("ReloadAll"))
        {
            CancelInvoke("ReloadAll");
            this.ammoLeft += ammo;
            ReloadAll();
        }
        
        else this.ammoLeft += ammo;
    }

    public int GetAmmoMax()
    {
        return this.ammoMax;
    }

    public int GetStackMax()
    {
        return this.stackMax;
    }

    public int GetAmmoLeft()
    {
        return this.ammoLeft;
    }
    public void SetAmmoToMax()
    {
        this.ammoLeft = this.ammoMax - this.stackMax;
    }

   protected void SetAmmo(int ammoMax, int stackMax)
    {
        this.ammoMax = ammoMax;
        this.ammoLeft = this.ammoMax;
        this.stackMax = stackMax;
        this.ammoOnStack = this.stackMax;
        this.ammoLeft -= this.stackMax;
    }

    public void SetUpWeapon(bool alreadyWeapon)
    {
        SetAmmo(ammoMax, stackMax);
        if (alreadyWeapon)
        {
            ReloadAll();
        }
        else
        {
            playerAttack.PickupWeapon(this);
        }
        UpdateAmmoText();
        
    }

    void PlayReloadClip()
    {
        if (!playing)
        {
            this.playing = true;
            AudioSource.PlayClipAtPoint(reloadClip, this.transform.position);
            Invoke("SetNotPlaying", reloadClip.length);
        }
    }

    void SetNotPlaying()
    {
        this.playing = false;
    }

    public void AmmoChange(int ammoChange)
    {
        ammoOnStack += ammoChange;
        UpdateAmmoText();
    }
}
