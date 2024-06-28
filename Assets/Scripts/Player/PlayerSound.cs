using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : InputHandler
{
    public void PlayWalkingSound()
    {
        AudioManager._Instance.Play("Walking");
    }

    public void PlayVineSound()
    {
        AudioManager._Instance.Play("ClimbVine");
    }
}
