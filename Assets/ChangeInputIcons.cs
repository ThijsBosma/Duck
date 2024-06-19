using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeInputIcons : FindInputBinding
{
    public TextMeshProUGUI[] _Texts;

    private InputAction _Action;

    // Start is called before the first frame update
    void Start()
    {
        //playerInput.onControlsChanged += 
        foreach (TextMeshProUGUI text in _Texts)
        {
            Debug.Log(GetSpriteAssetText(text.text) + '"' + GetCurrentControlScheme() + FindBinding("Interact") + '"' + ">");
            string inputAction = ExtractInputName(text.text);
            Debug.Log(GetInputAction(inputAction));
        }
    }

    private string GetSpriteAssetText(string text)
    {
        string[] splitText = text.Split('"');

        for (int i = 0; i < splitText.Length; i++)
        {
            return splitText[i];
        }
        return "";
    }

    private string GetCurrentControlScheme()
    {
        if (playerInput.currentControlScheme == "PlayStation")
        {
            return "PS_";
        }
        else if (playerInput.currentControlScheme == "Xbox" || playerInput.currentControlScheme == "Gamepad")
        {
            return "Xbox_";
        }
        else
        {
            return "Keyboard_";
        }
    }

    private string ExtractInputName(string text)
    {
        string[] splitText = text.Split(">");
        string[] splittedText = splitText[1].Split(" ");

        if (splittedText[1] == "Select")
            return "Interact";
        else
            return splittedText[1];
    }

    private string GetInputAction(string text)
    {
        _Action = playerInput.actions.FindAction(text);

        if (_Action != null)
        {
            string controlScheme = playerInput.currentControlScheme;

            InputBinding? bindingForControlScheme = null;

            foreach (var binding in _Action.bindings)
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
        return "Action not found";
    }

    private void UpdateUIIcons()
    {
        for (int i = 0; i < _Texts.Length; i++)
        {
            _Texts[i].text = GetSpriteAssetText(_Texts[i].text) + '"' + GetCurrentControlScheme() + '"' + ">";
        }
    }
}
