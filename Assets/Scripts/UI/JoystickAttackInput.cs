using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickAttackInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;  // Joystick frame
    public RectTransform joystickKnob;       // Joystick mover

    public float maxRadius = 100f;           // Max distance the knob can move
    public Vector2 inputDirection;           // Normalized direction vector
    public bool isDragging = false;          // Is joystick being used

    private Vector2 joystickCenter;          // Center position of the joystick
    private bool isAtEdge = false;           // Is the knob at the edge?

    void Start()
    {
        joystickCenter = joystickBackground.position;  // Set the center position
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        // Convert screen position to local joystick space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out position
        );

        // Calculate distance and clamp to max radius
        float distance = position.magnitude;
        if (distance > maxRadius)
        {
            position = position.normalized * maxRadius;
            isAtEdge = true; // Knob is at the edge
        }
        else
        {
            isAtEdge = false; // Knob is not at the edge
        }

        // Update knob position
        joystickKnob.localPosition = position;

        // Normalize direction
        inputDirection = position / maxRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // Reset knob position and input direction
        joystickKnob.localPosition = Vector2.zero;
        inputDirection = Vector2.zero;
        isAtEdge = false;
    }

    void Update()
    {
        if (isDragging && isAtEdge)
        {
            // Handle attack logic only when the knob is at the edge
            HandleAttack(inputDirection);
        }
    }

    private void HandleAttack(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Normalize direction for consistent movement
            direction.Normalize();

            // Example: Rotate character or weapon based on direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Debug.Log($"Attacking! Direction: {direction}, Angle: {angle}");

            // Add your attack logic here, e.g., spawn bullets or swing weapon
        }
    }
}
