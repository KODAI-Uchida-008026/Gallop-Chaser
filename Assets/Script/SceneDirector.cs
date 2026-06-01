using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    // タイムディレクターの参照
    [SerializeField] TimeDirector timeDirector;

    public void GameStart()
    {
        // 練習シーンへ
        SceneManager.LoadScene("PracticeScene");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Next"))
        {
            // ゲーム本番へ
            SceneManager.LoadScene("GameScene");
        }
        else if (other.CompareTag("Finish"))
        {
            // タイマーストップ
            timeDirector.timerStop();
            Debug.Log("タイマーストップ");

            // リザルト画面へ
            SceneManager.LoadScene("ResultScene");
        }
    }
}