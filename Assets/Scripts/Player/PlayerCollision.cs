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
                else if(playerScript._IsGettingHit == false && !playerScript._IsAttacking && !playerScript._IsGroundPound)
                {
                    StartCoroutine(playerScript.GetHit());
                }
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (playerScript != null)
            {
                if ((playerScript._IsAttacking || playerScript._IsGroundPound))
                {
                    return;
                }
                else if (playerScript._IsGettingHit == true) return;
                else if (playerScript._IsGettingHit == false && !playerScript._IsAttacking && !playerScript._IsGroundPound)
                {
                    StartCoroutine(playerScript.GetHit());
                }
            }
        }
    }
}
