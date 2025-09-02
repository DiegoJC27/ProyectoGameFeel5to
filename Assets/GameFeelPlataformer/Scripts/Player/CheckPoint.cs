using System.Collections;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject posTeleport;
    private Coroutine teleportRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null && teleportRoutine == null) 
            {
                teleportRoutine = StartCoroutine(Teleport(player));
            }
        }
    }

    IEnumerator Teleport(PlayerMovement player)
    {
        player.transform.position = posTeleport.transform.position;
        StartCoroutine(player.GetHit());
        yield return new WaitForSeconds(1f);
        teleportRoutine = null;
    }
}
