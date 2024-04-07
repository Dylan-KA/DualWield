using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGun : ParticleWeapon
{
    private Camera playerCamera;
    private PlayerCharacter player;
    private float maxCameraXRotation = 90;
    [SerializeField] private float baseWindPower = .2f;
    [SerializeField] private float windAndWindMultiplier = 10f;
    private float flyingForce = 10;

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

    private void WindAndFreeze()
    {
        foreach (BaseEnemy Enemy in ListofEnemies)
        {
            if (Enemy.GetStatueEffect() == StatusEffect.Freeze)
            {
                PushEnemiesInRange(50f);
            }
        }
        PushEnemiesInRange(windAndWindMultiplier);
        
        
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
        return adjustedRotationX >= maxCameraXRotation-30; //Not exactly straight down (30) so the player can see better and use hover more easily.
    }
}
