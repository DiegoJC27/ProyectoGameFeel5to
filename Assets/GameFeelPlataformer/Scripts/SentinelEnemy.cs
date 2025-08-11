using System.Collections;
using UnityEngine;

public class SentinelEnemy : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 360f;

    [SerializeField]
    private float cooldown = 2f;

    //[SerializeField]
    //private Collider damageCollider; //Collider del arma

    void Start()
    {
        StartCoroutine(SpinAttack());
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator SpinAttack()
    {
        while (true)
        {
            //Se activa el daño
            //if (damageCollider != null)
            //{
            //    damageCollider.enabled = true;
            //}
            //Girar durante segundos de spinDuration
            float rotated = 0f;
            while (rotated < 360f)
            {
                float step = spinSpeed * Time.deltaTime;
                transform.Rotate(0f, step, 0f);
                rotated += step;
                yield return null;
            }
            ////Desactivar daño
            //if (damageCollider != null)
            //{
            //    damageCollider.enabled = false;
            //}
            //Espera cooldown
            yield return new WaitForSeconds(cooldown);
        }
    } 
}
