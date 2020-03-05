using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Common/UI Adapt")]
public class UIAdapt : MonoBehaviour
{
    [Tooltip("此摄像机启用时，应适应窗口尺寸的所有画布的CanvasScaler组件。")]
    public CanvasScaler[] canvasScalers;

    [Tooltip("此摄像机的目标高度。（单位：100像素）")] public float targetHeight;
    [Tooltip("此摄像机的目标宽度。（单位：100像素）")] public float targetWidth;

    private Camera m_Camera;
    private int m_LastScreenHeight;
    private int m_LastScreenWidth;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
        Adapt();
    }

    private void Update()
    {
        if (m_LastScreenHeight != Screen.height || m_LastScreenWidth != Screen.width)
        {
            Adapt();
        }
    }

    private void Adapt()
    {
        m_LastScreenHeight = Screen.height;
        m_LastScreenWidth = Screen.width;
        float aspectRatio = (float) m_LastScreenWidth / m_LastScreenHeight;
        if (aspectRatio * targetHeight < targetWidth)
        {
            foreach (CanvasScaler item in canvasScalers)
            {
                item.matchWidthOrHeight = 0f;
            }

            if (m_Camera.orthographic)
            {
                m_Camera.orthographicSize = targetWidth * 0.5f / aspectRatio;
            }
            else
            {
                float height = aspectRatio * targetHeight / targetWidth;
                m_Camera.rect = new Rect(0f, (1f - height) / 2f, 1f, height);
            }
        }
        else
        {
            foreach (CanvasScaler item in canvasScalers)
            {
                item.matchWidthOrHeight = 1f;
            }

            if (m_Camera.orthographic)
            {
                m_Camera.orthographicSize = targetHeight * 0.5f;
            }
            else
            {
                float width = targetWidth / aspectRatio / targetHeight;
                m_Camera.rect = new Rect((1f - width) / 2f, 0f, width, 1f);
            }
        }
    }
}