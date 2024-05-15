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

    private InputBinding? bindingForControlScheme = null;

    protected string FindBinding()
    {
        InputAction interactAction = playerInput.actions.FindAction("Interact");

        if (interactAction != null)
        {
            string controlScheme = playerInput.currentControlScheme;

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
                Debug.Log(bindingForControlScheme.Value.path);
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
        string[] splitPath = bindingForControlScheme.Value.path.Split('/');
        string path = splitPath[1];
        string controlScheme = playerInput.currentControlScheme;

        switch (path)
        {
            case "buttonSouth": return buttonSouth;
            case "buttonNorth": return buttonNorth;
            case "buttonEast": return buttonEast;
            case "buttonWest": return buttonWest;
            case "start": return startButton;
            case "select": return selectButton;
            case "leftTrigger": return leftTrigger;
            case "rightTrigger": return rightTrigger;
            case "leftShoulder": return leftShoulder;
            case "rightShoulder": return rightShoulder;
            case "dpad": return dpad;
            case "dpad/up": return dpadUp;
            case "dpad/down": return dpadDown;
            case "dpad/left": return dpadLeft;
            case "dpad/right": return dpadRight;
            case "leftStick": return leftStick;
            case "rightStick": return rightStick;
            case "leftStickPress": return leftStickPress;
            case "rightStickPress": return rightStickPress;
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
