using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected Vector3 handOffset;
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] AudioSource weaponEmptySound;
    [SerializeField] AudioSource weaponFireSound; 
    [SerializeField] AudioSource weaponFiringSound;
    protected WeaponType otherWeaponType;
    protected Hand hand;
    protected bool isFiring = false;
    public void SetFiring(bool bIsFiring) { isFiring = bIsFiring; }

    protected virtual void Start()
    {
        
        PositionGun();
    }

    protected virtual void Update()
    {
        if (isFiring)
            if (GameManager.Instance.GetAmmo() > 0)
                Fire();
            else
                SetFiring(false);
        //PositionGun(); // uncomment this temporarily if u want to find a good handOffset
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public void SetHand(Hand hand)
    {
        this.hand = hand;
    }

    public virtual void SetOtherWeaponType(WeaponType otherWeaponType)
    {
        this.otherWeaponType = otherWeaponType;
    }

    // Puts the gun into a good position
    private void PositionGun()
    {
        Vector3 multiplier = hand == Hand.Right ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        handOffset.Scale(multiplier);
        gameObject.transform.localPosition = handOffset;
    }

    public abstract void Fire();

    public void PlayWeaponEmptySFX()
    {
        weaponEmptySound.Play();
    }

    public void PlayWeaponFireSFX()
    {
        weaponFireSound.Play();
        if (weaponFiringSound != null)
        {
            PlayWeaponFiringSFX();
        }
    }

    public void PlayWeaponFiringSFX()
    {
        weaponFiringSound.Play();
    }

    public void ClearWeaponSFX()
    {
        weaponFireSound.Stop();
        weaponFiringSound.Stop();
    }
}
