using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    [Header("Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundPoundForce;
    [SerializeField]private float attackDuration = 0f;
    [SerializeField] private float fallMultiplier = 2.5f;
    // Checks
    [Header("Checks")]
    [SerializeField] public bool _IsGroundPound = false;
    [SerializeField] private bool _FirstJump = false;
    [SerializeField] private bool _IsGrounded = false;
    [SerializeField] public bool _IsAttacking = false;
    [SerializeField]public bool _IsGettingHit = false;

    [Header("AttackColision")]
    //GameObject attackColision;
    // Animations
    [SerializeField] private Animator animator;
    
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //attackColision.gameObject.SetActive(false);
    }

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

        if(!_IsGroundPound)
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, (Input.GetAxisRaw("Vertical")));
        move = move.normalized * speed;
        rigidbody.linearVelocity = new Vector3(move.x, rigidbody.linearVelocity.y, move.z);

        // Update walk/run animation
        animator.SetFloat("Speed", move.magnitude);

        if (move != Vector3.zero)
        {
            Vector3 rotation = new Vector3(move.x, 0, move.z);
            Quaternion targetRotation = Quaternion.LookRotation(rotation);
            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,10*Time.deltaTime);

            //move = move.normalized * speed;
            //rigidbody.linearVelocity = new Vector3(move.x, rigidbody.linearVelocity.y, move.z);
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
            // Trigger jump animation
            animator.SetBool("IsJumping", true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _FirstJump)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce / 2f, rigidbody.linearVelocity.z);
            _FirstJump = false;
        }
        // Aumentar gravedad cuando cae
        if (rigidbody.linearVelocity.y < 0)
        {
            rigidbody.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void GroundPound()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_IsGrounded)
        {
            _IsGroundPound = true;
            animator.SetTrigger("GroundPound");

            // Aplicar fuerza hacia abajo
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, -groundPoundForce, rigidbody.linearVelocity.z);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !_IsAttacking && !_IsGroundPound)
        {
            //attackColision.gameObject.SetActive(true);
            _IsAttacking = true;
            attackDuration = 0f;
            Debug.Log("ataque");
            // Trigger attack animation
            animator.SetTrigger("Attack");
        }

        if (_IsAttacking)
        {
            attackDuration += Time.deltaTime;

            if (attackDuration > 1f)
            {
                _IsAttacking = false;
                attackDuration = 0f;
                //attackColision.gameObject.SetActive(false);
                Debug.Log("Fin ataque");
            }
        }
    }
    public void BoxEnemyJump()
    {
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce, rigidbody.linearVelocity.z);
    }
    public IEnumerator GetHit()
    {
        _IsGettingHit = true;
        GameManager.instance.LoseLife();
        //animacion de danio
        yield return new WaitForSeconds(1);
        _IsGettingHit = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        _IsGrounded = true;
        _IsGroundPound = false;
        // End jump animation when we land
        animator.SetBool("IsJumping", false);
    }
}
