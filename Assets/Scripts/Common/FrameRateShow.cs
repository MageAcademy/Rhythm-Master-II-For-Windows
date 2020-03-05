using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Common/Frame Rate Show")]
public class FrameRateShow : MonoBehaviour
{
    [Range(0.1f, 1f)] [Tooltip("对帧率采样的时间间隔。（单位：秒）")]
    public float samplingTimeInterval;

    private static int _frameCount = -1;
    private static float _frameRate;
    private static string[] _frameRateParts;
    private static float _lastSamplingTime;
    private static float _nextSamplingTime;
    private static string _stringFrameRate = "";

    private Text m_TextFrameRate;

    private void Start()
    {
        m_TextFrameRate = GetComponent<Text>();
        if (_frameCount < 0)
        {
            _frameCount = 1;
            _lastSamplingTime = Time.unscaledTime;
            _nextSamplingTime = ((int) (_lastSamplingTime / samplingTimeInterval) + 1) * samplingTimeInterval;
        }
        else
        {
            ++_frameCount;
            if (_stringFrameRate != "")
            {
                m_TextFrameRate.text = _stringFrameRate;
            }
        }
    }

    private void Update()
    {
        ++_frameCount;
        if (_nextSamplingTime < Time.unscaledTime)
        {
            _frameRate = _frameCount / (Time.unscaledTime - _lastSamplingTime);
            if (_frameRate < 999.9f)
            {
                _frameRateParts = _frameRate.ToString().Split('.');
                if (_frameRateParts.Length == 1)
                {
                    _stringFrameRate = _frameRateParts[0] + ".0 FPS";
                }
                else
                {
                    _stringFrameRate = _frameRateParts[0] + "." + _frameRateParts[1].Substring(0, 1) + " FPS";
                }
            }
            else
            {
                _stringFrameRate = "999.9 FPS";
            }

            _frameCount = 0;
            _lastSamplingTime = Time.unscaledTime;
            _nextSamplingTime = ((int) (_lastSamplingTime / samplingTimeInterval) + 1) * samplingTimeInterval;
            m_TextFrameRate.text = _stringFrameRate;
        }
    }
}