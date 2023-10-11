using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private LoadSceneManager loadSceneManager;

    private void Start()
    {
        loadSceneManager = FindObjectOfType<LoadSceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var currentSceneCount = SceneManager.sceneCountInBuildSettings - 1;
        if (currentSceneCount <= SceneManager.GetActiveScene().buildIndex)
        {
            loadSceneManager.QuitGame();
            return;
        }

        loadSceneManager.NextLevel();
    }
}