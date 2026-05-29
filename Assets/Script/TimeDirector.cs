using TMPro;
using UnityEngine;

public class TimeDirector : MonoBehaviour
{
    // ゲームオブジェクトの参照
    GameObject Timer;

    // 時間
    public float time = 0.0f;

    // 記録タイム
    public float newTime;

    // 時間判定
    public bool isPlaying;

    void Start()
    {
        this.Timer = GameObject.Find("Time");
        isPlaying = true;
    }

    void Update()
    {
        // 時間経過
        if (isPlaying)
        {
            this.time += Time.deltaTime;
        }

        // 時間経過に合わせてテキストの更新
        this.Timer.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
    }

    public void timerStop()
    {
        // タイマーストップ
        isPlaying = false;

        // 記録タイム
        newTime = this.time;

        // タイム表示
        Debug.Log("記録タイム： " + newTime.ToString("F1"));

        // データ保存
        PlayerPrefs.SetFloat("NewTime", newTime);

        // データセーブ
        PlayerPrefs.Save();
        Debug.Log("セーブ完了");
    }
}