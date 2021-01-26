using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    [Header("General References")]
    public AudioMixerGroup sfxGroup;
    public AudioSource generalAudioSource;

    [Header("SFXs")]
    public AudioClip cubeCollectSFX1;
    public AudioClip cubeCollectSFX2;
    public AudioClip cubeLoseSFX1;
    public AudioClip gemCollectSFX1;

    private void Awake()
    {
        _instance = this;

    } // Awake()

    private void Start()
    {
        InitPrefValues();

    } // Start()

    void InitPrefValues()
    {
        if (PlayerPrefs.HasKey("SFXOff"))
        {
            int tmp = PlayerPrefs.GetInt("SFXOff");

            if (tmp == 0)
            {
                UnMuteSFX(false);
            }
            else
            {
                MuteSFX(false);
            }
        }

    } // InitPrefValues()

    public void PlayCubeCollectSFX()
    {
        int tmpRnd = Random.Range(0, 2);

        if (tmpRnd == 0)
        {
            generalAudioSource.PlayOneShot(cubeCollectSFX1);
        }
        else
        {
            generalAudioSource.PlayOneShot(cubeCollectSFX2);
        }

    } // PlaySFXTest()

    public void PlayCubeLoseSFX()
    {
        generalAudioSource.PlayOneShot(cubeLoseSFX1);

    } // PlayCubeLoseSFX()

    public void PlayGemCollectSFX()
    {
        generalAudioSource.PlayOneShot(gemCollectSFX1);

    } // PlayGemCollectSFX()

    public void MuteSFX(bool _savePref)
    {
        generalAudioSource.mute = true;

        if (_savePref)
        {

            PlayerPrefs.SetInt("SFXOff", 1);
        }

    } // MuteSFX()

    public void UnMuteSFX(bool _savePref)
    {
        generalAudioSource.mute = false;

        if (_savePref)
        {

            PlayerPrefs.SetInt("SFXOff", 0);
        }

    } // UnMuteSFX()

} // class
