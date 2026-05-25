using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    // プレイヤーコントローラースクリプトの取得
    [SerializeField] private PrayerController pc;

    // スライダーの取得
    public Slider hp;

    // 現在のHP
    float currentHp;

    void Start()
    {
        //SliderをMAXにする
        hp.value = 1;

        //現在のHPを最大HPと同じにする
        currentHp = hp.value;

        Debug.Log("Start currentHp : " + currentHp);
    }

    void Update()
    {
        // ほんの少しずつ体力回復
        currentHp += Time.deltaTime * 0.01f;

        // キー入力を離したらジャンプ判定
        if (Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.sKey.wasReleasedThisFrame)
        {
            // 溜めるジャンプで体力減少
            if (pc.chargeTime >= 3.5f)
            {
                // 溜め過ぎると50%の減少
                currentHp -= 0.5f;
                Debug.Log("溜め過ぎると50%の減少");
            }
            else if (pc.chargeTime >= 2.5f)
            {
                // 大ジャンプ 25%の減少
                currentHp -= 0.25f;
                Debug.Log("大ジャンプ 25% 減少");
            }
            else if (pc.chargeTime >= 1.5f)
            {
                // 中ジャンプ 15%の減少
                currentHp -= 0.15f;
                Debug.Log("中ジャンプ 15% 減少");
            }
            else
            {
                // 小ジャンプ 10%の減少
                currentHp -= 0.1f;
                Debug.Log("小ジャンプ 10% 減少");
            }
        }

        // 体力ゲージが15%以下であれば減速する
        if (currentHp <= 0.15)
        {
            pc.moveSpeed = 2.5f;
            Debug.Log("体力が少ないので減速します...");

            // 20%以下なら多めに回復
            if (currentHp <= 0.20)
            {
                currentHp += Time.deltaTime * 0.025f;

                // 減少すると加速ができない
                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
                {
                    pc.moveSpeed = 2.5f;
                    Debug.Log("今は加速できません");
                }
            }
        }
        else
        {
            // 加速で体力減少
            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            {
                // 1秒ごとに10%減少していく
                currentHp -= Time.deltaTime * 0.05f;
                Debug.Log("加速で体力減少");
            }
        }

        // 更新してスライダーに反映
        hp.value = currentHp;
    }
}