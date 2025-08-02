using UnityEngine;

public class MangoScript : Collectibles
{
    public override void OnCollideWithPlayer()
    {
        GameManager.instance.CollectMango();
        Destroy(gameObject);
    }
}
