using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static float Speed;

    [HideInInspector] public float decisionTime;
    [HideInInspector] public int trackIndex;

    private bool m_IsDestroyed;
    private Renderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        Speed = MusicSettings.Speed * 6f;
    }

    private void Update()
    {
        if (!m_IsDestroyed)
        {
            if (Settings.Music.time - decisionTime > 0.15f)
            {
                CubeDecision.ChangeStatus(2);
                m_IsDestroyed = true;
                StartCoroutine(Destroy());
                BlocksCreate.ExistingBlockLists[trackIndex].Remove(this);
            }

            transform.position = new Vector3(transform.position.x, transform.position.y,
                (decisionTime - Settings.Music.time) * Speed);
        }
    }

    public IEnumerator Destroy()
    {
        m_IsDestroyed = true;
        m_Renderer.material = Settings.Instance.materialBlockGetHit;
        float startTime = Time.time;
        yield return 0;
        while (Time.time - startTime < 0.5f)
        {
            m_Renderer.material.color = Color.Lerp(m_Renderer.material.color, Color.clear, Time.deltaTime * 4f);
            yield return 0;
        }

        Destroy(gameObject);
    }
}