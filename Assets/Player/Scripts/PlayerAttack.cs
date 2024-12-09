using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform shotSpawnPosition;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private NormalShotController normalShotController;

    private void OnEnable()
    {
        Input.instance.Register_NormalAttack_Callback(OnAttack);
    }
    private void OnDisable()
    {
        Input.instance.UnRegister_NormalAttack_Callback(OnAttack);
    }
    private void Start()
    {
        normalShotController = shotPrefab.GetComponent<NormalShotController>();
    }

    private void OnAttack(InputAction.CallbackContext attackCallback)
    {
        animator.SetTrigger("shot");
    }

    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
    }

    public void OnSpawnShot()
    {
        normalShotController.direction = transform.localScale.x;
        Instantiate(shotPrefab, shotSpawnPosition.position, shotSpawnPosition.rotation);
    }
}
