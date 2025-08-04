using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
