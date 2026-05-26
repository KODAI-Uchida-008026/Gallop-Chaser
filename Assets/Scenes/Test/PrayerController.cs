using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// CharacterController が必須になる
[RequireComponent(typeof(CharacterController))]
public class PrayerController : MonoBehaviour
{
    // 移動スピード
    [SerializeField] private float moveSpeed;

    // ジャンプ力
    [SerializeField] private float jumpPower;

    // CharacterController の参照
    private CharacterController characterController;

    // アニメーターの参照
    private Animator animator;

    // 移動速度を保存する変数
    private Vector3 moveVelocity;

    // 溜めるジャンプの変数
    public float chargeTime;

    // 減速配置
    private bool isSlow;

    // 加速判定
    private bool isFast;

    void Start()
    {
        // CharacterControllerコンポーネント取得
        characterController = GetComponent<CharacterController>();

        // 馬の子オブジェクトにあるAnimatorコンポーネントを取得
        animator = GetComponentInChildren<Animator>();

        // 溜めるジャンプのコルーチン    先生のサイトを参考
        StartCoroutine(ChargeJumpCoroutine());
    }

    void Update()
    {
        // 毎フレーム 横移動をリセット
        moveVelocity.x = 0f;

        // 前に自動で進ませる
        moveVelocity.z = moveSpeed;

        // 速度判定
        if (isSlow)
        {
            moveSpeed = 2.5f;
        }
        else if (isFast)
        {
            moveSpeed = 13f;
        }
        else
        {
            moveSpeed = 5f;
        }

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
            fastCheck(true);
        }
        else
        {
            fastCheck(false);
        }


        // Sと↓キーで止まる
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            // 通常は勢いがあるため少しだけ後退させる
            isCharge(true);
        }

        // 地面に接しているか
        if (characterController.isGrounded)
        {
            // キー入力を離したらジャンプ
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

    // 溜めるジャンプ
    public void isCharge(bool stop)
    {
        // 停止判定
        if (stop)
        {
            // 通常は勢いがあるため少しだけ後退させる
            moveVelocity.z -= moveSpeed + 0.3f;
        }
        else
        {
            // 減速中は完全に停止させる
            moveVelocity.z *= 0f;
        }
    }

    // 減速判定
    public void slowCheck(bool slow)
    {
        isSlow = slow;
    }

    // 加速判定
    public void fastCheck(bool fast)
    {
        isFast = fast;
    }

    public IEnumerator ChargeJumpCoroutine()
    {
        while (true)    // ChatGPT協力
        {
            // Sか↓キーが押されている間は続く
            if (Keyboard.current.downArrowKey.isPressed ||
                Keyboard.current.sKey.isPressed)
            {
                // 再起動させる
                chargeTime = 0f;

                // 押している間チャージ
                while (Keyboard.current.downArrowKey.isPressed ||
                       Keyboard.current.sKey.isPressed)
                {
                    chargeTime += Time.deltaTime;

                    // 段階ごとのジャンプ力
                    if (chargeTime >= 2.5f)
                    {
                        jumpPower = 7f;
                        Debug.Log("大ジャンプ");
                    }
                    else if (chargeTime >= 1.5f)
                    {
                        jumpPower = 6f;
                        Debug.Log("中ジャンプ");
                    }
                    else
                    {
                        jumpPower = 5f;
                        Debug.Log("小ジャンプ");
                    }

                    yield return null;
                }

                // 離したら戻す
                jumpPower = 5f;
                Debug.Log("再開");
            }

            // 1フレーム待って終了
            yield return null;
        }
    }
}