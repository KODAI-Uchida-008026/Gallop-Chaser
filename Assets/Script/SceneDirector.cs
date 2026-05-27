using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    GameObject goal;
    [SerializeField] private CharacterController characterController;

    private void Start()
    {
        goal = GameObject.Find("GoalLine");
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("MainScene");
    }
}