using UnityEngine;

public class SimpleVerticalMovement : MonoBehaviour
{
    public float moveDistance = 5f; // Dist�ncia total que o personagem percorrer�
    public float speed = 0.1f; // Velocidade do movimento

    private float startPositionY;
    private int direction = 1;



    void Start()
    {
        // Armazena a posi��o inicial
        startPositionY = transform.position.y;
       
    }

    void FixedUpdate()
    {
        // Move o personagem na dire��o atual
        transform.Translate(Vector3.up * direction * speed);

        // Verifica se atingiu o limite e inverte a dire��o
        if (Mathf.Abs(transform.position.y - startPositionY) >= moveDistance)
        {
            direction *= -1; // Inverte a dire��o
        }
    }
}


