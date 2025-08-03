using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;

    public Transform[] targets;
    int targetsIndex;
    Transform currentTarget;

    void Start()
    {
        currentTarget = targets[targetsIndex];
        RotateTowardsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 targetPos = currentTarget.position;

        if (Vector3.Distance(transform.position, targetPos) <= 0.5f)
        {
            targetsIndex++;
            if (targetsIndex == targets.Length) { targetsIndex = 0; }
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
}
