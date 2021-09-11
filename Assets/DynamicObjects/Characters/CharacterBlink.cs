using System.Collections;
using UnityEngine;

public class CharacterBlink : MonoBehaviour
{
    [SerializeField] private GameObject m_leftEyeOpen;
    [SerializeField] private GameObject m_rightEyeOpen;
    [SerializeField] private GameObject m_leftEyeClosed;
    [SerializeField] private GameObject m_rightEyeClosed;

    [Range(1, 10)]
    [SerializeField] private float m_blinkIntervalMin = 3;

    [Range(1, 10)]
    [SerializeField] private float m_blinkIntervalMax = 10;

    [Range(0, 1)]
    [SerializeField] private float m_blinkDuration = 0.2f;

    private float m_currentInterval;
    private bool m_isBlinking;

    private bool m_blinkingEnabled = true;
    public bool BlinkingEnabled
    {
        get => m_blinkingEnabled;
        set => m_blinkingEnabled = value;
    }

    private void Awake()
    {
        if (m_blinkIntervalMin >= m_blinkIntervalMax)
        {
            Debug.LogWarning("Min blink interval is greater than or equal to max blink interval!", this);
        }

        m_currentInterval = GenerateInterval();

        OpenEyes();
    }

    private void Update()
    {
        if (!m_blinkingEnabled)
        {
            OpenEyes();
            return;
        }

        m_currentInterval -= Time.deltaTime;
        
        if (m_currentInterval <= 0 && !m_isBlinking)
        {
            m_isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        CloseEyes();

        yield return new WaitForSeconds(m_blinkDuration);

        OpenEyes();

        m_currentInterval = GenerateInterval();
        m_isBlinking = false;
    }

    private void CloseEyes()
    {
        m_leftEyeClosed.SetActive(true);
        m_rightEyeClosed.SetActive(true);
        m_leftEyeOpen.SetActive(false);
        m_rightEyeOpen.SetActive(false);
    }

    private void OpenEyes()
    {
        m_leftEyeClosed.SetActive(false);
        m_rightEyeClosed.SetActive(false);
        m_leftEyeOpen.SetActive(true);
        m_rightEyeOpen.SetActive(true);
    }

    private float GenerateInterval() => Random.Range(m_blinkIntervalMin, m_blinkIntervalMax);
}
