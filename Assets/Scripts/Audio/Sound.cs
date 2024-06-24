using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string _soundName;
    public AudioClip _clip;
    public AudioMixerGroup _mixerOutput;
    public bool _PlayOnAwake, _Loop;

    [Range(0, 1)] public float Volume = 1;
    [Range(-3, 3)] public float Pitch = 1;
    [Range(0, 1)] public float SpatialBend = .5f;
    [HideInInspector] public AudioSource _Source;
}