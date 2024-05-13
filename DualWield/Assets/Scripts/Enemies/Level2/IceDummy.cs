using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDummy : BaseEnemy
{
    [SerializeField] private Rigidbody rb;

    protected override void Start()
    {
        try
        {
            rend = GetComponent<MeshRenderer>();
            characterColor = rend.material.color;
        }
        catch
        {
            Debug.Log("Mesh Renderer does not exist");
        }
        SetGravity(false);
    }

    public override void TakeDamage(float damageAmount)
    {

    }


    public override void Freeze()
    {
        base.Freeze();
        SetGravity(true);
    }

    private void SetGravity(bool isGrav)
    {
        rb.useGravity = isGrav;
    }
}
