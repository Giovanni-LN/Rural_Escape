using UnityEngine;

public class NormalShotController : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float shotSpeed;
    [SerializeField] private float lifetime;
    [SerializeField] public float direction;
    [SerializeField] private Vector3 directionVector;

    private void Start()
    {
        if (direction > 0)
            directionVector = Vector3.right;
        else directionVector = Vector3.left;
        transform.localScale = new Vector3(direction, 1, 1);
        Invoke(nameof(OnDrestroyShot), lifetime);
    }
    private void FixedUpdate()
    {
        transform.Translate(directionVector * shotSpeed);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {          
        animator.SetTrigger("hit");
        shotSpeed /= 5;       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            animator.SetTrigger("hit");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grond"))
        {
            animator.SetTrigger("hit");
        }
    }
    public void OnDrestroyShot()
    {
        Destroy(gameObject);
    }
}
