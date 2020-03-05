using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static bool IsMusicEnded;
    public static bool IsMusicStarted;
    public static AudioSource Music;
    public static MusicInformation MusicInformation;
    public static Settings Instance;

    public AudioClip[] audioClips;
    public new Camera camera;
    public CubeDecision[] cubeDecisions;
    public Material materialBlockGetHit;

    private const int LayerMask = 1 << 12;
    private float m_LastMusicTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int a = 0; a < 4; ++a)
        {
            Scoreboard.Instance[a] = 0;
        }

        IsMusicEnded = false;
        IsMusicStarted = false;
        Music = GameObject.Find("Camera 0/Music").GetComponent<AudioSource>();
        Music.clip = audioClips[MusicSettings.MusicIndex];
        MusicInformation = MusicInformation.Load(0);
        Music.panStereo = MusicSettings.StereoPan;
        StartCoroutine(MusicPlay());
    }

    private void Update()
    {
        if (IsMusicStarted)
        {
            if (!IsMusicEnded)
            {
                if (m_LastMusicTime > Music.time)
                {
                    IsMusicStarted = false;
                    IsMusicEnded = true;
                    StartCoroutine(ScoreboardShow());
                }
            }

            m_LastMusicTime = Music.time;
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (IsMusicStarted)
        {
            if (hasFocus)
            {
                Music.UnPause();
            }
            else
            {
                Music.Pause();
            }
        }
    }

    private IEnumerator MusicPlay()
    {
        yield return new WaitForSeconds(4f);
        Music.Play();
        IsMusicStarted = true;
    }

    private IEnumerator ScoreboardShow()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scoreboard");
    }
}