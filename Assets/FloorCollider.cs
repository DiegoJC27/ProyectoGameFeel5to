using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerScript;
    [SerializeField] private Collider collider;
    [SerializeField]private Rigidbody rigidbody;
    [SerializeField]private bool hasJumpedFromBox = false;


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
            if (playerScript != null)
            {
                if (enemy == null || playerScript == null) return;

                playerScript.BoxEnemyJump();


            }
        }
        if (other.CompareTag("Caja") && !hasJumpedFromBox)
        {
            Caja caja = other.GetComponent<Caja>();
            if (caja == null || playerScript == null) return;

            playerScript.BoxEnemyJump();
            caja.Romper(Caja.TipoImpacto.Saltar);
            hasJumpedFromBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) hasJumpedFromBox=false;
        if (other.CompareTag("Caja")) hasJumpedFromBox = false;
    }
}

