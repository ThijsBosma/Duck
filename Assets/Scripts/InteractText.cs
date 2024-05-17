using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractText : MonoBehaviour
{
    public static InteractText instance;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image _InputIcon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetText();
    }

    public void SetText(string displayText)
    {
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
