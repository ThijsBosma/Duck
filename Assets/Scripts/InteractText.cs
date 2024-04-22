using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractText : MonoBehaviour
{
    public static InteractText instance;

    [SerializeField] private TextMeshProUGUI text;

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
        text.text = "";
    }
}
