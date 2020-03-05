using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    public static float DecisionTimeOffset;
    public static int MusicIndex = 0;
    public static float Speed;
    public static float StereoPan;

    public AudioSource musicPreview;
    public Slider sliderDecisionTimeOffset;
    public Slider sliderSpeed;
    public Slider sliderStereoPan;
    public Text textDecisionTimeOffset;
    public Text textSpeed;
    public Text textStereoPan;

    private int m_StereoPanLevel;

    private void Start()
    {
        Speed = 1f;
    }

    public void ChangeDecisionTimeOffset()
    {
        DecisionTimeOffset = sliderDecisionTimeOffset.value / 100f;
        if (((int) sliderDecisionTimeOffset.value) == 0)
        {
            textDecisionTimeOffset.text = "Default";
        }
        else
        {
            textDecisionTimeOffset.text = sliderDecisionTimeOffset.value.ToString() + "0 ms";
        }
    }

    public void ChangeSpeed()
    {
        Speed = sliderSpeed.value;
        textSpeed.text = "Level " + sliderSpeed.value.ToString();
    }

    public void ChangeStereoPan()
    {
        m_StereoPanLevel = (int) sliderStereoPan.value;
        StereoPan = m_StereoPanLevel / 10f;
        musicPreview.panStereo = StereoPan;
        if (m_StereoPanLevel < 0)
        {
            textStereoPan.text = "Left " + (-m_StereoPanLevel).ToString();
        }
        else if (m_StereoPanLevel == 0)
        {
            textStereoPan.text = "Balance";
        }
        else
        {
            textStereoPan.text = "Right " + m_StereoPanLevel.ToString();
        }
    }
}