using UnityEngine;

public class LifeScript : Collectibles
{
    public override void OnCollideWithPlayer()
    {
        GameManager.instance.CollectLife();
        Destroy(gameObject);
    }
}
