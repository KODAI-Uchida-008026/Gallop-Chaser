using UnityEngine;
using UnityEngine.InputSystem;

public class DummyJump : MonoBehaviour
{
    CharacterController controller;

    // 上に進む速度
    float moveVelocityY;

    // ジャンプするときの速度（重力での速度より大きくすると上にジャンプできる
    float jumpPower = 5;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Jumpボタンを押されていない時、重力分の速度を加えます
        // Physics.gravity.yは、デフォルトで -9.8fなので+=ですが、引くことになります
        moveVelocityY += Physics.gravity.y * Time.deltaTime;

        Debug.Log(controller.isGrounded);

        // キャラクタが地上の場合
        if (controller.isGrounded)
        {
            // moveVelocityY = 0でいいのですが、接地判定が不安定になる現象があります
            // 接地判定を確実に知るため-0.5fを代入します
            // ちなみに0を代入すると、Debug.Logで表示されているのがtrue、falseとバタつくのがわかります
            moveVelocityY = -0.5f;

            // ジャンプボタンがおされたら（標準はスペースキー）
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                // Y軸の速度を代入
                moveVelocityY = jumpPower;
            }
        }

        // キャラクタの移動（今回Y軸だけでテスト）
        controller.Move(new Vector3(0, moveVelocityY, 0) * Time.deltaTime);
    }
}

// 先生のサイト『CharacterContollerを使ったキャラクタをジャンプさせる』： https://soft-rime.com/post-7317/