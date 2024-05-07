using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : GroundEnemy
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float maxThrowForce = 50f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        transform.LookAt(playerTransform);
        StopEnemyMovement();
        ThrowProjectile();
        ResetAttackWaitTime();
    }
    private void ThrowProjectile()
    {
        GameObject bullet = Instantiate(projectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        Debug.Log(playerTransform.gameObject.name);
        bullet.GetComponent<Rigidbody>().velocity = 
            GetPredictedPosition(playerTransform.position + playerTransform.gameObject.GetComponent<CharacterController>().center, bullet.GetComponent<Transform>().position);
        Debug.Log(bullet.GetComponent<Rigidbody>().velocity);
    }

    private ThrowData CalculateInitalVelocity(Vector3 targetPosition, Vector3 startPosition)
    {
        Vector3 displacement = new Vector3(targetPosition.x, startPosition.y, targetPosition.z) - startPosition;
        float deltaY = targetPosition.y - startPosition.y;
        float deltaXZ = displacement.magnitude;
        float gravity = Mathf.Abs(Physics.gravity.y);
        float throwStrength = Mathf.Clamp(Mathf.Sqrt(gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXZ, 2)))), 0.01f, maxThrowForce);
        float angle = Mathf.PI / 2f - (0.5f * Mathf.PI / 2 - (deltaY / deltaXZ));
        Vector3 initalVelocity = Mathf.Cos(angle) * throwStrength * displacement.normalized + Mathf.Sin(angle) * throwStrength * Vector3.up;
        Debug.Log($"deltaY: {deltaY} deltaXZ: {deltaXZ} gravity: {gravity} throwStrength: {throwStrength} angle: {angle} initalVelocity: {initalVelocity}");
        return new ThrowData
        {
            InitalVelocity = initalVelocity,
            Angle = angle,
            DeltaXZ = deltaXZ,
            DeltaY = deltaY,
        };
    }

    private Vector3 GetPredictedPosition(Vector3 targetPosition, Vector3 startPosition)
    {
        ThrowData throwData = CalculateInitalVelocity(targetPosition, startPosition);
        Vector3 initalVelocity = throwData.InitalVelocity;
        initalVelocity.y = 0;
        float time = throwData.DeltaXZ / initalVelocity.magnitude;
        CharacterController playerController = playerTransform.GetComponent<CharacterController>();
        Vector3 playerMovement = playerController.velocity * time;
        Vector3 newPlayerPosition = new Vector3(playerTransform.position.x + playerController.center.x + playerMovement.x,
                                                projectileSpawnTransform.position.y,
                                                playerTransform.position.z + playerController.center.x + playerMovement.z);
        // Re calculate the trageectory based on player's position
        ThrowData predictiveInitalVelcoity = CalculateInitalVelocity(newPlayerPosition, projectileSpawnTransform.position);
        predictiveInitalVelcoity.InitalVelocity = Vector3.ClampMagnitude(predictiveInitalVelcoity.InitalVelocity, maxThrowForce);
        return predictiveInitalVelcoity.InitalVelocity;
    }
}

internal class ThrowData
{
    public Vector3 InitalVelocity { get; set; }
    public float Angle { get; set; }
    public float DeltaXZ { get; set; }
    public float DeltaY { get; set; }
}