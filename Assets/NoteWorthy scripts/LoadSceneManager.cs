using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private int currentIndex = 0;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += LevelChanged;
    }

    private void LevelChanged(Scene current, Scene next)
    {
        currentIndex = next.buildIndex;
    }
    public void NextLevel()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadSceneAsync(currentIndex + 1);
    }
    
    public void PreviousLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 1)
            return;
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
  #endif
    }
}
