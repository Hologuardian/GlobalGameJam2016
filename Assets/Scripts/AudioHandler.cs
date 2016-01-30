using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

    //USE LIKE THIS
    //AudioHandler.Instance.MethodName();
    //Need a 
    public AudioSource m_BackgroundSource;
    public AudioSource m_SoundEffectSource;


    public static AudioHandler Instance { get; private set; }

    [Header("BGM Tracks")]
    public AudioClip m_Spoops;
    public AudioClip m_Tribal;

    [Header("SFX Tracks")]
    public AudioClip m_ButtonOne;
    public AudioClip m_ButtonTwo;
    public AudioClip m_ButtonThree;
    public AudioClip m_ButtonFour;

    [Header("Male Screams")]
    public AudioClip m_MaleScreamOne;
    public AudioClip m_MaleScreamTwo;
    public AudioClip m_MaleScreamThree;

    [Header("Female Screams")]
    public AudioClip m_FemaleScreamOne;
    public AudioClip m_FemaleScreamTwo;
    public AudioClip m_FemaleScreamThree;


    public enum BackgroundMusic
    {
        BGMSPOOPY,
        BGMTRIBAL,
        BGMMENU
    }

    public enum ButtonEffects
    {
        SFXBUTTONONE,
        SFXBUTTONTWO,
        SFXBUTTONTHREE,
        SFXBUTTONFOUR
    }

    public enum PainedScreams
    {
        MALESCREAMONE,
        MALESCREAMTWO,
        MALESCREAMTHREE,
        FEMALESCREAMONE,
        FEMALESCREAMTWO,
        FEMALESCREAMTHREE
    }

    //Maybe moans of joy?
    public enum MoansOfMurderousJoy
    {
        NONE
    }

    void Awake()
    {
        AudioSource[] aSource = GetComponents<AudioSource>();

        m_BackgroundSource = aSource[0];
        m_SoundEffectSource = aSource[1];

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        m_BackgroundSource.clip = m_Tribal;
        m_BackgroundSource.Play();
    }



}
