using UnityEngine;
using TMPro;

public class LevelCompleteText : MonoBehaviour
{
    private GameManager m_gameManager;
    private TMP_Text m_text;
    private Color m_color;

    private void Start()
    {
        m_gameManager = GameManager.Instance;
        m_text = GetComponent<TMP_Text>();
        m_color = m_text.color;

        m_text.color = new Color(0, 0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (m_gameManager.CurrentLevel.LevelComplete)
            m_text.color = m_color;
    }
}
