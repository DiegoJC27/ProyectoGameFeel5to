using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerMovement playerScript;

    private void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
    
    }
    //Recolectables
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
                if(((!playerScript._IsAttacking || !playerScript._IsGroundPound) && playerScript._IsGettingHit == false))
                {
                    StartCoroutine(playerScript.GetHit());
                }
            }
        }
    }
    //Recibir danio
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (playerScript != null)
            {
                if (((!playerScript._IsAttacking || !playerScript._IsGroundPound) && playerScript._IsGettingHit == false))
                {
                    StartCoroutine(playerScript.GetHit());
                }
            }
        }
    }
}
