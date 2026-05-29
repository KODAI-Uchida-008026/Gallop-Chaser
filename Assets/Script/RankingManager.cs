using UnityEngine;

public class RankingManager : MonoBehaviour
{
    // ランキング表示の変数
    float resultTime;

    void Start()
    {
        // セーブデータの読み込み
        resultTime = PlayerPrefs.GetFloat("NewTime");
        Debug.Log("記録" + resultTime.ToString("F1"));
    }
}