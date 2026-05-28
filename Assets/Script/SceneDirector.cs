using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Next"))
        {
            SceneManager.LoadScene("TestScene");
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}