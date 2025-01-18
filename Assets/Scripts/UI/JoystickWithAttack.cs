using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickWithAttack : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickFrame; // The outer circle (frame) of the joystick
    public RectTransform joystickMover; // The inner movable part of the joystick
    public float attackThreshold = 1f; // How close to the edge to trigger the attack (1.0 = exact edge)

    private Vector2 joystickCenter; // Center of the joystick frame
    private float joystickRadius; // Radius of the joystick frame
    private bool isAttacking = false;

    void Start()
    {
        joystickCenter = joystickFrame.position;
        joystickRadius = joystickFrame.sizeDelta.x / 2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate the position of the joystick mover relative to the joystick center
        Vector2 inputPosition = eventData.position - joystickCenter;
        float distance = inputPosition.magnitude;
        Vector2 clampedPosition = inputPosition.normalized * Mathf.Min(distance, joystickRadius);

        // Move the joystick mover
        joystickMover.anchoredPosition = clampedPosition;
        Debug.Log(distance);

        // Check if the joystick mover is near the edge of the frame
        if (distance >= joystickRadius * attackThreshold)
        {
            if (!isAttacking) // Trigger attack only once
            {
                isAttacking = true;
                Vector2 attackDirection = clampedPosition.normalized;
                Debug.Log(attackDirection);
            }
        }
        else
        {
            isAttacking = false; // Reset attack state when mover is not at the edge
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset joystick mover to the center
        joystickMover.anchoredPosition = Vector2.zero;
        isAttacking = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Handle pointer down if necessary
    }

    private void TriggerAttack(Vector2 direction)
    {
        // Your attack logic goes here
        Debug.Log("Attack triggered in direction: " + direction);
        // Example: Pass the direction to your character or attack system
    }
}
