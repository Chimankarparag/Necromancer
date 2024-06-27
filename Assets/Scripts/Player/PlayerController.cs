using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : Singleton<PlayerController>
{
    public bool ismoving=false;
    // public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 1f; 
    [SerializeField] private float dashSpeed =4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;


    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private Knockback knockback ;


    private bool isDashing = false;


    protected override void Awake() {
        base.Awake(); 
        // protected void Awake(){
        // Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        
    }
    private void Start()
    {

        playerControls.Combat.Dash.performed += _ => Dash();

        ActiveInventory.Instance.EquipStartingWeapon();
    }
    private void OnEnable() {
        playerControls.Enable();
    }

    private void Update() {
        PlayerInput();
    }

    

    private void FixedUpdate() {
        AdjustPlayerFacingDirection();
        Move();
    }
    public Transform GetWeaponCollider(){
        return weaponCollider;
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        if (movement!=Vector2.zero){
            Move();
            myAnimator.SetBool("ismove",true);
            myAnimator.SetFloat("moveX", movement.x);
            myAnimator.SetFloat("moveY", movement.y);

        }
        else{
            myAnimator.SetBool("ismove",false);

        }

       
    }

    private void Move() {
        if(knockback.GettingKnockedBack|| PlayerHealth.Instance.isDead){
            return;
        }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        ismoving=true;
        
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) {
            mySpriteRender.flipX = true;
        } else {
            mySpriteRender.flipX = false;
        }
    }
    private void Dash(){
        if(!isDashing){
            isDashing = true;
            moveSpeed *=dashSpeed;
            myTrailRenderer.emitting = true;

            StartCoroutine(EndDashRoutine());

        }
    }

    private IEnumerator EndDashRoutine(){
        float dashTime = .25f;
        float dashCD = 1.75f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /=dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        
        isDashing = false;
    }
}
