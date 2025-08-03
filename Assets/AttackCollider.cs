using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    [SerializeField] private PlayerMovement playerScript;
    [SerializeField]private Collider collider;
    [SerializeField] private Rigidbody rigidbody;

    private void Start()
    {
        playerScript = GetComponentInParent<PlayerMovement>();
        rigidbody = GetComponentInParent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (playerScript != null && (playerScript._IsAttacking || playerScript._IsGroundPound))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    //enemy.Die();  
                }
            }
        }
        if (other.CompareTag("Caja"))
        {
            if (playerScript != null && (playerScript._IsAttacking || playerScript._IsGroundPound))
            {
                Caja caja = other.GetComponent<Caja>();
                if (caja != null)
                {
                    caja.Romper(Caja.TipoImpacto.Golpear);
                    return;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (playerScript != null && (playerScript._IsAttacking || playerScript._IsGroundPound))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    //enemy.Die();  
                }
            }
        }
        if (other.CompareTag("Caja"))
        {
            if (playerScript != null && (playerScript._IsAttacking || playerScript._IsGroundPound))
            {
                Caja caja = other.GetComponent<Caja>();
                if (caja != null)
                {
                    caja.Romper(Caja.TipoImpacto.Golpear);
                    return;
                }
            }
        }
    }
}


