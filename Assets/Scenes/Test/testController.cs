using UnityEngine;
using UnityEngine.InputSystem;

// CharacterController が必須になる
[RequireComponent(typeof(CharacterController))]
public class testController : MonoBehaviour
{
    // 横移動スピード
    [SerializeField] private float moveSpeed = 3;

    // ジャンプ力
    [SerializeField] private float jumpPower = 5;

    // CharacterController の参照
    private CharacterController characterController;

    // Transform の参照
    private Transform tm;

    // 移動速度を保存する変数
    private Vector3 moveVelocity;

    void Start()
    {
        // CharacterController コンポーネント取得
        characterController = GetComponent<CharacterController>();

        // Transform取得
        tm = transform;
    }

    void Update()
    {
        // 毎フレーム 横移動をリセット
        moveVelocity.x = 0f;
        moveVelocity.z = 0f;

        // →キーで右移動
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            moveVelocity.x += moveSpeed;
        }

        // ←キーで左移動
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            moveVelocity.x -= moveSpeed;
        }

        // ↑キーで前進
        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveVelocity.z += moveSpeed;
        }

        // ↓キーで後退
        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveVelocity.z -= moveSpeed;
        }

        // 地面に接触しているか
        if (characterController.isGrounded)
        {
            // スペースキー押下時にジャンプ
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                // 上方向にジャンプ力を与える
                moveVelocity.y = jumpPower;
            }
        }
        else
        {
            // 空中では重力を加算  Physics.gravity.y は通常 -9.81
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // キャラクター移動
        // Move() は「このフレームでどれだけ移動するか」を渡すUnity側のメソッド
        characterController.Move(moveVelocity * Time.deltaTime);
    }
}