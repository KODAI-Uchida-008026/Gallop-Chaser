using UnityEngine;
using UnityEngine.InputSystem;

public class umaCon : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) moveX = -1f;
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) moveX = 1f;
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) moveZ = 1f;
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) moveZ = -1f;

        Vector3 move = transform.forward * moveZ + transform.right * moveX;

        rb.linearVelocity = new Vector3(
            move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);

        // ジャンプ（地面にいるときだけ）
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}