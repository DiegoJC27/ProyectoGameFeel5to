using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 3.5f;
    public Transform[] targets;
    int targetsIndex;
    Transform currentTarget;

    [Header("Audio")]
    [SerializeField, Range(0f, 1f)] private float killVolume = 1f;

    // Evita muerte doble por colisiones múltiples
    private bool isDying = false;

    void Start()
    {
        currentTarget = targets[targetsIndex];
        RotateTowardsTarget();
    }

    void Update()
    {
        if (isDying) return; // no seguir moviendo si está “muriendo”

        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 targetPos = currentTarget.position;
        if (Vector3.Distance(transform.position, targetPos) <= 0.5f)
        {
            targetsIndex++;
            if (targetsIndex == targets.Length) targetsIndex = 0;
            currentTarget = targets[targetsIndex];
        }
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float rotationSpeed = 850f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;

        //  Desactivar colisiones y render para evitar más triggers/bugs visuales
        var col = GetComponent<Collider>();
        if (col) col.enabled = false;
        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        
        Destroy(gameObject);
    }
}
