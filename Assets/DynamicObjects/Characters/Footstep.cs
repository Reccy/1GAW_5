using Shapes;
using UnityEngine;
using System.Collections;

public class Footstep : MonoBehaviour
{
    private ShapeRenderer[] m_shapes;
    private Color[] m_colors;

    private int Length => m_colors.Length;

    [SerializeField] private float m_lifetime = 5.0f;
    [SerializeField] private float m_fadeSpeed = 0.5f;

    private float m_currentTime;
    private bool m_hidden;
    private float m_t;

    private Coroutine m_hideCoroutine;

    private void Awake()
    {
        m_shapes = GetComponentsInChildren<ShapeRenderer>();
        m_colors = new Color[m_shapes.Length];

        for (int i = 0; i < Length; ++i)
        {
            m_colors[i] = m_shapes[i].Color;
        }

        m_currentTime = m_lifetime;
        m_hidden = false;
    }

    public void Hide()
    {
        for (int i = 0; i < Length; ++i)
        {
            Color c = m_colors[i];
            c.a = 0;

            m_shapes[i].Color = c;
        }

        m_hidden = true;
        m_currentTime = m_lifetime;

        if (m_hideCoroutine != null)
            StopCoroutine(m_hideCoroutine);
    }

    public void Show()
    {
        for (int i = 0; i < Length; ++i)
        {
            m_shapes[i].Color = m_colors[i];
        }

        m_hidden = false;
        m_currentTime = m_lifetime;
    }

    private void Update()
    {
        if (m_hidden)
            return;

        if (m_currentTime > 0)
        {
            m_currentTime -= Time.deltaTime;
            m_t = 1.0f;
            return;
        }
        else if (m_hideCoroutine == null)
        {
            m_hidden = true;
            m_hideCoroutine = StartCoroutine(SlowHide());
        }
    }

    private IEnumerator SlowHide()
    {
        while (m_t > 0)
        {
            for (int i = 0; i < Length; ++i)
            {
                Color target = m_colors[i];
                target.a = 0;

                m_shapes[i].Color = Color.Lerp(target, m_colors[i], m_t);
            }

            m_t -= Time.deltaTime * m_fadeSpeed;

            yield return new WaitForEndOfFrame();
        }

        m_hideCoroutine = null;
        m_hidden = true;
    }
}
