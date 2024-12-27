using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerControllerUI : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image pvImage;    
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    private bool pauseGame;

    private void OnEnable()
    {
       Input.instance.Register_PauseGame_Callback(OnPauseGame);
    }
    private void OnDisable()
    {
        Input.instance.UnRegister_PauseGame_Callback(OnPauseGame);
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.OnGameOver += OnGamerOver;
    }
    private void OnPauseGame(InputAction.CallbackContext pauseCallback)
    {
        if (pauseCallback.started)
        {
            if (!pauseGame)
            {
                Time.timeScale = 0f;
                pauseGame = true;
                PausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pauseGame = false;
                PausePanel.SetActive(false);
            }
        }
    }

    private void OnInteractable(InputAction.CallbackContext interactableCallback)
    {
        if (playerController.onTerminal)
        {

        }
    }
    public void FixedUpdate()
    {
        UpdatePvImage();
    }
    public void OnGamerOver()
    {       
        GameOverPanel.SetActive(true);
    }
    public void UpdatePvImage()
    {
        pvImage.fillAmount = playerController.PV / 100;
    }

}
