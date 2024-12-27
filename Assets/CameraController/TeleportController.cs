using Unity.Cinemachine;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform exitTeleport;
    public CinemachineConfiner2D confiner;
    public Collider2D newLimitCamera;
    public GameObject myGrid, gridExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = 
                new Vector3(exitTeleport.transform.position.x, exitTeleport.transform.position.y, exitTeleport.transform.position.z) ;
            confiner.BoundingShape2D = newLimitCamera;
            gridExit.SetActive(true);
            myGrid.SetActive(false);
        }
    }
}
