using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : BaseCharacter
{
    [SerializeField] private WeaponType startingWeaponRight;
    [SerializeField] private WeaponType startingWeaponLeft;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private BaseWeapon flamethrowerPrefab;
    [SerializeField] private BaseWeapon windGunPrefab;
    [SerializeField] private BaseWeapon FreezeGunPrefab;
    [SerializeField] private BaseWeapon RocketGunPrefab;
    [SerializeField] private Image UIPlayerHealth;
    [SerializeField] private GameObject playerCanvas;

    public BaseWeapon leftWeapon { get; private set; }
    public BaseWeapon rightWeapon { get; private set; }
    public bool isFlying { private get; set; }
    public CharacterController characterController;
    public Camera playerCamera;
    public bool canMove = true;
    public float gravity = 10f;
    public float lookspeed = 2f;
    public float lookXLimit = 90f;

    private Coroutine healthRegenCoroutine;
    private Vector3 velocity;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    private float maxFallingSpeed = -50.0f;
    private float rotationX = 0;
    private float cooldownTime = 2f;
    private float lastUsedTime;
    private float healthRegenDelay = 5f;
    private float healthRegenRate = 10f;

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
        UpdatePlayerRedScreen();
        TriggerHealthRegen();
    }

    private void UpdatePlayerRedScreen()
    {
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
        if (IsPlayerDead())
        {
            playerCanvas.SetActive(false);
        }

        // movement and camera
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        moveDirection = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal"));

        if (moveDirection.magnitude < 1)
        {
            moveDirection *=  baseMovementSpeed;
        }
        else
        {
            moveDirection = moveDirection.normalized * baseMovementSpeed;
        }
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
            if (Time.time - lastUsedTime >= cooldownTime)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 3, LayerMask.GetMask("Enemy"));
                foreach (Collider EnemyCollider in colliders)
                {
                    Vector3 directionFromPlayer = EnemyCollider.transform.position - transform.position;
                    directionFromPlayer.Normalize();
                    EnemyCollider.GetComponent<Rigidbody>().AddForce(50f * directionFromPlayer, ForceMode.Impulse);
                    GameManager.Instance.RefillAmmoCustom(5);

                }

                lastUsedTime = Time.time;
            }
        }

        // firing
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) // stop firing when swapping weapons
        {
            leftWeapon.SetFiring(false);
            rightWeapon.SetFiring(false);
            isFlying = false;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0)) //True on first frame of mouse click
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

    public bool IsPlayerDead()
    {
        return health <= 0;
    }

    public void AddFlyingForce(float force)
    {
        isFlying = true;
        velocity.y = force;
    }

    public void IsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundlayer);
    }

    private void TriggerHealthRegen()
    {
        if (healthRegenCoroutine != null)
        {
            StopCoroutine(healthRegenCoroutine);
        }
        healthRegenCoroutine = StartCoroutine(RegenerateHealth());
    }

    IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(healthRegenDelay);

        while (health < maxHealth)
        {
            health += healthRegenRate * Time.deltaTime;
            health = Mathf.Min(health, maxHealth);
            UpdatePlayerRedScreen();
            yield return null;
        }
        healthRegenCoroutine = null;
    }
}