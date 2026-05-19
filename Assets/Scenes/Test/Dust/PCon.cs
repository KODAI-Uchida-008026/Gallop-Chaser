using UnityEngine;
using UnityEngine.InputSystem;

public class PCon : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jump = 3.0f;
    private Vector3 moveVelocity;
    private Transform tm;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tm = transform;
    }

    void Update()
    {
        moveVelocity.x = 0f; moveVelocity.z = 0f;

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            moveVelocity.x += speed;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            moveVelocity.x -= speed;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveVelocity.z += speed;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveVelocity.z -= speed;
        }

        // Y速度は維持
        moveVelocity.y = rb.linearVelocity.y;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            moveVelocity.y += jump;
        }

        rb.linearVelocity = moveVelocity;
    }
}