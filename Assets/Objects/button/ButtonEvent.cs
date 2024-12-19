using UnityEngine;
public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] bool isOpen;
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
            doorAnimator.SetTrigger("clicked");
            doorAnimator.SetBool("open", isOpen);
            isOpen = !isOpen;
            Invoke("OnDestroyDor", 1f);
        }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("clicked", false);
    }
    public void OnDestroyDor()
    {
        Destroy(door);
    }
    private void OnValidate()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
}
