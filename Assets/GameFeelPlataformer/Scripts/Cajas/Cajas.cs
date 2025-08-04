using UnityEngine;

public abstract class Caja : MonoBehaviour
{
    public enum TipoImpacto
    {
        Golpear,
        Saltar
    }

    public abstract void Romper(TipoImpacto tipo);
}