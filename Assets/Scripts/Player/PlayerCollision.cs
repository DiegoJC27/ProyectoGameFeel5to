using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerMovement playerScript;

    private void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Collectibles col = other.GetComponent<Collectibles>();
        if (col != null)
            col.OnCollideWithPlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (playerScript != null)
            {
                if((playerScript._IsAttacking || playerScript._IsGroundPound))
                {
                    //matar enemigo
                    //
                    return;
                }
                else if(playerScript._IsGettingHit == true) return;
                else if(playerScript._IsGettingHit == false && playerScript._IsAttacking! && playerScript._IsGroundPound!)
                {
                    StartCoroutine(playerScript.GetHit());
                }
            }
        }
        if (collision.collider.CompareTag("Caja"))
        {
           
                Caja caja = collision.collider.GetComponent<Caja>();

                if (caja == null) return;

                Vector3 normal = collision.contacts[0].normal;

                if (playerScript._IsAttacking || playerScript._IsGroundPound)
                { 
                    caja.Romper(Caja.TipoImpacto.Golpear); 
                    return;
                }
                else if (normal.y < -0.5f)
                {
                    caja.Romper(Caja.TipoImpacto.Saltar);
                    return;
                }
            
        }
    }
}
