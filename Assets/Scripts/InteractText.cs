using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractText : InputHandler
{
    public static InteractText instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image _InputIcon;

    [SerializeField] private TMP_SpriteAsset _PSAsset;
    [SerializeField] private TMP_SpriteAsset _XboxAsset;

    public string _inputPath;
    public string _controlScheme;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    private void Start()
    {
        ResetText();
        _controlScheme = playerInput.currentControlScheme;
    }

    public void SetText(string displayText)
    {
        _controlScheme = playerInput.currentControlScheme;
        if (_controlScheme == "PlaystationController")
        {
            text.spriteAsset = _PSAsset;
        }
        else if (_controlScheme == "XboxController" || _controlScheme == "Gamepad")
        {
            text.spriteAsset = _XboxAsset;
        }
        else
        {
            text.spriteAsset = null;
        }

        text.text = displayText;
    }

    public void ResetText()
    {
        _InputIcon.sprite = null;

        Color iconColor = _InputIcon.color;
        iconColor.a = 0;
        _InputIcon.color = iconColor;

        text.text = "";
    }
}
