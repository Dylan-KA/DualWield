using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter: BaseCharacter
{
    [SerializeField] private WeaponType startingWeaponRight;
    [SerializeField] private WeaponType startingWeaponLeft;
    [SerializeField] private BaseWeapon flamethrowerPrefab;
    [SerializeField] private BaseWeapon windGunPrefab;
    [SerializeField] private BaseWeapon FreezeGunPrefab;
    [SerializeField] private BaseWeapon RocketGunPrefab;
    [SerializeField] private Image UIPlayerHealth;
    public BaseWeapon leftWeapon { get; private set; }
    public BaseWeapon rightWeapon { get; private set; }
    
    public Camera playerCamera;
    public float gravity = 10f;
    private Vector3 velocity;
    private float maxFallingSpeed = -50.0f;

    public float lookspeed = 2f;
    public float lookXLimit = 90f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    public bool canMove = true;
    public bool isFlying { private get; set; }

    public CharacterController characterController;

    [SerializeField] private LayerMask groundlayer;
    private bool isGrounded;
    
    protected override void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EquipWeapon(startingWeaponRight, Hand.Right);
        EquipWeapon(startingWeaponLeft, Hand.Left);
    }

    public void SetMouseLocked(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        //Update red screen
        float newTransparency = (1 - (health / 100));
        Color newColor = new Color(
        UIPlayerHealth.color.r,
        UIPlayerHealth.color.g,
        UIPlayerHealth.color.b,
        newTransparency
        );
        UIPlayerHealth.color = newColor;
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
            case WeaponType.FreezeGun:
                weapon = Instantiate(FreezeGunPrefab, playerCamera.transform);
                break;
            case WeaponType.RocketLauncher:
                weapon = Instantiate(RocketGunPrefab, playerCamera.transform);
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
        moveDirection = (forward * (baseMovementSpeed * Input.GetAxis("Vertical"))) + (right * (baseMovementSpeed * Input.GetAxis("Horizontal")));
        IsGrounded();
        if (!isFlying)
        {
            if (!isGrounded)
            {
                velocity.y -= gravity * Time.deltaTime;

                // limit to maximum falling speed 
                if (velocity.y <= maxFallingSpeed)
                {
                    velocity.y = maxFallingSpeed;
                }
            }
            else
            {
                velocity.y = 0;
            }

        }

        characterController.Move(velocity * Time.deltaTime);
        characterController.Move(moveDirection * Time.deltaTime);
        if (canMove && Cursor.visible == false)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookspeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookspeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //May need to implement a cooldown for this.
            Collider[] colliders =  Physics.OverlapSphere(transform.position, 3, LayerMask.GetMask("Enemy"));
            foreach (Collider EnemyCollider in colliders)
            {
                Vector3 directionFromPlayer = EnemyCollider.transform.position - transform.position;
                directionFromPlayer.Normalize();
                EnemyCollider.GetComponent<Rigidbody>().AddForce(50f * directionFromPlayer, ForceMode.Impulse);
                GameManager.Instance.RefillAmmoCustom(5);
            }
        }

        // firing
        if (Input.GetKeyDown(KeyCode.Mouse0)) //True on first frame of mouse click
        {
            //Debug.Log("Firing");
            leftWeapon.SetFiring(true);
            rightWeapon.SetFiring(true);
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) //True on first frame of mouse release
        {
            //Debug.Log("Not Firing");
            leftWeapon.SetFiring(false);
            rightWeapon.SetFiring(false);
            isFlying = false;
        }
        else
        {
            isFlying = false;
        }

    }

    public void AddFlyingForce(float force)
    {
        isFlying = true;
        velocity.y = force;
    }

    public void IsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2, groundlayer);
    }

}