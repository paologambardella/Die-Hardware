using UnityEngine;
using System.Collections;

public class PlayAudioOnEnable : MonoBehaviour 
{
    [SerializeField] AudioSource audioSource;

    void OnEnable()
    {
        audioSource.Play();
    }
}
