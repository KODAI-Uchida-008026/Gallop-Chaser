using System.Collections;
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

    private void Start()
    {
        StartCoroutine(ReStartCoroutine());
    }

    IEnumerator ReStartCoroutine()
    {
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            yield return new WaitForSeconds(15);
            SceneManager.LoadScene("TitleScene");
            Debug.Log("10秒経ってタイトルに戻る");
        }
    }
}