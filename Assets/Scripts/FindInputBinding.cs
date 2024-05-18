using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FindInputBinding : InputHandler
{
    [Header("Input Icons")]
    [SerializeField] private GamepadIcons ps4Icons;
    [SerializeField] private GamepadIcons xboxIcons;

    InputAction _action;

    protected string FindBinding(string typeOfAction)
    {
        if (typeOfAction == "Interact")
            _action = playerInput.actions.FindAction("Interact");
        else if(typeOfAction == "Pickup")
            _action = playerInput.actions.FindAction("Pickup");

        if (_action != null)
        {
            string controlScheme = playerInput.currentControlScheme;

            InputBinding? bindingForControlScheme = null;

            foreach (var binding in _action.bindings)
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

    protected Sprite FindIconBinding()
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
                string[] splitPath = bindingForControlScheme.Value.path.Split('/');
                string path = splitPath[1];

                Debug.Log(controlScheme);

                if (controlScheme == "PlaystationController")
                {
                    return ps4Icons.GetSprite(path);
                }
                else if (controlScheme == "XboxController" || controlScheme == "Gamepad")
                {
                    return xboxIcons.GetSprite(path);
                }
            }
        }

        return null;
    }

    protected string ExtractButtonName(string bindingPath)
    {
        string[] splitPath = bindingPath.Split('/');
        if (splitPath.Length > 1)
            return splitPath[splitPath.Length - 1].ToUpperInvariant();
        else
            return "Unknown Button";
    }
}
