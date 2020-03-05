using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("Common/Mask Change Scene")]
public class MaskChangeScene : MonoBehaviour
{
    public Color maskEndColor;
    public string nextSceneName;

    private bool m_IsFirstFrame;
    private Image m_Mask;
    private float m_StartTime;

    private void Update()
    {
        if (Time.time - m_StartTime < 0.5f)
        {
            m_Mask.color = Color.Lerp(m_Mask.color, maskEndColor, Time.deltaTime * 10f);
        }
        else
        {
            if (m_IsFirstFrame)
            {
                m_IsFirstFrame = false;
                m_Mask.color = maskEndColor;
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private void OnEnable()
    {
        m_IsFirstFrame = true;
        m_Mask = GetComponent<Image>();
        m_StartTime = Time.time;
    }
}