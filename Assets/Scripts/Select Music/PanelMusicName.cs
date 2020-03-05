using UnityEngine;
using UnityEngine.UI;

public class PanelMusicName : MonoBehaviour
{
    public static bool IsAudioPlayEnabled = true;

    public AudioClip audioClip;
    public int index;

    private static Color _endColor = new Color(0.4f, 0.4f, 0.4f, 0.6f);
    private static AudioSource _musicPreview;
    private static Color _startColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);

    private bool m_IsMouseDown;
    private Image m_PanelMusicName;

    private void Start()
    {
        m_PanelMusicName = GetComponent<Image>();
        if (_musicPreview == null)
        {
            _musicPreview = GameObject.Find("Music Preview").GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        m_PanelMusicName.color = m_IsMouseDown
            ? Color.Lerp(m_PanelMusicName.color, _endColor, Time.deltaTime * 8f)
            : Color.Lerp(m_PanelMusicName.color, _startColor, Time.deltaTime * 8f);
    }

    private void OnMouseDown()
    {
        m_IsMouseDown = true;
    }

    private void OnMouseUp()
    {
        if (IsAudioPlayEnabled)
        {
            MusicSettings.MusicIndex = index;
            _musicPreview.clip = audioClip;
            _musicPreview.Play();
        }

        m_IsMouseDown = false;
    }
}