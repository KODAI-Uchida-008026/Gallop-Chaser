using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    // タイトル
    public TextMeshProUGUI Title;

    // 1位
    // public TextMeshProUGUI First;

    // 2位
    // public TextMeshProUGUI Second;

    // 3位
    // public TextMeshProUGUI Third;

    // 4位
    // public TextMeshProUGUI Fourth;

    // 5位
    // public TextMeshProUGUI Fifth;

    // プレイヤーの記録
    public TextMeshProUGUI Your;

    // 1位から5位までの配列
    public TextMeshProUGUI[] ranking;

    // 5位までと最新記録に限定させるので配列でデータを管理
    float[] ranks = new float[6];


    void Start()
    {
        // セーブデータの読み込み
        ranks[0] = PlayerPrefs.GetFloat("First", 999);
        ranks[1] = PlayerPrefs.GetFloat("Second", 999);
        ranks[2] = PlayerPrefs.GetFloat("Third", 999);
        ranks[3] = PlayerPrefs.GetFloat("Fourth", 999);
        ranks[4] = PlayerPrefs.GetFloat("Fifth", 999);

        // 今回の記録
        ranks[5] = PlayerPrefs.GetFloat("NewTime");

        // 並び替え
        System.Array.Sort(ranks);

        for (int i = 0; i < 5; i++)
        {
            if (ranks[i] == 999)
            {
                ranking[i].text = "---";
            }
            else
            {
                ranking[i].text = ranks[i].ToString("F1");
            }
        }

        // 表示
        // First.text = ranks[0].ToString("F1");
        // Second.text = ranks[1].ToString("F1");
        // Third.text = ranks[2].ToString("F1");
        // Fourth.text = ranks[3].ToString("F1");
        // Fifth.text = ranks[4].ToString("F1");

        // 今回の記録
        Your.text = ranks[5].ToString("F1");
    }
}