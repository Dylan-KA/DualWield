using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGun : ParticleWeapon
{
    private Camera playerCamera;
    private PlayerCharacter player;
    private float maxCameraXRotation = 44;
    private float windPower = 3;
    private float windMultiplier = 2;
    private float flyingForce = 5;

    protected override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Due to human error/more consistency to register
        maxCameraXRotation = player.lookXLimit-1;
    }

    protected override void Update()
    {
        base.Update();
    }
    
    /// <summary>
    /// Shoots the gun, if possible
    /// </summary>
    public override void Fire()
    {
        ManagePlayerFlying();
        if (otherWeaponType == WeaponType.WindGun)
        {
            WindAndWind();
        }
            
        else if (otherWeaponType == WeaponType.Flamethrower)
        {
            WindAndFlame();
        }
            
    }

    private void WindAndWind()
    {
        PushEnemiesInRange(windMultiplier);
    }
    private void WindAndFlame()
    {
        // TODO: implement this
    }
    private void PushEnemiesInRange()
    {
        if (ListofEnemies.Length != 0)
        {
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.gameObject.GetComponent<Rigidbody>().AddRelativeForce(gameObject.transform.position * windPower);
            }
        }
    }
    private void PushEnemiesInRange(float windMultiplier)
    {
        if (ListofEnemies.Length != 0)
        {
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.gameObject.GetComponent<Rigidbody>().AddRelativeForce(gameObject.transform.position * windPower * windMultiplier);
            }
        }
    }
    private void ManagePlayerFlying()
    {
        if (playerCamera == null) { Debug.Log("PlayerCamera is missing in 'WindGun'"); return; }
        if (IsPlayerLookingStraightDown())
        {
            player.AddFlyingForce(flyingForce);
        }
        else
        {
            player.isFlying = false;
        }
    }
    private bool IsPlayerLookingStraightDown()
    {
        float adjustedRotationX = playerCamera.transform.localEulerAngles.x > 180 ? playerCamera.transform.localEulerAngles.x - 360 : playerCamera.transform.localEulerAngles.x;
        return adjustedRotationX >= maxCameraXRotation;
    }
}
