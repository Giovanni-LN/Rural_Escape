using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Collider2D collider2;
    private Animator animator;
    private Vector2 inputMove;
    private bool doubleJump, doubleJumpActived;
    [SerializeField] private bool canShot, canSlide;
    private bool onGrounded;
    private bool onSlide;
    private bool pauseGame;

    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float slideForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private bool climbDetected;
    [SerializeField] private bool onClinb;

    
    [SerializeField] private float pv = 50;
    [SerializeField] public Image pvImage;

    [SerializeField] GameObject PausePanel, GameOverPanel;
    public Transform PlayerTransform => PlayerTransform;
    public float PV { get { return pv; } set { pv = value; } }
    public bool CanShot => canShot;
    public bool CanSlide => canSlide;

    private void OnEnable()
    {
        Input.instance.Register_HorizontalMove_Callback(OnRunning); // registra para o evento do input system
        Input.instance.Register_Jump_Callback(OnJump);
        Input.instance.Register_Slide_Callback(OnSlide);
        Input.instance.Register_PauseGame_Callback(OnPauseGame);
    }
    private void OnDisable()
    {
        Input.instance.UnRegister_HorizontalMove_Callback(OnRunning); // remove o registro para o evento do input system
        Input.instance.UnRegister_Jump_Callback(OnJump);
        Input.instance.UnRegister_Slide_Callback(OnSlide);
        Input.instance.UnRegister_PauseGame_Callback(OnPauseGame);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2 = GetComponent<Collider2D>();        
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.y) != 0.1) // Verifica se existe velocidade vertical
        {
            animator.SetFloat("verticalVelocity", rb.linearVelocityY);
        }
        onGrounded = Physics2D.BoxCast(transform.position - new Vector3(0, 0.9f, 0), new Vector2(0.9f, 0.1f), 0, Vector2.down, 0.4f, groundLayer).collider != null;
        animator.SetBool("ground", onGrounded);
        if (onGrounded && doubleJumpActived && !doubleJump)
        {
            doubleJump = doubleJumpActived;
        }
    }
    private void FixedUpdate()
    {
        if (inputMove.x != 0 && !onSlide)
        {
            rb.linearVelocityX = inputMove.x * speed;
        }
        else if (inputMove.y != 0 && climbDetected)
        {
            onClinb = true;
            rb.gravityScale = 0;
            rb.linearVelocityY = inputMove.y * speed;
            animator.SetBool("climb", onClinb);
        }
        pv -= 0.01f;
        UpdatePvImage();
        if (pv <= 0)
        {
            OnGamerOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DeathZone"))
        {            
            animator.SetTrigger("getHit");
            OnGamerOver();
        }
        if (collision.gameObject.CompareTag("PlayerDoubleJumpActive"))
        {
            doubleJumpActived = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PlayerShotActive"))
        {
            canShot = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PlayerSlideActive"))
        {
            canSlide = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Climb"))
        {
            climbDetected = true;
        }
        if (collision.CompareTag("Damage"))
        {
            pv -= Random.Range(1, 10);
        }
        if (collision.CompareTag("GetPv"))
        {
            pv += Random.Range(10, 20);
            if (pv > 100)
            {
                pv = 100;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Climb"))
        {
            climbDetected = false;
            animator.SetBool("climb", false);
            if (onClinb)
            {
                rb.gravityScale = 3f;
                onClinb = false;
            }
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
        if (onGrounded)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
        else if (!onGrounded && doubleJump)
        {
            rb.linearVelocityY = 0;
            rb.AddForceY(doubleJumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            doubleJump = false;
        }
    }
    private void OnSlide(InputAction.CallbackContext slideCallback)
    {
        if (onGrounded && canSlide)
        {
            rb.linearVelocityX = 0;
            rb.gravityScale = 0;
            collider2.isTrigger = true;
            animator.SetTrigger("slide");
            onSlide = true;
            rb.AddForceX(transform.localScale.x * slideForce, ForceMode2D.Impulse);            
        }
    }
    public void StopSlide()
    {
        rb.linearVelocityX = 0;
        rb.gravityScale = 3;
        collider2.isTrigger = false;
        onSlide = false;
        
    }
    private void OnPauseGame(InputAction.CallbackContext pauseCallback)
    {
        if (pauseCallback.started)
        {
            if (!pauseGame)
            {
                Time.timeScale = 0f;
                print("acessou 1");
                pauseGame = true;
                PausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                print("acessou 2");
                pauseGame = false;
                PausePanel.SetActive(false);
            }
        } 
    }
    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    public void OnGamerOver()
    {
        animator.SetBool("death", true);
        GameOverPanel.SetActive(true);
        gameObject.SetActive(false);      
        
    }
    public void UpdatePvImage()
    {
        pvImage.fillAmount = pv/100;
    }
}
