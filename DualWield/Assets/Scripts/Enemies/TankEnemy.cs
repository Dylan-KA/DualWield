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
        ThrowProjectile();
        ResetAttackWaitTime();
    }
    private void ThrowProjectile()
    {
        GameObject bullet = Instantiate(projectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = 
            CalculateInitalVelocity(playerTransform.position + playerTransform.gameObject.GetComponent<CharacterController>().center, bullet.GetComponent<Transform>().position);
    }

    private Vector3 CalculateInitalVelocity(Vector3 targetPosition, Vector3 startPosition)
    {
        Vector3 displacement = new Vector3(targetPosition.x, startPosition.y, targetPosition.z) - startPosition;
        float deltaY = targetPosition.y - startPosition.y;
        float deltaXZ = displacement.magnitude;
        float gravity = Mathf.Abs(Physics.gravity.y);
        float throwStrength = Mathf.Clamp(Mathf.Sqrt(gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXZ, 2)))), 0.01f, maxThrowForce);
        float angle = Mathf.PI / 2f - (0.5f * Mathf.PI / 2 - (deltaY / deltaXZ));
        Debug.Log($"deltaY: {deltaY} deltaXZ: {deltaXZ} gravity: {gravity} throwStrength: {throwStrength} angle: {angle}");
        Vector3 initalVelocity = Mathf.Cos(angle) * throwStrength * displacement.normalized + Mathf.Sin(angle) * throwStrength * Vector3.up;
        Debug.Log(initalVelocity);
        return initalVelocity;
    }
}
