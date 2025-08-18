using DG.Tweening.Plugins.Core;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Caja : MonoBehaviour
{
    public enum TipoImpacto { Golpear, Saltar }

    [Header("Audio")]
    [SerializeField] private SoundManager boxSoundManager;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private bool spatialSound = false; // false=2D, true=3D
    [SerializeField, Range(0f, 0.1f)] private float pitchJitter = 0.02f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 15f;
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
    IEnumerator RomperCaja()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
