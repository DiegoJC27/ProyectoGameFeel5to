using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FloorCollider : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerScript;
    [SerializeField] private Collider collider;
    [SerializeField]private Rigidbody rigidbody;
    [SerializeField]private bool hasJumpedFromBox = false;
    public UnityEvent onFreezeEvent;


    private void Start()
    {
        playerScript = GetComponentInParent<PlayerMovement>();
        rigidbody = GetComponentInParent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

               if (enemy == null || playerScript == null) return;
            onFreezeEvent?.Invoke();

            playerScript.BoxEnemyJump();            
        }
        else if (other.CompareTag("Caja"))
        {
            Caja caja = other.GetComponent<Caja>();
            if (caja == null || playerScript == null) return;
            if (hasJumpedFromBox == false && playerScript._IsGroundPound==false)
            {
                onFreezeEvent?.Invoke();

                playerScript.BoxEnemyJump();
                caja.Romper(Caja.TipoImpacto.Saltar);
                hasJumpedFromBox = true;
                StartCoroutine(ResetJumpFromBoxAfterDelay(.2f));

            }
            else if( playerScript._IsGroundPound == true)
            {
                onFreezeEvent?.Invoke();

                caja.Romper(Caja.TipoImpacto.Golpear);
            }
        }
        else
        {
            hasJumpedFromBox = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) hasJumpedFromBox = false;
        if (other.CompareTag("Caja")) hasJumpedFromBox = false;
    }
    IEnumerator ResetJumpFromBoxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasJumpedFromBox = false;
        playerScript._IsGrounded = false;
        playerScript._FirstJump = true;
    }
}

