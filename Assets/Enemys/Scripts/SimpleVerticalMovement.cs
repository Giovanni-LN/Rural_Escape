using UnityEngine;

public class SimpleVerticalMovement : MonoBehaviour
{
    public float moveDistance = 5f; // Distância total que o personagem percorrerá
    public float speed = 0.1f; // Velocidade do movimento

    private float startPositionY;
    private int direction = 1;



    void Start()
    {
        // Armazena a posição inicial
        startPositionY = transform.position.y;
       
    }

    void FixedUpdate()
    {
        // Move o personagem na direção atual
        transform.Translate(Vector3.up * direction * speed);

        // Verifica se atingiu o limite e inverte a direção
        if (Mathf.Abs(transform.position.y - startPositionY) >= moveDistance)
        {
            direction *= -1; // Inverte a direção
        }
    }
}


