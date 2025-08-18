using UnityEngine;

public class MainMenuSounds : MonoBehaviour
{
    public SoundManager menuSoundManager;
    void Start()
    {
        menuSoundManager.PlaySound("MenuTheme");
    }
    public void PlayClickSound()
    {
        menuSoundManager.PlaySound("ClickButton");
    }
}
