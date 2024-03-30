using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter: BaseCharacter
{
    [SerializeField] private WeaponType startingWeaponRight;
    [SerializeField] private WeaponType startingWeaponLeft;
    [SerializeField] private BaseWeapon flamethrowerPrefab;
    [SerializeField] private BaseWeapon windGunPrefab;
    private BaseWeapon leftWeapon;
    private BaseWeapon rightWeapon;
    
    public Camera playerCamera;
    public float gravity = 10f;
    private Vector3 Velocity;

    public float lookspeed = 2f;
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    public bool canMove = true;

    public CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EquipWeapon(startingWeaponRight, Hand.Right);
        EquipWeapon(startingWeaponLeft, Hand.Left);
    }
    
    /// <summary>
    /// Equips a weapon in the specified hand
    /// </summary>
    public void EquipWeapon(WeaponType weaponType, Hand hand)
    {
        // TODO: destroy the previous weapon held in this hand
        if (leftWeapon && hand == Hand.Left)
            Destroy(leftWeapon.gameObject);
        else if (rightWeapon && hand == Hand.Right)
            Destroy(rightWeapon.gameObject);
        BaseWeapon weapon = null;

        switch (weaponType)
        {
            case WeaponType.Flamethrower:
                weapon = Instantiate(flamethrowerPrefab, playerCamera.transform);
                
                break;
            case WeaponType.WindGun:
                weapon = Instantiate(windGunPrefab, playerCamera.transform);
                break;
            default:
                Debug.LogError("Unknown weapon type! Add it to this switch statement or something");
                break;

        }

        if (hand == Hand.Left)
            leftWeapon = weapon;
        else
            rightWeapon = weapon;
        weapon.SetHand(hand);

        if (leftWeapon && rightWeapon)
        {
            leftWeapon.SetOtherWeaponType(rightWeapon.GetWeaponType());
            rightWeapon.SetOtherWeaponType(leftWeapon.GetWeaponType());
        }

       
        
        
    }


    
    void Update()
    {
        // movement and camera
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        moveDirection = (forward * (movementSpeed * Input.GetAxis("Vertical"))) + (right * (movementSpeed * Input.GetAxis("Horizontal")));
        Velocity.y -= gravity * Time.deltaTime;
        characterController.Move(Velocity * Time.deltaTime);
        characterController.Move(moveDirection * Time.deltaTime);
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookspeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookspeed, 0);
        }

        // firing
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Firing");
            leftWeapon.SetFiring(true);
            rightWeapon.SetFiring(true);
        } else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Debug.Log("Not Firing");
            leftWeapon.SetFiring(false);
            rightWeapon.SetFiring(false);
        }
    }   
}