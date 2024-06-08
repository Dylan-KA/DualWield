using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGun : ParticleWeapon
{
    private Camera playerCamera;
    private PlayerCharacter player;
    private float maxCameraXRotation = 90;
    [SerializeField] private float baseWindPower = 0.5f;
    [SerializeField] private float windAndWindMultiplier = 10f;
    private float flyingForce = 10;

    protected override void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerCharacter>();
        playerCamera = FindObjectOfType<Camera>();
        //Due to human error/more consistency to register
        maxCameraXRotation = player.lookXLimit - 1;
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
        base.Fire();

        ManagePlayerFlying();
        switch (otherWeaponType)
        {
            case WeaponType.Flamethrower:
                WindAndFlame();
                break;
            case WeaponType.FreezeGun:
                WindAndFreeze();
                break;
            case WeaponType.RocketLauncher:
                WindAndRocket();
                break;
            case WeaponType.WindGun:
                WindAndWind();
                break;
        }
            
    }

    public override void SetOtherWeaponType(WeaponType otherWeaponType)
    {
        base.SetOtherWeaponType(otherWeaponType);
        //Sets Wind gun to blue wind if other gun is freeze
        if (otherWeaponType == WeaponType.FreezeGun)
        {
            base.SetParticleRangeExtended();
        }
        else
        {
            base.SetParticleRangeNormal();
        }
    }

    private void WindAndFreeze()
    {
        foreach (BaseEnemy Enemy in ListofEnemies)
        {
            Enemy.AddFreezePercent(2.5f);
            if (Enemy.GetStatueEffect() == StatusEffect.Freeze)
            {
                PushEnemiesInRange(25f);
            }
        }
        PushEnemiesInRange(baseWindPower);
    }

    private void WindAndRocket()
    {
        
    }
    private void WindAndWind()
    {
        PushEnemiesInRange(windAndWindMultiplier);
    }
    private void WindAndFlame()
    {
        // TODO: implement this
        PushEnemiesInRange(baseWindPower);
    }
    private void PushEnemiesInRange(float multiplier)
    {
        if (ListofEnemies.Length != 0)
        {
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                if (Enemy == null) { return; }

                Rigidbody enemyRb = Enemy.gameObject.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 directionFromPlayer = Enemy.gameObject.transform.position - player.gameObject.transform.position;
                    directionFromPlayer.Normalize();
                    enemyRb.AddForce(multiplier * baseWindPower * directionFromPlayer, ForceMode.Impulse);
                    try
                    {
                        Enemy.GetComponent<GroundEnemy>().PushEnemy();
                    }
                    catch
                    {
                        Debug.Log("Enemy Pushed error");
                    }
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
        float adjustedRotationX = playerCamera.transform.eulerAngles.x > 180 ? playerCamera.transform.eulerAngles.x - 360 : playerCamera.transform.eulerAngles.x;
        return adjustedRotationX >= maxCameraXRotation-40; //Not exactly straight down (40) so the player can see better and use hover more easily.
    }
}
