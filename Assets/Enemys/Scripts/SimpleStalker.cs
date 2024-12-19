using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Velocidade de movimento do inimigo
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask hitDamageCheck;
    private Transform playerTransform; // Referência ao Transform do jogador
    Animator animator;
    [SerializeField] float timeLife;
    
    
    private bool flip;
    

    void Start()
    {
        // Encontra o jogador na cena pela tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player não encontrado na cena!");
        }
    }
  
    private void FixedUpdate()
    {        
        // Se o Transform do jogador foi encontrado, mova o inimigo em direção ao jogador
        if (playerTransform != null)
        {
            // Direção para o jogador
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            if ((direction.x > 0 && !flip) || (direction.x < 0 && flip)) // muda a orientação horizonal do personagem
            {   
                flip = !flip;
                spriteRenderer.flipX = flip;
                
            }
            // Movimenta o inimigo em direção ao jogador
            transform.position += direction * speed;
        }
        timeLife += .01f;
        if(timeLife >= 3)
        {
            animator.SetTrigger("death");
        }
    }
    private void OnValidate()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hitDamageCheck) != 0 || collision.CompareTag("Player"))
        {
            animator.SetTrigger("death");
        }
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

