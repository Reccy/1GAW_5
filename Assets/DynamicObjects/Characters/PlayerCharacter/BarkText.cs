using UnityEngine;
using TMPro;

public class BarkText : MonoBehaviour
{
    private string[] m_barks =
    {
        "baark",
        "bork",
        "bark",
        "woof",
        "baaark",
        "yip"
    };

    private TMP_Text m_text;

    private float m_maxY = 100;
    private Vector3 m_target;
    private static int m_lastIdx = 0; // Hack :P

    [SerializeField] [Range(1, 5)] private float m_speed = 1.0f;
    [SerializeField] [Range(1, 5)] private float m_shrinkSpeed = 1.0f;

    private void Awake()
    {
        m_text = GetComponent<TMP_Text>();

        int idx = GetRandom();

        m_text.text = m_barks[idx];

        m_target = transform.position + Vector3.up * m_maxY;
    }

    private int GetRandom()
    {
        int idx;
        do
        {
            idx = Random.Range(0, m_barks.Length);
        }
        while (idx == m_lastIdx);

        m_lastIdx = idx;

        return idx;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_target, Time.deltaTime * m_speed);

        if (Vector3.Distance(transform.position, m_target) < 1.0f)
        {
            Destroy(gameObject);
            return;
        }

        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * m_shrinkSpeed);

        if (Vector3.Distance(transform.localScale, Vector3.zero) < 0.01f)
            Destroy(gameObject);
    }
}
