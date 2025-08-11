using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    [Header("Variables")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundPoundForce;
    [SerializeField] private float attackDuration = 0f;
    [SerializeField] private float fallMultiplier = 2.5f;
    // Checks
    [Header("Checks")]
    [SerializeField] public bool _IsGroundPound = false;
    [SerializeField] public bool _FirstJump = false;
    [SerializeField] public bool _IsGrounded = false;
    [SerializeField] public bool _IsAttacking = false;
    [SerializeField] public bool _IsGettingHit = false;

    [Header("AttackColision")]
    [SerializeField]private GameObject attackCollision;
    [SerializeField]private GameObject floorCollision;
    // Animations
    [Header("Cinemachine")]
    [SerializeField]private CinemachineCamera cinemachineCamera;
    [SerializeField]private CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] private float amplitud;
    [SerializeField] private float frecuency;
    [SerializeField] private Animator animator;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        attackCollision.gameObject.SetActive(false);
        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
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

        if (!_IsGroundPound)
            move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, (Input.GetAxisRaw("Vertical")));
        move = move.normalized * speed;
        rigidbody.linearVelocity = new Vector3(move.x, rigidbody.linearVelocity.y, move.z);

        // Update walk/run animation
        animator.SetFloat("Speed", move.magnitude);

        if (move != Vector3.zero)
        {
            Vector3 rotation = new Vector3(move.x, 0, move.z);
            Quaternion targetRotation = Quaternion.LookRotation(rotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * Time.deltaTime);

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
            attackCollision.gameObject.SetActive(true);

            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, -groundPoundForce, rigidbody.linearVelocity.z);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !_IsAttacking && !_IsGroundPound)
        {
            attackCollision.gameObject.SetActive(true);
            _IsAttacking = true;
            attackDuration = 0f;
            Debug.Log("ataque");
            // Trigger attack animation
            animator.SetTrigger("Attack");
            if (!_IsGettingHit)
            {
                noise.AmplitudeGain = amplitud;
                noise.FrequencyGain = frecuency;
            }
        }

        if (_IsAttacking)
        {
            attackDuration += Time.deltaTime;

            if (attackDuration > 1f)
            {
                _IsAttacking = false;
                attackDuration = 0f;
                attackCollision.gameObject.SetActive(false);
                Debug.Log("Fin ataque");
                noise.AmplitudeGain = 0f;
                noise.FrequencyGain = 0f;
            }
        }
    }
    public void BoxEnemyJump()
    {
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, jumpForce, rigidbody.linearVelocity.z);
        animator.SetBool("IsJumping", true);
    }
    public IEnumerator GetHit()
    {
        _IsGettingHit = true;
        GameManager.instance.LoseLife();
        Debug.Log("Recibir danio");
        if (!_IsAttacking)
        {
            noise.AmplitudeGain = 3.7f;
            noise.FrequencyGain = 0.08f;
        }
        //animacion de danio
        yield return new WaitForSeconds(1);
        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
        _IsGettingHit = false;
    }
    //Checar suelo
    void OnCollisionEnter(Collision collision)
    {

        if (_IsGroundPound == true)
        {
            StartCoroutine(GroundPoundFinishing());
        }
        else
        {
            _IsGrounded = true;
            _FirstJump = false;
            animator.SetBool("IsJumping", false);
        }
    }
    IEnumerator GroundPoundFinishing()
    {
        noise.AmplitudeGain = amplitud;
        noise.FrequencyGain = frecuency;
        yield return new WaitForSeconds(.5f);
        _IsGrounded = true;
        _IsGroundPound = false;
        attackCollision.gameObject.SetActive(false);
        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
        animator.SetBool("IsJumping", false);
    }
}
