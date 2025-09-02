using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameWonMenu;
    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void LoadGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void LoadMenuScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GameLost()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameWon()
    {
        gameWonMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
