using UnityEngine;
using UnityEngine.UI;

public class PanelStart : MonoBehaviour
{
    public GameObject maskChangeScene;

    private Color m_EndColor = new Color(0.4f, 0.4f, 0.4f, 0.6f);
    private bool m_IsMouseDown;
    private Image m_PanelStart;
    private Color m_StartColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);

    private void Start()
    {
        m_PanelStart = GetComponent<Image>();
    }

    private void Update()
    {
        m_PanelStart.color = m_IsMouseDown
            ? Color.Lerp(m_PanelStart.color, m_EndColor, Time.deltaTime * 8f)
            : Color.Lerp(m_PanelStart.color, m_StartColor, Time.deltaTime * 8f);
    }

    private void OnMouseDown()
    {
        m_IsMouseDown = true;
    }

    private void OnMouseUp()
    {
        m_IsMouseDown = false;
        maskChangeScene.SetActive(true);
    }
}