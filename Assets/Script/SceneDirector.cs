using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Next"))
        {
            SceneManager.LoadScene("GameScene");
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}