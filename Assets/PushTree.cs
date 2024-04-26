using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTree : InputHandler, IInteractable
{
    [SerializeField] public Transform _BridgePosition;

    [SerializeField] private float _MoveTime;

    [SerializeField] private AnimationCurve _Curve;

    private float _time;

    private bool _canInteract;
    public bool _hasInteracted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_Interact.IsPressed() && _canInteract && !_hasInteracted)
        {
            transform.position = _BridgePosition.position;
            transform.rotation = _BridgePosition.rotation;
            _hasInteracted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasInteracted)
        {
            _Interact.Enable();
            Interact();
            _canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasInteracted)
        {
            _Interact.Enable();
            InteractText.instance.ResetText();
            _canInteract = false;
        }
    }

    public void Interact()
    {
        InteractText.instance.SetText("Press F to push tree");
    }
}
