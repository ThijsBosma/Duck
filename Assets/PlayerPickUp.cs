using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : FindInputBinding
{
    private PlayerInteract _PlayerInteract;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerInteract = GetComponent<PlayerInteract>();
    }

    private void OnValidate()
    {
        _PlayerInteract = GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
