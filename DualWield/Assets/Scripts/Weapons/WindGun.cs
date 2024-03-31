using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGun : ParticleWeapon
{
    private Camera playerCamera;
    private PlayerCharacter player;
    private float maxCameraXRotation = 90;
    private float windPower = 1;
    private float windMultiplier = 1.5f;
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
    private void PushEnemiesInRange(float windMultiplier)
    {
        if (ListofEnemies.Length != 0)
        {
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Rigidbody enemyRb = Enemy.gameObject.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 directionFromPlayer = Enemy.gameObject.transform.position - player.gameObject.transform.position;
                    directionFromPlayer.Normalize();
                    enemyRb.AddForce(windMultiplier * windPower * directionFromPlayer, ForceMode.Impulse);
                }
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
