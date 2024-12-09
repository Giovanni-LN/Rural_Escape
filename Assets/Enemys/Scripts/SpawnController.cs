using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private Transform spanwPosition;
    [SerializeField] private GameObject enemyGameObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(enemyGameObject, spanwPosition.position, Quaternion.identity);
        }
    }
}
