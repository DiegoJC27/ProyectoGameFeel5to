using DG.Tweening.Plugins.Core;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Caja : MonoBehaviour
{
    public enum TipoImpacto { Golpear, Saltar }

    [Header("Audio")]
    [SerializeField] private SoundManager boxSoundManager;
    bool isBeingDestroyed = false;
    /// Llama esto en los hijos cuando reciban impacto.
    protected void PlayImpactSound(TipoImpacto tipo)
    {
        if (tipo == TipoImpacto.Saltar)
        {
            boxSoundManager.PlaySound("Bounce");
        }
        else if( tipo == TipoImpacto.Golpear)
        {
            boxSoundManager.PlaySound("Destroy");
        }
    }

    public virtual void Romper(TipoImpacto tipo)
    {
        if (!isBeingDestroyed)
        {
            isBeingDestroyed = true;
            StartCoroutine(RomperCaja());
        }
    }
    protected IEnumerator RomperCaja()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
