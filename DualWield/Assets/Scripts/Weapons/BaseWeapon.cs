using System.Collections;
using System.Collections.Generic;
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
        //PositionGun(); // uncomment this temporarily if u want to find a good handOffset
        if (isFiring)
        {
            if (GameManager.Instance.GetAmmo() > 0)
                Fire();
            else
            {
                SetFiring(false);
                ClearWeaponSFX();
            }
        }

        // Weapon Audio 
        if (Input.GetKeyUp(KeyCode.Mouse0)) //First frame of mouse release
        {
            ClearWeaponSFX();
        }

        // Prevents duplicate audio playing if both guns are the same type
        if (hand == Hand.Right && weaponType == otherWeaponType)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //First frame of mouse click
        {
            if (GameManager.Instance.GetAmmo() > 0)
            {
                PlayWeaponFireSFX();
            } else
            {
                PlayWeaponEmptySFX();
            }
        }

        //Don't add code below here, as update may be returned early.
        //Instead add above all audio code.

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

    // This is the start of a weapon's fire audio (which does not loop)
    public void PlayWeaponFireSFX()
    {
        weaponFireSound.Play();
        if (weaponFiringSound != null)
        {
            PlayWeaponFiringSFX();
        }
    }

    // This is the looping part of a weapon's firing audio
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
