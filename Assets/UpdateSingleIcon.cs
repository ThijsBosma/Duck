using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdateSingleIcon : FindInputBinding
{
    [SerializeField] private TextMeshProUGUI _IconText;
    [SerializeField] private string _InputBinding;

    private InputAction _Action;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UpdateIcon(playerInput);
    }

    private string GetSpriteAssetText(string text)
    {
        string[] splitText = text.Split('"');
        return splitText[0];
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
            Debug.Log($"splitted text {i}:{splitText[1]}");
        }

        string[] splittedText = splitText[1].Split(" ");

        for (int i = 0; i < splitText.Length; i++)
        {
            Debug.Log($"splitted text {i}:{splittedText[1]}");
        }

        return splittedText[1];
    }
    private string GetInputBinding(PlayerInput playerInput)
    {
        _Action = playerInput.actions.FindAction(_InputBinding);

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


    public void UpdateIcon(PlayerInput playerInput)
    {
        string spriteAssetName = GetSpriteAssetText(_IconText.text) + '"';
        string currentControlScheme = GetCurrentControlScheme(playerInput);
        string inputBinding = GetInputBinding(playerInput) + '"' + "> ";

        _IconText.text = spriteAssetName + currentControlScheme + inputBinding;
    }
}
