using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWithJump : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // 水平方向の移動
            // float moveHorizontal = Input.GetAxis("Horizontal");
            // float moveVertical = Input.GetAxis("Vertical");
            moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
            moveDirection = transform.TransformDirection(moveDirection);
            // moveDirection *= speed;
        }

        // ジャンプの入力
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            moveDirection.y = jumpSpeed;
        }

        // 右
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            moveDirection.x += speed;
        }

        // 左
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            moveDirection.x -= speed;
        }

        // 前
        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveDirection.z += speed;
        }

        // 後
        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveDirection.z -= speed;
        }

        // 重力を適用
        moveDirection.y -= gravity * Time.deltaTime;

        // Moveメソッドで移動
        controller.Move(moveDirection * Time.deltaTime);
    }
}
// 先生のサイト『Unity CharacterController.SimpleMove を使用したジャンプ実装方法』： https://soft-rime.com/post-19551/