using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    [SerializeField] private int mangos = 0;
    [SerializeField] private int lifes = 3;
    public bool maskActive = false;

    private void Start()
    {
        PlayerUIController.instance.UpdateMangosText(mangos);
        PlayerUIController.instance.UpdateLifesText(lifes);

    }
    public void CollectMask()
    {
        maskActive = true;
    }
    public void CollectMango()
    {
        mangos++;
        if(mangos >= 100) {
            mangos = 0;
            CollectLife();
        }
        PlayerUIController.instance.UpdateMangosText(mangos);
    }
    public void CollectLife()
    {
        lifes++;
        PlayerUIController.instance.UpdateLifesText(lifes);
    }
    public void LoseLife()
    {
        if (maskActive)
        {
            maskActive = false;
            return;
        }
        lifes--;
        if (lifes <= 0)
        {
            //cosas para perder el juego
        }
        PlayerUIController.instance.UpdateLifesText(lifes - 1);

    }
}
