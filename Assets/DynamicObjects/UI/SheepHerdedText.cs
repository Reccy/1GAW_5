using UnityEngine;
using TMPro;

public class SheepHerdedText : MonoBehaviour
{
    private GameManager m_gameManager;
    private TMP_Text m_text;
    private string m_baseText;

    private void Start()
    {
        m_gameManager = GameManager.Instance;
        m_text = GetComponent<TMP_Text>();
        m_baseText = m_text.text;
    }

    private void Update()
    {
        m_text.text = $"{m_baseText}{m_gameManager.CurrentLevel.SheepHerded} of {m_gameManager.CurrentLevel.SheepInLevel}";
    }
}
