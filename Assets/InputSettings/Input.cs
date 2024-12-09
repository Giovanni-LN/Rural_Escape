using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public static Input instance;   
    public InputSystem_Actions input;

    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;
        }
        input = new InputSystem_Actions();
        input.Enable();
    }

    public void Register_HorizontalMove_Callback(Action<InputAction.CallbackContext> moveCallback) // essa é a estrutura padrão para adicionar eventos para o input system
    {
        input.Player.Move.performed += moveCallback; // registra a ação de permanecer clicando no botão
        input.Player.Move.canceled += moveCallback; // Registra também para o evento quando o botão for solto
    }
    public void UnRegister_HorizontalMove_Callback(Action<InputAction.CallbackContext> moveCallback) // remove os eventos em espera na memória
    {
        input.Player.Move.performed -= moveCallback;
        input.Player.Move.canceled -= moveCallback;
    }

    public void Register_Jump_Callback(Action<InputAction.CallbackContext> jumpCallback)
    {
        input.Player.Jump.performed += jumpCallback;
    }
    public void UnRegister_Jump_Callback(Action<InputAction.CallbackContext> jumpCallback)
    {
        input.Player.Jump.performed -= jumpCallback;
    }

    public void Register_NormalAttack_Callback(Action<InputAction.CallbackContext> attackCallback)
    {
        input.Player.Attack.performed += attackCallback;
    }
    public void UnRegister_NormalAttack_Callback(Action<InputAction.CallbackContext> attackCallback)
    {
        input.Player.Attack.performed -= attackCallback;
    }

}
