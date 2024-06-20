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

    private string[] _UITexts;

    protected override void Awake()
    {
        base.Awake();
        _UITexts = new string[_Texts.Length];
        SaveTexts();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIIcons(playerInput);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        //Debug.Log(GetCurrentControlScheme(pi));
        UpdateUIIcons(pi);
    }

    private void SaveTexts()
    {
        for (int i = 0; i < _Texts.Length; i++)
        {
            _UITexts[i] = _Texts[i].text;
        }
    }

    private string GetSpriteAssetText(string text)
    {
        for (int i = 0; i < _UITexts.Length; i++)
        {
            if (_UITexts[i].Contains(text))
            {
                string[] splitText = _UITexts[i].Split('"');
                return splitText[0];
            }
        }
        return "";
    }

    public string GetCurrentControlScheme(PlayerInput pi)
    {
        string currentScheme = pi.currentControlScheme;

        if (currentScheme == "PlaystationController")
            return "PS_";
        else if (currentScheme == "XboxController" || currentScheme == "Gamepad")
            return "Xbox_";
        else
            return "Keyboard_";
    }

    private string ExtractActiontName(string text)
    {
        string[] splitText = text.Split(">");

        for (int i = 0; i < splitText.Length; i++)
        {
            Debug.Log($"splitted text {i}: {splitText[1]}");
        }

        string[] splittedText = splitText[1].Split(" ");

        if (splittedText[1] == "Select")
            return "Interact";
        else
            return splittedText[1];
    }

    private string ExtractInputBinding(PlayerInput playerInput, string text)
    {
        Debug.Log("Action text: " + text);

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

    public void UpdateUIIcons(PlayerInput playerInput)
    {
        for (int i = 0; i < _Texts.Length; i++)
        {
            string spriteAssetName = GetSpriteAssetText(_UITexts[i]) + '"';
            string currentControlScheme = GetCurrentControlScheme(playerInput);
            string inputBinding = ExtractInputBinding(playerInput, ExtractActiontName(_UITexts[i])) + '"' + "> ";
            string actionName = ExtractActiontName(_UITexts[i]);

            Debug.Log(spriteAssetName);
            Debug.Log(currentControlScheme);
            Debug.Log(inputBinding);
            Debug.Log(actionName);

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

                Debug.Log("using controller");



                _Texts[i].text = spriteAssetName + currentControlScheme + inputBinding + actionName;
            }
        }
    }
}
