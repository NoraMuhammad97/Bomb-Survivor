using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    [SerializeField] AudioSource backgroundAudioSource;
    [SerializeField] AudioSource effectsAudioSource;

    [SerializeField] AudioClip  bombPickClip;
    [SerializeField] AudioClip  bombExplodeClip;
    [SerializeField] AudioClip  timerTickingClip;
    [SerializeField] AudioClip  winClip;
    [SerializeField] AudioClip  loseClip;

    [Space(0)]
    [Header("Audio UI")]
    [SerializeField] Image  audioIcon;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;

    bool isBGPlaying;
    public enum GameClips
    {
        Background,
        BombPick,
        BombExplode,
        TimerTicking,
        Win,
        Lose
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        backgroundAudioSource.Play();
        isBGPlaying = true;
    }

    #region Helper Functions
    public void PlayClip(GameClips clip)
    {
        if(clip == GameClips.Background)
        {
            backgroundAudioSource.Play();
        }
        else
        {
            AudioClip clipToPlay;

            switch (clip)
            {
                case GameClips.BombPick:
                    clipToPlay = bombPickClip;
                    break;
                case GameClips.BombExplode:
                    clipToPlay = bombExplodeClip;
                    break;
                case GameClips.TimerTicking:
                    clipToPlay = timerTickingClip;
                    break;
                case GameClips.Win:
                    clipToPlay = winClip;
                    break;
                default:
                    clipToPlay = loseClip;
                    break;
            }

            effectsAudioSource.clip = clipToPlay;
            effectsAudioSource.Play();
        }
    }
    public void StopClip(GameClips clip)
    {
        if (clip == GameClips.Background)
            backgroundAudioSource.Stop();
        else
            effectsAudioSource.Stop();
    }
    public void ToggleBackgroundMusic()
    {
        if (isBGPlaying)
        {
            backgroundAudioSource.Stop();
            audioIcon.sprite = musicOff;
            isBGPlaying = false;
        }
        else
        {
            backgroundAudioSource.Play();
            audioIcon.sprite = musicOn;
            isBGPlaying = true;
        }
    }
    #endregion
}
