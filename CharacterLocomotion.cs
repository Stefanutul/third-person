using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public Animator rigController;
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;

    Animator animator;
    CharacterController cc;
    Active_Weapon activeWeapon;
    ReloadWeapon reloadWeapon;
    CharacterAiming characterAiming;
    Vector2 input;

    Vector3 rootMotion;
    Vector3 velocity;
    bool isChangingWeapon;
    bool isJumping;
    bool isSprinting;
    bool isFiring;

    int isSprintingParam = Animator.StringToHash("isSprinting");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        activeWeapon = GetComponent<Active_Weapon>();
        reloadWeapon = GetComponent<ReloadWeapon>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x  = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("x", input.x);
        animator.SetFloat("y", input.y);
       
        UpdateisSprinting();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        } 
    }
    bool IsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isFiring = activeWeapon.IsFiring();
        bool isReloading = reloadWeapon.isReloading;
        bool isChangingWeapon = activeWeapon.isChangingWeapon;
       return isSprinting && !isFiring && !isReloading && !isChangingWeapon ;
    }
    private void UpdateisSprinting()
    {
        bool isSprinting = IsSprinting();
        animator.SetBool(isSprintingParam, isSprinting);
        rigController.SetBool(isSprintingParam, isSprinting);
        if (isSprinting)
            groundSpeed = 1.2f;
        else
            groundSpeed = 1;

    }

    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;

    }

    private void FixedUpdate()
    {
        if (isJumping)
        { // in air State
            UpdateInAir();
        }
        else // isGRounded state
        {
            UpdateOnGround();
        }
        Vector3 CalculateAirControl()
        {
            return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
        }

        void UpdateInAir()
        {
            velocity.y -= gravity * Time.deltaTime;
            Vector3 displacement = velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControl();
            cc.Move(displacement);
            isJumping = !cc.isGrounded;
            rootMotion = Vector3.zero;

        }
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed ;
        Vector3 stepDownAmount = Vector3.down * stepDown;
        cc.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);

        if (!cc.isGrounded)
        {
            SetInAir(0);
        }
    }
    

    void Jump()
        {
            if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", true);
    }
}

