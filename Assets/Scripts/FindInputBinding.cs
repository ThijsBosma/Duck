using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FindInputBinding : InputHandler
{
    [SerializeField] private Image inputIcon;

    [SerializeField] private GamepadIcons ps4Icons;
    [SerializeField] private GamepadIcons xboxIcons;

    private void Update()
    {
        if (playerInput.currentControlScheme.Equals("Gamepad"))
        {
            Debug.Log("Playerstation");
        }
        else if (playerInput.currentControlScheme.Equals("Gamepad"))
        {
            Debug.Log("Xbox");
        }
    }

    protected string FindBinding()
    {
        InputAction interactAction = playerInput.actions.FindAction("Interact");

        if (interactAction != null)
        {
            string controlScheme = playerInput.currentControlScheme;

            InputBinding? bindingForControlScheme = null;

            foreach (var binding in interactAction.bindings)
            {
                if (binding.groups.Contains(controlScheme))
                {
                    bindingForControlScheme = binding;
                    break;
                }
            }

            if (bindingForControlScheme != null)
            {
                
                string buttonName = ExtractButtonName(bindingForControlScheme.Value.path);
                return buttonName;
            }
            else
            {
                return "No binding for current control scheme";
            }
        }
        else
        {
            Debug.LogError("Player input not found");
            return "";
        }
    }

    protected Image FindIconBinding()
    {
        string buttonName = FindBinding();
        return null;
    }

    protected string ExtractButtonName(string bindingPath)
    {
        string[] splitPath = bindingPath.Split('/');
        if (splitPath.Length > 1)
        {

            return splitPath[splitPath.Length - 1].ToUpperInvariant();
        }
        else
        {
            return "Unknown Button";
        }
    }
}
