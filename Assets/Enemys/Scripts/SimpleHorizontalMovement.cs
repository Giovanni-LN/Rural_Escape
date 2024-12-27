using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float moveDistance = 5f; // Dist�ncia total que o personagem percorrer�
    public float speed = 0.1f; // Velocidade do movimento

    private float startPositionX;
    private int direction = 1; // 1 = direita, -1 = esquerda
    private SpriteRenderer spriteRenderer;
    
    

    void Start()
    {
        // Armazena a posi��o inicial
        startPositionX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Move o personagem na dire��o atual
        transform.Translate(Vector3.right * direction * speed);

        // Verifica se atingiu o limite e inverte a dire��o
        if (Mathf.Abs(transform.position.x - startPositionX) >= moveDistance)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            direction *= -1; // Inverte a dire��o
        }
    }   
}

