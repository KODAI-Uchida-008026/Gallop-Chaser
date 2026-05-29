using TMPro;
using UnityEngine;

public class TimeDirector : MonoBehaviour
{
    // ゲームオブジェクトの参照
    GameObject Timer;

    // 時間
    public float time = 0.0f;

    void Start()
    {
        this.Timer = GameObject.Find("Time");
    }

    void Update()
    {
        // 時間経過
        this.time += Time.deltaTime;

        // 時間経過に合わせてテキストの更新
        this.Timer.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
    }
}