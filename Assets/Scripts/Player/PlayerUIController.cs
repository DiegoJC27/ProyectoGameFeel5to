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
    [SerializeField] TextMeshProUGUI boxesText;

    public void UpdateMangosText(int q)
    {
        mangosText.text = q.ToString();
    }
    public void UpdateLifesText(int q)
    {
        lifeText.text = q.ToString();
    }
    public void UpdateboxesText(int q)
    {
        boxesText.text = q.ToString();
    }
}
