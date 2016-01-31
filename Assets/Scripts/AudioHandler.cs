using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour
{

    //USE LIKE THIS
    //AudioHandler.Instance.MethodName();
    //Need a 
    public AudioSource m_BackgroundSource;
    public AudioSource m_SoundEffectSource;


    public static AudioHandler Instance { get; private set; }

    [Header("BGM Tracks")]
    public AudioClip m_BackGroundSpoops;
    public AudioClip m_BackGroundTribal;

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

    [Header("Voice Overs")]
    public AudioClip m_DemonicMumblingOne;
    public AudioClip m_DemonicMumblingTwo;
    public AudioClip m_DemonicMumblingThree;
    public AudioClip m_DemonicYell;
    public AudioClip m_CheerfulGreeting;
    public AudioClip m_SadGreting;
    public AudioClip m_Grunt;
    public AudioClip m_Sigh;

    public enum BackgroundMusic
    {
        BGMSPOOPY,
        BGMTRIBAL
    }

    public enum ButtonEffects
    {
        SFXBUTTONONE,
        SFXBUTTONTWO,
        SFXBUTTONTHREE,
        SFXBUTTONFOUR
    }


    //TODO: Get Microphone
    //      Fill these enums out
    //      Record voiceovers from Jam participants (?)
    public enum VoiceOvers
    {
        MALESCREAMONE,
        MALESCREAMTWO,
        MALESCREAMTHREE,
        FEMALESCREAMONE,
        FEMALESCREAMTWO,
        FEMALESCREAMTHREE,
        DEMONICMUMBLINGONE,
        DEMONICMUMBLINGTWO,
        DEMONICMUMBLETHREE,
        DEMONICLAUGH,
        CHEERFULGREETING,
        SADGREETING,
        GRUNTS,
        SIGH
    }

    void Awake()
    {
        AudioSource[] aSource = GetComponents<AudioSource>();

        m_BackgroundSource = aSource[0];
        m_SoundEffectSource = aSource[1];

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic(BackgroundMusic bGM)
    {

        if (m_BackgroundSource.isPlaying)
        {
            m_BackgroundSource.Stop();
        }
        else
        {
            switch (bGM)
            {
                case BackgroundMusic.BGMSPOOPY:
                    m_BackgroundSource.clip = m_BackGroundSpoops;
                    m_BackgroundSource.Play();
                    break;

                case BackgroundMusic.BGMTRIBAL:
                    m_BackgroundSource.clip = m_BackGroundTribal;
                    m_BackgroundSource.Play();
                    break;

                default:
                    Debug.Log("BGM: Nothing playing");
                    break;
            }
            m_BackgroundSource.loop = true;

        }

    }

    //Plays a one shot of sound effects
    public void PlayButtonSound(ButtonEffects buttonFX)
    {
        switch (buttonFX)
        {
            case ButtonEffects.SFXBUTTONONE:
                m_SoundEffectSource.PlayOneShot(m_ButtonOne);
                break;

            case ButtonEffects.SFXBUTTONTWO:
                m_SoundEffectSource.PlayOneShot(m_ButtonTwo);
                break;


            case ButtonEffects.SFXBUTTONTHREE:
                m_SoundEffectSource.PlayOneShot(m_ButtonThree);
                break;

            case ButtonEffects.SFXBUTTONFOUR:
                m_SoundEffectSource.PlayOneShot(m_ButtonFour);
                break;

            default:
                Debug.Log("BUTTON: NOT PLAYING");
                break;
        }
    }


    //TODO: Implement Voice Over
    //      GET VOICE OVER FIRST. :l
    public void PlayVoiceOver(VoiceOvers voiceOvers)
    {

        switch (voiceOvers)
        {
            case VoiceOvers.DEMONICMUMBLINGONE:

                break;

            case VoiceOvers.DEMONICMUMBLINGTWO:

                break;

            case VoiceOvers.DEMONICMUMBLETHREE:

                break;

            case VoiceOvers.DEMONICLAUGH://(?),

                break;

            case VoiceOvers.CHEERFULGREETING:

                break;

            case VoiceOvers.SADGREETING:

                break;

            case VoiceOvers.GRUNTS:

                break;

            case VoiceOvers.SIGH:

                break;

            case VoiceOvers.FEMALESCREAMONE:
                m_SoundEffectSource.PlayOneShot(m_FemaleScreamOne);
                break;

            case VoiceOvers.FEMALESCREAMTWO:
                m_SoundEffectSource.PlayOneShot(m_FemaleScreamTwo);
                break;

            case VoiceOvers.FEMALESCREAMTHREE:
                m_SoundEffectSource.PlayOneShot(m_FemaleScreamThree);
                break;

            case VoiceOvers.MALESCREAMONE:
                m_SoundEffectSource.PlayOneShot(m_MaleScreamOne);
                break;

            case VoiceOvers.MALESCREAMTWO:
                m_SoundEffectSource.PlayOneShot(m_MaleScreamTwo);
                break;

            case VoiceOvers.MALESCREAMTHREE:
                m_SoundEffectSource.PlayOneShot(m_MaleScreamThree);
                break;

            default:
                Debug.Log("Does literally nothing");
                break;
        }
    }
}
