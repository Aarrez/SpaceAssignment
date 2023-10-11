using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private Button[] buttons;
    private Canvas pauseCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        pauseCanvas = GetComponentInChildren<Canvas>();

        buttons = GetComponentsInChildren<Button>();

        buttons[0].onClick.AddListener(ResumePause);
        buttons[1].onClick.AddListener(MainMenu);
        InputScript.CPause = ResumePause;
        InputScript.KPause = ResumePause;


        pauseCanvas.gameObject.SetActive(false);
    }



    private void ResumePause()
    {
        pauseCanvas.gameObject.SetActive(!pauseCanvas.gameObject.activeSelf);
        Time.timeScale = pauseCanvas.gameObject.activeSelf ? Time.timeScale = 0 : Time.timeScale = 1;
    }

    private void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}