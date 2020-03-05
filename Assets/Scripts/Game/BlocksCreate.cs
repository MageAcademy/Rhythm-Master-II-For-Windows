using System.Collections.Generic;
using UnityEngine;

public class BlocksCreate : MonoBehaviour
{
    public static List<Block>[] ExistingBlockLists = new List<Block>[4]
    {
        new List<Block>(),
        new List<Block>(),
        new List<Block>(),
        new List<Block>()
    };

    public Object blockPrefab;

    private float[] m_IndexToX = {-3.3f, -1.1f, 1.1f, 3.3f};
    private float m_DecisionTime;
    private float m_InstantiateTimeOffset;

    private void Start()
    {
        for (int a = 0; a < 4; ++a)
        {
            ExistingBlockLists[a].Clear();
        }

        m_InstantiateTimeOffset = -4f / MusicSettings.Speed;
    }

    private void Update()
    {
        if (!Settings.IsMusicEnded)
        {
            while (Settings.MusicInformation.IndexList.Count > 0)
            {
                m_DecisionTime = MusicSettings.DecisionTimeOffset + Settings.MusicInformation.DecisionTimeOffset +
                                 Settings.MusicInformation.TimeIntervalPerBeat * Settings.MusicInformation.TimeList[0];
                if (m_DecisionTime + m_InstantiateTimeOffset < Settings.Music.time)
                {
                    Block block = ((GameObject) Instantiate(blockPrefab,
                            new Vector3(m_IndexToX[Settings.MusicInformation.IndexList[0]], 0f, 24f),
                            Quaternion.identity))
                        .GetComponent<Block>();
                    block.decisionTime = m_DecisionTime;
                    block.trackIndex = Settings.MusicInformation.IndexList[0];
                    ExistingBlockLists[block.trackIndex].Add(block);
                    Settings.MusicInformation.IndexList.RemoveAt(0);
                    Settings.MusicInformation.TimeList.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }
    }
}