using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using System.Runtime.ExceptionServices;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool onGround;
    private Vector2 inputMove;
    private bool doubleJump, doubleJumpActived;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float hitForce;
    [SerializeField] private Transform checkPoint;

    public bool OnGround => onGround;
    public Transform PlayerTransform => PlayerTransform;
    private void OnEnable()
    {
        Input.instance.Register_HorizontalMove_Callback(OnRunning); // registra para o evento do input system
        Input.instance.Register_Jump_Callback(OnJump);
    }
    private void OnDisable()
    {
        Input.instance.UnRegister_HorizontalMove_Callback(OnRunning); // remove o registro para o evento do input system
        Input.instance.UnRegister_Jump_Callback(OnJump);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.y) != 0.1) // Verifica se existe velocidade vertical
        {
            animator.SetFloat("verticalVelocity", rb.linearVelocityY);
        }
    }
    private void FixedUpdate()
    {
        if (inputMove.x != 0)
        {
            rb.linearVelocityX = inputMove.x * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            animator.SetBool("ground", onGround);
            if (doubleJumpActived)
            {
                doubleJump = true;
            }
        }
        //  if (collision.gameObject.CompareTag("Bat"))
        // {
        //      rb.linearVelocity = new Vector2(0, 0);
        //      float hitDirection = -transform.localScale.x;
        //       rb.AddForceX(hitDirection * hitForce, ForceMode2D.Impulse);            
        //   }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            animator.SetBool("ground", onGround);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DeathZone"))
        {
            gameObject.transform.position = checkPoint.transform.position;
        }
        if (collision.gameObject.CompareTag("PlayerDoubleJumpActive"))
        {
            doubleJumpActived = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnRunning(InputAction.CallbackContext moveCallback)
    {
        inputMove = moveCallback.ReadValue<Vector2>();// o move, nesse caso é representado por um vector2 , eu usei o input system que já veio pré configurado 
        animator.SetBool("run", inputMove.x != 0);  // passar o valor boleado 'run' para o animator, lembrar que ele está verificando se a velocidade é diferente de 0(true para sim e false para não) 

        if (moveCallback.performed)
        {
            if (inputMove.x > 0 && transform.localScale.x < 0 || inputMove.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            }
        }
        else if (moveCallback.canceled)
        {
            // Para o movimento quando o input é cancelado
            rb.linearVelocityX = 0;
        }
    }
    private void OnJump(InputAction.CallbackContext jumpCallback)
    {
        if (onGround)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
        else if (!onGround && doubleJump)
        {
            rb.linearVelocityY = 0;
            rb.AddForceY(doubleJumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }
}
