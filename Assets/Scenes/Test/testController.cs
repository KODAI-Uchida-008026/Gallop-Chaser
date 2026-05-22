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

        // Dと→キーで右移動
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            moveVelocity.x += moveSpeed;
        }

        // Aと←キーで左移動
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            moveVelocity.x -= moveSpeed;
        }


        // Wと↑キーで加速させる
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            moveSpeed = 12.5f;
        }
        else
        {
            moveSpeed = 5;
        }


        // Sと↓キーで止まる
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            moveVelocity.z -= moveSpeed;
        }

        // 地面に接しているか
        if (characterController.isGrounded)
        {
            // キー入力を離したらジャンプ！
            if (Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.sKey.wasReleasedThisFrame)
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