using Shapes;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private ShapeRenderer[] m_shapes;
    private Color[] m_colors;

    private int Length => m_colors.Length;

    private void Awake()
    {
        m_shapes = GetComponentsInChildren<ShapeRenderer>();
        m_colors = new Color[m_shapes.Length];

        for (int i = 0; i < Length; ++i)
        {
            m_colors[i] = m_shapes[i].Color;
        }
    }

    public void Hide()
    {
        for (int i = 0; i < Length; ++i)
        {
            Color c = m_colors[i];
            c.a = 0;

            m_shapes[i].Color = c;
        }
    }

    public void Show()
    {
        for (int i = 0; i < Length; ++i)
        {
            m_shapes[i].Color = m_colors[i];
        }
    }
}
