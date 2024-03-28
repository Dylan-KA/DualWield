using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected Vector3 handOffset;
    protected WeaponType weaponType;
    protected WeaponType otherWeaponType;
    protected Hand hand;

    protected virtual void Start()
    {
        PositionGun();
    }

    protected virtual void Update()
    {
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

    public void SetOtherWeaponType(WeaponType otherWeaponType)
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
}
