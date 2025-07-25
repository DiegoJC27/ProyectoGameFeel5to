using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField]private float speed;
    [SerializeField]private float jumpForce;
    [SerializeField] private float groundPoundForce;
    private float attackDuration = 0f;
    //Checks
    [SerializeField]private bool _IsGroundPound = false;
    [SerializeField]private bool _FirstJump = false;
    [SerializeField]private bool _IsGrounded = false;
    [SerializeField]private bool _IsAttacking = false;

    private Rigidbody rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        GroundPound();
        Attack();
    }

    private void Movement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) move += Vector3.back;
        if (Input.GetKey(KeyCode.A)) move += Vector3.left;
        if (Input.GetKey(KeyCode.D)) move += Vector3.right;

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);

            move = move.normalized * speed;
            rigidbody.linearVelocity = new Vector3(move.x, rigidbody.linearVelocity.y, move.z);
        }
        else
        {
            rigidbody.linearVelocity = new Vector3(0, rigidbody.linearVelocity.y, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _IsGrounded)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce, rigidbody.linearVelocity.z);
            _FirstJump = true;
            _IsGrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _FirstJump)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce / 2f, rigidbody.linearVelocity.z);
            _FirstJump = false;
        }
    }

    private void GroundPound()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_IsGrounded)
        {
            _IsGroundPound = true;
        }

        if (_IsGroundPound && !_IsGrounded)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, -groundPoundForce, rigidbody.linearVelocity.z);
            _IsGroundPound = false;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !_IsAttacking && !_IsGroundPound)
        {
            _IsAttacking = true;
            attackDuration = 0f;
            Debug.Log("ataque");
        }

        if (_IsAttacking)
        {
            attackDuration += Time.deltaTime;

            if (attackDuration > 1f)
            {
                _IsAttacking = false;
                attackDuration = 0f;
                Debug.Log("Fin ataque");
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        _IsGrounded = true;
    }
}
