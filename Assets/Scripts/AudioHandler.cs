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

    [Header("WILHELM SCREAM")]
    public AudioClip m_WilhelmScream;

    [Header("Voice Overs")]
    public AudioClip m_DemonicMumblingOne;
    public AudioClip m_DemonicMumblingTwo;
    public AudioClip m_DemonicMumblingThree;
    public AudioClip m_DemonicLaugh;
    public AudioClip m_ShoggothFeast;
    public AudioClip m_PriestVoice;
    public AudioClip m_CheerfulGreeting;
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

    public enum VoiceOvers
    {
        WILHELMSCREAM,
        DEMONICMUMBLINGONE,
        DEMONICMUMBLINGTWO,
        DEMONICMUMBLETHREE,
        DEMONICLAUGH,
        SHOGGOTHFEAST,
        PRIESTVOICE,
        CHEERFULGREETING,
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

            case VoiceOvers.PRIESTVOICE:
                m_SoundEffectSource.PlayOneShot(m_PriestVoice);
                break;
            case VoiceOvers.DEMONICMUMBLINGONE:
                m_SoundEffectSource.PlayOneShot(m_DemonicMumblingOne);
                break;

            case VoiceOvers.DEMONICMUMBLINGTWO:
                m_SoundEffectSource.PlayOneShot(m_DemonicMumblingTwo);
                break;

            case VoiceOvers.DEMONICMUMBLETHREE:
                m_SoundEffectSource.PlayOneShot(m_DemonicMumblingThree);
                break;

            case VoiceOvers.DEMONICLAUGH://(?),
                m_SoundEffectSource.PlayOneShot(m_DemonicLaugh);
                break;

            case VoiceOvers.CHEERFULGREETING:
                m_SoundEffectSource.PlayOneShot(m_CheerfulGreeting);
                break;

            case VoiceOvers.GRUNTS:
                m_SoundEffectSource.PlayOneShot(m_Grunt);
                break;

            case VoiceOvers.SIGH:
                m_SoundEffectSource.PlayOneShot(m_Sigh);
                break;

            case VoiceOvers.SHOGGOTHFEAST:
                m_SoundEffectSource.PlayOneShot(m_ShoggothFeast);
                break;

            case VoiceOvers.WILHELMSCREAM:
                m_SoundEffectSource.PlayOneShot(m_WilhelmScream);
                break;

            default:
                Debug.Log("Plays nothing");
                break;
        }
    }

}
