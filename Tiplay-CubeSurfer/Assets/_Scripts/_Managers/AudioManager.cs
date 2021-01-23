using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    [Header("General References")]
    public AudioMixerGroup sfxGroup;
    AudioSource generalAudioSource;

    [Header("SFXs")]
    public AudioClip sfxTest;

    private void Awake()
    {
        _instance = this;

    } // Awake()

    private void Start()
    {
        generalAudioSource = GetComponent<AudioSource>();

    } // Start()

    public void PlaySFXTest()
    {
        generalAudioSource.PlayOneShot(sfxTest);

    } // PlaySFXTest()

} // class
