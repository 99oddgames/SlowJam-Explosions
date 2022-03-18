using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VFXScreenshake
{
    public bool Play;

    [Range(0, 1f)]
    public float Trauma01;
    public float Decay;
}

[System.Serializable]
public struct VFXScreenFreeze
{
    public bool Play;
    public Delay Duration;
}

public class VFX : GameplayObject
{
    [Header("Screenshake")]

    public VFXScreenshake Screenshake;
    public VFXScreenFreeze ScreenFreeze;

    private AudioSource[] audioSources = null;
    private ParticleSystem[] particleSystems = null;

    private void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        if (Screenshake.Play)
        {
            GlobalEvents.SendMessage(Screenshake);
        }

        if(ScreenFreeze.Play)
        {
            ScreenFreeze.Duration.UseScaledTime = false;
            ScreenFreeze.Duration.Next();
            Time.timeScale = 0f;
        }
    }

    public void Update()
    {
        if (ScreenFreeze.Duration.IsUp)
        {
            Time.timeScale = 1f;
        }
        else
        {
            return;
        }

        foreach (var source in audioSources)
        {
            if (source.isPlaying)
                return;
        }

        foreach (var system in particleSystems)
        {
            if (system.isPlaying)
                return;
        }

        Despawn();
    }
}
