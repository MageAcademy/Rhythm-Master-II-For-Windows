using UnityEngine;

public class MusicListDrag : MonoBehaviour
{
    public float bottomPositionY;
    public new Camera camera;
    public float frictionFactor;
    public float moveSpeedFactor;
    public float topPositionY;

    private float m_DeltaPositionY;
    private bool m_IsDragStarted;
    private float m_LastTouchPositionY;
    private float m_MoveSpeed;
    private float m_PositionY;
    private float m_StartPositionY;
    private float m_TouchPositionY;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && (Input.mousePosition.x < Screen.width / 2f))
        {
            m_IsDragStarted = true;
            m_LastTouchPositionY = camera.ScreenToWorldPoint(Input.mousePosition).y;
            m_StartPositionY = m_PositionY;
            PanelMusicName.IsAudioPlayEnabled = true;
        }
        else if (Input.GetMouseButton(0) && m_IsDragStarted)
        {
            m_TouchPositionY = camera.ScreenToWorldPoint(Input.mousePosition).y;
            m_DeltaPositionY = m_TouchPositionY - m_LastTouchPositionY;
            m_LastTouchPositionY = m_TouchPositionY;
            m_MoveSpeed = moveSpeedFactor * m_DeltaPositionY / Time.deltaTime;
            m_PositionY += m_DeltaPositionY;
            if (PanelMusicName.IsAudioPlayEnabled && (Mathf.Abs(m_PositionY - m_StartPositionY) > 0.1f))
            {
                PanelMusicName.IsAudioPlayEnabled = false;
            }
        }
        else
        {
            m_DeltaPositionY = m_MoveSpeed * Time.deltaTime;
            if (m_DeltaPositionY + transform.position.y < topPositionY)
            {
                m_MoveSpeed = 0f;
                m_PositionY = topPositionY;
            }
            else if (m_DeltaPositionY + transform.position.y > bottomPositionY)
            {
                m_MoveSpeed = 0f;
                m_PositionY = bottomPositionY;
            }
            else
            {
                if (m_IsDragStarted)
                {
                    m_IsDragStarted = false;
                }

                m_PositionY = m_DeltaPositionY + transform.position.y;
                m_MoveSpeed = Mathf.Lerp(m_MoveSpeed, 0f, frictionFactor * Time.deltaTime);
                if (Mathf.Abs(m_MoveSpeed) < 0.5f)
                {
                    m_MoveSpeed = 0f;
                }
            }
        }

        transform.position = new Vector3(0f, m_PositionY, 0f);
    }
}