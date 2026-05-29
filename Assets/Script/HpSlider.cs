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

    // 回復判定
    private bool isRefresh;

    // 襲歩判定
    private bool isGallop;

    // 体力が15%を切ると20%を超えるまで減速
    public bool isDanger;

    void Start()
    {
        //SliderをMAXにする
        hp.value = 1;

        //現在のHPを最大HPと同じにする
        currentHp = hp.value;
        // Debug.Log("Start currentHp : " + currentHp);
    }

    void Update()
    {
        // 体力の上限・下限
        if (currentHp >= 1)
        {
            currentHp = 1;
        }
        else if (currentHp < 0)
        {
            currentHp = 0;
        }

        // 体力回復・体力減少
        if (isGallop)
        {
            // 加速で体力減少
            currentHp -= Time.deltaTime * 0.05f;
        }
        else if (isRefresh)
        {
            // 20%以下なら多めに回復
            currentHp += Time.deltaTime * 0.02f;
        }
        else
        {
            // ほんの少しずつ体力回復
            currentHp += Time.deltaTime * 0.01f;
        }

        // キー入力を離したらジャンプ判定
        if (Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.sKey.wasReleasedThisFrame)
        {
            // 溜めるジャンプで体力減少
            if (pc.chargeTime >= 3.5f)
            {
                // 溜め過ぎると50%の減少
                currentHp -= 0.5f;
                // Debug.Log("溜め過ぎると50%の減少");
            }
            else if (pc.chargeTime >= 2.5f)
            {
                // 大ジャンプ 25%の減少
                currentHp -= 0.25f;
                // Debug.Log("大ジャンプ 25% 減少");
            }
            else if (pc.chargeTime >= 1.5f)
            {
                // 中ジャンプ 15%の減少
                currentHp -= 0.15f;
                // Debug.Log("中ジャンプ 15% 減少");
            }
            else
            {
                // 小ジャンプ 10%の減少
                currentHp -= 0.1f;
                // Debug.Log("小ジャンプ 10% 減少");
            }
        }

        // 加速で体力減少
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            // 減少すると加速ができない
            if (isDanger)
            {
                pc.fastCheck(false);
                isGallop = false;
                // Debug.Log("今は加速できません");
            }
            else
            {
                pc.fastCheck(true);
                isGallop = true;
            }
        }
        else
        {
            pc.fastCheck(false);
            isGallop = false;
        }

        // 体力ゲージが15%を下回ると疲労判定
        if (currentHp <= 0.15)
        {
            isDanger = true;
        }

        // 減少してから20%を超えれば復活
        if (currentHp > 0.20)
        {
            // 疲労判定
            isDanger = false;

            // 回復判定
            isRefresh = false;
        }

        // 疲労判定
        if (isDanger)
        {
            // 体力ゲージが15%以下であれば減速する
            pc.slowCheck(true);
            // Debug.Log("体力が少ないので減速します...");

            // 20%以下なら多めに回復
            isRefresh = true;
        }

        // 更新してスライダーに反映
        hp.value = currentHp;
    }
}