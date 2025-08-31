using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public SoundManager menuSoundManager;
    void Start()
    {
        menuSoundManager.PlaySound("MenuTheme");
    }
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
}
