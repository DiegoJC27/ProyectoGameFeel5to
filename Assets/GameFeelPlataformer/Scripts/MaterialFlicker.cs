using UnityEngine;
using DG.Tweening;

public class MaterialFlicker : MonoBehaviour
{
    public Material [] materials;
    public Color targetColor;
    public float tweenTime = 0.5f;
    public int loops = 4;
    private Ease tweenEase = Ease.Linear;
    private Sequence flickerTweenSequence, resetSequence;

	private void Start()
	{
		ResetMaterial();
	}

	/// <summary>
	/// Func�n Flicker
	/// </summary>
	public void FlickerMaterial()
    {
		//Un sequence es una lista de tweens a ejecutar, como los materiales del personaje pueden estar por partes es necesario usar esta clase en lugar de un Tween.
		flickerTweenSequence?.Kill();

		//Hay que declatar una nueva secuencia cada vez que se quiera usar una, comparte funciones con el tween: Kill, OnCommplete, OnStart, etc.
        flickerTweenSequence = DOTween.Sequence();
		flickerTweenSequence.OnComplete(ResetSequence);


		//Por cada material se crea un tween para modificar el color del material
		for (int i = 0; i < materials.Length; i++)
		{
			Tween flicker = materials[i].DOColor(targetColor, tweenTime).SetLoops(loops, LoopType.Yoyo).SetEase(tweenEase).SetUpdate(true);


			// la funci�n Join de la secuendia a�ade el tween a la lista, cuando se a�adan el �ltimo se repoducir�n al mismo tiempo autom�ticamente.
			flickerTweenSequence.Join(flicker);
		}
	}


	/// <summary>
	/// Funci�n para asegurarse de que el tinte del material regrese a blanco (usualmente el valor est�ndard)
	/// </summary>
	private void ResetSequence()
	{
		resetSequence?.Kill();
		resetSequence = DOTween.Sequence();

		for (int i = 0; i < materials.Length; i++)
		{
			Tween flicker = materials[i].DOColor(Color.white, tweenTime).SetEase(tweenEase);
			resetSequence.Join(flicker);
		}
	}

	/// <summary>
	/// Funci�n para inicializar el material en tinte neutro.
	/// </summary>
	void ResetMaterial()
    {
		for (int i = 0; i < materials.Length; i++)
		{
			materials[i].SetColor("_BaseColor", Color.white);
		}
	}
}
