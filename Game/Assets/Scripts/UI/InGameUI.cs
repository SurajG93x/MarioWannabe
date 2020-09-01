using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text finalScore;
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;

    public void GameOver()
    {
        finalScore.text = score.text;
        gameOverUI.SetActive(true);
    }

    public void levelComplete()
    {
        finalScore.text = score.text;
        levelCompleteUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
