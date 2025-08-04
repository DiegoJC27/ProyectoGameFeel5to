using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController instance;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    [SerializeField] TextMeshProUGUI mangosText;
    [SerializeField] TextMeshProUGUI lifeText;

    public void UpdateMangosText(int q)
    {
        mangosText.text = q.ToString();
    }
    public void UpdateLifesText(int q)
    {
        lifeText.text = q.ToString();
    }
}
