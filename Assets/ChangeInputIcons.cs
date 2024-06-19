using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeInputIcons : FindInputBinding
{
    public TextMeshProUGUI[] _Texts;

    [SerializeField] private GameObject _keyboardInputs;

    [SerializeField] private string _changeInteractTo;
    [SerializeField] private string _changePickupTo;

    private InputAction _Action;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIIcons();
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
        if (playerInput.currentControlScheme == "PlaystationController")
            return "PS_";
        else if (playerInput.currentControlScheme == "XboxController" || playerInput.currentControlScheme == "Gamepad")
            return "Xbox_";
        else
            return "Keyboard_";
    }

    private string ExtractActiontName(string text)
    {
        string[] splitText = text.Split(">");
        string[] splittedText = splitText[1].Split(" ");

        return splittedText[1];
    }

    private string ExtractInputBinding(string text)
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

    public void UpdateUIIcons()
    {
        for (int i = 0; i < _Texts.Length; i++)
        {
            string spriteAssetName = GetSpriteAssetText(_Texts[i].text) + '"';
            string currentControlScheme = GetCurrentControlScheme();
            string inputBinding = ExtractInputBinding(ExtractActiontName(_Texts[i].text)) + '"' + "> ";
            string actionName = ExtractActiontName(_Texts[i].text);

            if (actionName == "Interact")
                actionName = _changeInteractTo;
            else if (actionName == "Pickup")
                actionName = _changePickupTo;

            if (actionName == "Move" && currentControlScheme != "Keyboard_")
            {
                currentControlScheme = "";
                inputBinding = "Gamepad_L" + '"' + "> ";
            }
            else if (actionName == "Look" && currentControlScheme != "Keyboard_")
            {
                currentControlScheme = "";
                inputBinding = "Gamepad_R" + '"' + "> ";
            }

            if (currentControlScheme == "Keyboard_")
            {
                _keyboardInputs.SetActive(true);

                if (actionName == "Move" || actionName == "Look")
                    _Texts[i].text = "     " + actionName;
                else
                    _Texts[i].text = spriteAssetName + currentControlScheme + inputBinding + actionName;
            }
            else if (currentControlScheme != "Keyboard_")
            {
                _keyboardInputs.SetActive(false);

                _Texts[i].text = spriteAssetName + currentControlScheme + inputBinding + actionName;
            }
        }
    }
}
