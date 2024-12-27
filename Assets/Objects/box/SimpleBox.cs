using UnityEngine;

public class SimpleBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerShot"))
        {
            Destroy(gameObject);
        }
    }
}
