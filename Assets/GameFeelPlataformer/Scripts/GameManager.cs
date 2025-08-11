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
    [SerializeField] private int currentLifes = 3;
    public bool maskActive = false;
    private bool gameOver = false;

    private void Start()
    {
        PlayerUIController.instance.UpdateMangosText(mangos);
        PlayerUIController.instance.UpdateLifesText(currentLifes);

    }
    public void GameOver()
    {
        gameOver = true;
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
        currentLifes++;
        PlayerUIController.instance.UpdateLifesText(currentLifes);
    }
    public void LoseLife()
    {
        if (maskActive)
        {
            maskActive = false;
            return;
        }
        currentLifes--;
        if (currentLifes <= 0)
        {
            GameOver();
        }
        PlayerUIController.instance.UpdateLifesText(currentLifes);

    }
}
