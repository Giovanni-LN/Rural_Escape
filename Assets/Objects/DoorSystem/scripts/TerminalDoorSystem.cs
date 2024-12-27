using UnityEngine;

public class TerminalDoorSystem : MonoBehaviour
{
    public Animator doorAnimation;
    public GameObject myChip;
    public string idChip;
    public bool isActivade;
    private void Start()
    {
        myChip.gameObject.GetComponent<ChipTerminal>().idChip = idChip;
    }
  
    public void OpenTheDoor()
    {
        if (!isActivade)
        {
            doorAnimation.SetBool("open", false);
            doorAnimation.SetTrigger("clicked");
            isActivade = true;
        }       
    }
}
