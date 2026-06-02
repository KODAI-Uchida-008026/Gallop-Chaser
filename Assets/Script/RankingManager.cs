using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    // プレイヤーの記録
    public TextMeshProUGUI Your;

    // 1位から5位までの配列
    public TextMeshProUGUI[] ranking;

    // 5位までと最新記録に限定させるので配列でデータを管理
    float[] ranks = new float[6];

    // 最新記録
    public float resultTime;

    // 記録なし (constで値の固定)
    private const float Empty = 999;

    private void Start()
    {
        // セーブデータの読み込み
        SaveLoad();

        // 今回の記録
        if (PlayerPrefs.HasKey("NewTime"))
        {
            ranks[5] = PlayerPrefs.GetFloat("NewTime");
        }
        else
        {
            ranks[5] = Empty;
        }

        // 個別で保持
        resultTime = ranks[5];


        // 並び替え
        System.Array.Sort(ranks);

        // データ更新
        for (int i = 0; i < ranking.Length; i++)
        {
            if (ranks[i] >= Empty)
            {
                ranking[i].text = "-----";
            }
            else
            {
                ranking[i].text = ranks[i].ToString("F1");
            }
        }

        // 記録保持
        RankSave();

        // NewTimeの削除 (resultTimeには残っている)
        PlayerPrefs.DeleteKey("NewTime");

        // 今回の記録
        Your.text = resultTime.ToString("F1");
    }

    // セーブデータの読み込み
    void SaveLoad()
    {
        ranks[0] = PlayerPrefs.GetFloat("First", Empty);
        ranks[1] = PlayerPrefs.GetFloat("Second", Empty);
        ranks[2] = PlayerPrefs.GetFloat("Third", Empty);
        ranks[3] = PlayerPrefs.GetFloat("Fourth", Empty);
        ranks[4] = PlayerPrefs.GetFloat("Fifth", Empty);
    }

    // 記録保持
    void RankSave()
    {
        PlayerPrefs.SetFloat("First", ranks[0]);
        PlayerPrefs.SetFloat("Second", ranks[1]);
        PlayerPrefs.SetFloat("Third", ranks[2]);
        PlayerPrefs.SetFloat("Fourth", ranks[3]);
        PlayerPrefs.SetFloat("Fifth", ranks[4]);

        // 保存
        PlayerPrefs.Save();
    }
}