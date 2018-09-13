using UnityEngine;
using System.Collections;

public class PlayParticlesOnEnable : MonoBehaviour 
{
    [SerializeField] ParticleSystem[] particleSystems;

    void OnEnable()
    {
        for (int i = 0; i < particleSystems.Length; ++i)
        {
            particleSystems[i].Play();
        }
    }
}
