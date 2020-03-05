using UnityEngine;
using UnityEngine.UI;

public class CubeDecision : MonoBehaviour
{
    public Renderer cubeTrack;
    public int index;
    public string keyName;

    private static int _comboCount;
    private static int _currentStatus;

    private static Color[] _endColor =
    {
        new Color(1f, 1f, 1f, 0.4f),
        new Color(1f, 1f, 1f, 0.6f)
    };

    private static Color[] _startColor =
    {
        new Color(1f, 1f, 1f, 0.6f),
        new Color(1f, 1f, 1f, 0.8f)
    };

    private static string[] _statuses = {"失  误 ", "一  般 ", "错  过 ", "完  美 "};
    private static Text _textComboCount;

    private static Color[] _textEndColor =
    {
        new Color(1f, 0f, 0f, 0.6f),
        new Color(0f, 1f, 0.4f, 0.6f),
        new Color(0.6f, 0.6f, 0.6f, 0.6f),
        new Color(1f, 0.6f, 0f, 0.6f)
    };

    private static Color[] _textStartColor =
    {
        new Color(1f, 0f, 0f, 1f),
        new Color(0f, 1f, 0.4f, 1f),
        new Color(0.6f, 0.6f, 0.6f, 1f),
        new Color(1f, 0.6f, 0f, 1f)
    };

    private Renderer m_Renderer;
    private float m_TimeInterval;

    private void Start()
    {
        _comboCount = 0;
        _currentStatus = -1;
        m_Renderer = GetComponent<Renderer>();
        _textComboCount = GameObject.Find("Canvas 1/Text Combo Count").GetComponent<Text>();
    }

    private void Update()
    {
        if (_currentStatus != -1)
        {
            _textComboCount.color =
                Color.Lerp(_textComboCount.color, _textEndColor[_currentStatus], Time.deltaTime * 6f);
        }

        if (Input.GetKey(keyName) || Input.GetKeyDown(keyName))
        {
            cubeTrack.material.color = Color.Lerp(cubeTrack.material.color, _endColor[0], Time.deltaTime * 10f);
            m_Renderer.material.color = Color.Lerp(m_Renderer.material.color, _endColor[1], Time.deltaTime * 10f);
            if (Input.GetKeyDown(keyName))
            {
                if (BlocksCreate.ExistingBlockLists[index].Count > 0)
                {
                    m_TimeInterval = Settings.Music.time - BlocksCreate.ExistingBlockLists[index][0].decisionTime;
                    if (m_TimeInterval < 0.15f && m_TimeInterval > -0.3f)
                    {
                        if (m_TimeInterval < -0.2f)
                        {
                            ChangeStatus(0);
                        }
                        else if (m_TimeInterval < -0.1f || m_TimeInterval > 0.1f)
                        {
                            ChangeStatus(1);
                        }
                        else
                        {
                            ChangeStatus(3);
                        }

                        StartCoroutine(BlocksCreate.ExistingBlockLists[index][0].Destroy());
                        BlocksCreate.ExistingBlockLists[index].RemoveAt(0);
                    }
                }
            }
        }
        else
        {
            cubeTrack.material.color = Color.Lerp(cubeTrack.material.color, _startColor[0], Time.deltaTime * 10f);
            m_Renderer.material.color = Color.Lerp(m_Renderer.material.color, _startColor[1], Time.deltaTime * 10f);
        }
    }

    public static void ChangeStatus(int nextStatus)
    {
        if (_currentStatus == nextStatus)
        {
            ++_comboCount;
        }
        else
        {
            _currentStatus = nextStatus;
            _comboCount = 1;
        }

        ++Scoreboard.Instance[_currentStatus];
        _textComboCount.color = _textStartColor[_currentStatus];
        _textComboCount.text = _statuses[_currentStatus] + _comboCount.ToString();
    }
}