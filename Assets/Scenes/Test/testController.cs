using UnityEngine;
using UnityEngine.InputSystem;

// CharacterController が必須になる
[RequireComponent(typeof(CharacterController))]
public class testController : MonoBehaviour
{
    // 横移動スピード
    [SerializeField] private float moveSpeed = 5;

    // ジャンプ力
    [SerializeField] private float jumpPower = 5;

    // CharacterController の参照
    private CharacterController characterController;

    // アニメーターの参照
    private Animator animator;

    // 移動速度を保存する変数
    private Vector3 moveVelocity;

    void Start()
    {
        // CharacterControllerコンポーネント取得
        characterController = GetComponent<CharacterController>();

        // 馬の子オブジェクトにあるAnimatorコンポーネントを取得
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 毎フレーム 横移動をリセット
        moveVelocity.x = 0f;

        // 前に自動で進ませる
        moveVelocity.z = moveSpeed;

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

        /*
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
        */

        // 地面に接触しているか
        if (characterController.isGrounded)
        {
            // スペースキーでジャンプ
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                moveVelocity.y = jumpPower;
            }
        }
        else
        {
            // 重力を加算（空中）
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // キャラクター移動
        characterController.Move(moveVelocity * Time.deltaTime);

        // アニメーター
        animator.SetFloat("JumpPower", moveVelocity.y);
        animator.SetFloat("MoveSpeed", new Vector2(moveVelocity.x, moveVelocity.z).magnitude);
    }
}