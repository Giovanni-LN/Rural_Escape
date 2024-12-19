using Unity.VisualScripting;
using UnityEngine;

public class PickEnergi : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Get");
        }
    }
    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
    public void EnableGameObject()
    {
        gameObject.SetActive(true);
    }
}
