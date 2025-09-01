using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameWonMenu;
    [SerializeField] TextMeshProUGUI coinsCollectedWon;
    [SerializeField] TextMeshProUGUI coinsCollectedLose;
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
        GetCoinsCollected();
        gameOverMenu.SetActive(true);
    }
    public void GameWon()
    {
        GetCoinsCollected();
        gameWonMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void GetCoinsCollected()
    {
        coinsCollectedLose.text = PlayerUIController.instance.mangosText.text;
        coinsCollectedWon.text = PlayerUIController.instance.mangosText.text;
    }
}
