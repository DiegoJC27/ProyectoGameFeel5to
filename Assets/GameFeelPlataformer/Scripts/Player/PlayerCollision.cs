using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    PlayerMovement playerScript;
    Coroutine getHitCorutine;
    public UnityEvent onFreezeEvent;
    [SerializeField] SoundManager collectibleSounds;
    Animator animator; 
    private void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    //Recolectables
    private void OnTriggerEnter(Collider other)
    {
        Collectibles col = other.GetComponent<Collectibles>();
        if (col != null)
        {
            if (other.CompareTag("Coin"))
                collectibleSounds.PlaySound("Coin");
            else if(other.CompareTag("Life"))
                collectibleSounds.PlaySound("Life");
            else if(other.CompareTag("Star"))
                collectibleSounds.PlaySound("Star");

                col.OnCollideWithPlayer();            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (playerScript != null)
            {
               
                if ((!playerScript._IsAttacking && !playerScript._IsGroundPound) && !playerScript._IsGettingHit)
                {
                    
                    if (getHitCorutine == null)
                    {
                        animator.SetTrigger("GetHit");
                        onFreezeEvent?.Invoke();
                        getHitCorutine = StartCoroutine(GetHitCoroutine());
                    }
                }
            }
        }
    }

    private IEnumerator GetHitCoroutine()
    {
        yield return StartCoroutine(playerScript.GetHit());
        getHitCorutine = null; 
    }    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (playerScript != null)
            {
                if (((!playerScript._IsAttacking || !playerScript._IsGroundPound) && playerScript._IsGettingHit == false))
                {
                    if (getHitCorutine == null)
                    {
                        onFreezeEvent?.Invoke();

                        getHitCorutine = StartCoroutine(GetHitCoroutine());
                    }
                }
            }
        }
    }
}
