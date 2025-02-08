using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    public static JoystickInput Instance;

    private Vector2 joystickDirection = Vector2.zero;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetJoystickDirection(Vector2 direction)
    {
        joystickDirection = direction;
    }

    public Vector2 GetJoystickDirection()
    {
        return joystickDirection;
    }
}
