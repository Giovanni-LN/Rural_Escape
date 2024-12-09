using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject door;
    private Animator doorAnimator;

    private void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("clicked", true);
        if (door != null)
        {
            doorAnimator.SetTrigger("open");
        }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("clicked", false);
    }
    private void OnValidate()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
}
