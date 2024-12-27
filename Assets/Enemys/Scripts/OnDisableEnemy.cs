using UnityEngine;

public class OnDisableEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask hitDamageCheck;
    private Animator animator;
    public int life = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnDestroy()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & hitDamageCheck) != 0)
        {
            life--;
            if(life <=0)
                animator.SetTrigger("death");
        }
    }
}

