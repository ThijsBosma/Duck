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
                string buttonName = ExtractInputName(bindingForControlScheme.Value.path);
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

    protected string FindIconBinding(string typeOfAction)
    {
        if (typeOfAction == "Interact")
            _action = playerInput.actions.FindAction("Interact");
        else if (typeOfAction == "Pickup")
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
                if (controlScheme == "PlaystationController")
                {
                    return ExtractIconName(bindingForControlScheme.Value.path, "IconFilled");
                }
                else if (controlScheme == "XboxController" || controlScheme == "Gamepad")
                {
                    return ExtractIconName(bindingForControlScheme.Value.path, "IconFilled");
                }
            }
        }

        return null;
    }

    private string ExtractInputName(string bindingPath)
    {
        string[] splitPath = bindingPath.Split('/');
        if (splitPath.Length > 1)
            return splitPath[splitPath.Length - 1].ToUpperInvariant();
        else
            return "Unknown Button";
    }

    /// <summary>
    /// Extracts the name of the icon
    /// </summary>
    /// <param name="bindingPath">The path of the input</param>
    /// <param name="iconType">Type of icon that needs to be shown. iconType can have a different types per icon:
    /// Icon, IconFilled, Color, ColorFilled. iconTypes are case sensitive.
    /// </param>
    /// <returns></returns>
    private string ExtractIconName(string bindingPath, string iconType)
    {
        string[] splitPath = bindingPath.Split('/');
        if (splitPath.Length > 1)
        {
            return "<sprite name=" + '"' + splitPath[splitPath.Length - 1] + iconType +'"' + ">";
        }
        else
            return "Unknown Button";
    }

    protected void SetText(string text, bool needsBindingReference, string inputType = default)
    {
        string controlScheme = playerInput.currentControlScheme;

        if (needsBindingReference)
        {
            if (controlScheme == "PlaystationController" || controlScheme == "XboxController" || controlScheme == "Gamepad")
            {
                InteractText.instance.SetText($"Press {FindIconBinding(inputType)} {text}");
            }
            else
            {
                InteractText.instance.SetText($"Press {FindBinding(inputType)} {text}");
            }
        }
        else if(inputType == default)
        {
            InteractText.instance.SetText($"{text}");
        }
    }
}
