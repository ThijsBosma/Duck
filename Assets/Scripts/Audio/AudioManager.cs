using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] Sounds;
    [SerializeField] private AudioMixerGroup sfxMixer;

    public static AudioManager _Instance;

    private void Awake()
    {
        _Instance = this;

        foreach (Sound s in Sounds)
        {
            s._Source = gameObject.AddComponent<AudioSource>();
            s._Source.clip = s._clip;
            s._Source.outputAudioMixerGroup = s._mixerOutput;
            s._Source.loop = s._Loop;
            s._Source.volume = s.Volume;
            s._Source.pitch = s.Pitch;
            s._Source.spatialBlend = s.SpatialBend;

            if (s._PlayOnAwake)
                Play(s._soundName);
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, x => x._soundName == name);
        s._Source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(Sounds, x => x._soundName == name);
        s._Source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, x => x._soundName == name);
        s._Source.Stop();
    }

    public bool SoundIsPlaying(string name)
    {
        Sound s = Array.Find(Sounds, x => x._soundName == name);
        return s._Source.isPlaying;
    }
}
