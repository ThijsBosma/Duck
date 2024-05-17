using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Function happens when the interact button is pressed
    /// </summary>
    public void Interact();

    /// <summary>
    /// Function happens when the interact button is no longer pressed
    /// </summary>
    public void UnInteract();

    /// <summary>
    /// Checks to see if something has already been interacted with
    /// </summary>
    /// <returns></returns>
    public bool HasInteracted();
}
