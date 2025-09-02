using UnityEngine;

public class CondicionDeVictoria : Collectibles
{
    public MenuController mc;

    public override void OnCollideWithPlayer()
    {
        mc.GameWon();
        GetComponent<MeshRenderer>().enabled = false;
    }
}
