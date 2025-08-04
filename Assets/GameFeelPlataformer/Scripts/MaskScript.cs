using UnityEngine;

public class MaskScript : Collectibles
{
    public override void OnCollideWithPlayer()
    {
        GameManager.instance.CollectMask();
        Destroy(gameObject);
    }
}
