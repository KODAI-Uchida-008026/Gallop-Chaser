using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    // プレイヤーコントローラースクリプトの取得
    [SerializeField] private PrayerController pc;

    // スライダーの取得
    public Slider hp;

    // 最大HP
    int maxHp = 100;

    // 現在のHP
    int currentHp;

    void Start()
    {
        //SliderをMAXにする
        hp.value = 1;

        //現在のHPを最大HPと同じにする
        currentHp = maxHp;

        Debug.Log("Start currentHp : " + currentHp);
    }
    void Update()
    {
        // ほんの少しずつ体力回復
        hp.value += Time.deltaTime * 0.01f;

        // valueの0.01がHPの1%
        currentHp += (int)(Time.deltaTime * 1);

        // 溜めるジャンプで体力減少
        if (pc.chargeTime >= 3.5f)
        {
            // 溜め過ぎると30%の減少
            hp.value -= 0.3f;
            currentHp -= 30;
            Debug.Log("溜め過ぎると30%の減少");
        }
        else if (pc.chargeTime >= 2.5f)
        {
            // 大ジャンプ 15%の減少
            hp.value -= 0.15f;
            currentHp -= 15;
            Debug.Log("大ジャンプ 15% 減少");
        }
        else if (pc.chargeTime >= 1.5f)
        {
            // 中ジャンプ 10%の減少
            hp.value -= 0.1f;
            currentHp -= 10;
            Debug.Log("中ジャンプ 10% 減少");
        }
        else
        {
            // 小ジャンプ 5%の減少
            hp.value -= 0.05f;
            currentHp -= 5;
            Debug.Log("小ジャンプ 5% 減少");
        }

        // 加速で体力減少
        if (pc.moveSpeed >= 10)
        {
            // 1秒ごとに10%減少していく
            hp.value -= Time.deltaTime * 0.1f;
            currentHp -= (int)(Time.deltaTime * 10);
            Debug.Log("加速で体力減少");
        }

        // 体力ゲージが15%以下であれば減速する
        if (hp.value <= 15)
        {
            pc.moveSpeed = 2.5f;
            Debug.Log("体力が少ないので減速します...");
        }
        else
        {
            pc.moveSpeed = 5;
        }
    }
}