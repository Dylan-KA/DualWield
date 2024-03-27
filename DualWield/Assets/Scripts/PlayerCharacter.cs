using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter: BaseCharacter
{
   
    
    /*
    private BaseWeapon LeftHand;

    private BaseWeapon RightHand;
    */
    
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
    }
    
    void Update()
    {
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
    }   


}
