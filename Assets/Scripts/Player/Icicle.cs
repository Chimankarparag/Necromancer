using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject ice1prefab;
    [SerializeField] private Transform ice1spawnpoint;
    [SerializeField] private GameObject attackController; // Main attack controller UI GameObject
    [SerializeField] private RectTransform joystickKnob; // Joystick knob (mover)
    [SerializeField] private RectTransform joystickBackground; // Joystick frame (background)

    private Animator myAnimator;
    private float manaUsage;
    private float manaLeft;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");


    private void Awake(){
        myAnimator = GetComponent<Animator>();
        
        if (attackController != null)
        {
            // Locate child elements dynamically by their names
            joystickBackground = attackController.transform.Find("Background").GetComponent<RectTransform>();
            joystickKnob = attackController.transform.Find("Knob").GetComponent<RectTransform>();

            if (joystickBackground == null || joystickKnob == null)
            {
                Debug.LogError("Joystick elements (Background or Knob) not found in AttackController. Check the hierarchy.");
            }
        }
        else
        {
            Debug.LogError("AttackController GameObject is not assigned.");
        }
                
    }
    private void Start() {
        manaUsage = GetWeaponInfo().manaUsage;
    }

    private void Update() {
        CalculateAttackDirection();
    }

    public void Attack()
    {
        manaLeft = PlayerMana.Instance.currentMana - manaUsage;
        if(manaLeft >=0 ){
            PlayerMana.Instance.UseMana(manaUsage);
            myAnimator.SetTrigger(FIRE_HASH);
            GameObject newice = Instantiate(ice1prefab,ice1spawnpoint.position,ActiveWeapon.Instance.transform.rotation);
            newice.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);

        }
        else{
            Debug.Log("Insufficient Mana");
        }
        
    }
    private void CalculateAttackDirection()
{
    Vector3 direction;

    // if (Application.isMobilePlatform)
    if (false)
    {
        // Get the position of the joystick mover relative to the frame center
        Vector2 moverPosition = joystickKnob.localPosition; // Local position of the joystick knob relative to its parent (frame)
        Vector2 frameCenter = Vector2.zero; // Center of the joystick frame in local space

        // Compute the relative direction
        direction = new Vector3(moverPosition.x, moverPosition.y, 0).normalized;

        // Calculate the angle in 360 degrees
        float angle360 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle360 < 0)
        {
            angle360 += 360f; // Convert negative angles to 0-360 range
        }

        Debug.Log($"Joystick Direction: {direction}, Angle: {angle360}°");

        // Set weapon rotation
        if (direction.x < 0)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(180, 0, -angle360);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle360);
        }
    }
    else
    {
        // Use mouse input for PC
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 playerPos = PlayerController.Instance.transform.position;
        direction = (mouseWorldPos - playerPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x < 0)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}


    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
