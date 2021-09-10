using UnityEngine;

public class CharacterLegMove : MonoBehaviour
{
    [SerializeField] private CharacterMover m_mover;
    [SerializeField] private Transform m_leftLeg;
    [SerializeField] private Transform m_rightLeg;

    [Range(0,1)]
    [SerializeField] private float m_maxAmplitude = 0.0f;
    
    [Range(0,20)]
    [SerializeField] private float m_maxSpeed = 0.0f;

    private Vector3 m_leftLegOrigin;
    private Vector3 m_rightLegOrigin;

    private float m_currentSpeed = 0;
    private float m_currentAmplitude = 0;
    private float m_offset = 0;

    private bool m_leftFootDownCalled = false;
    private bool m_rightFootDownCalled = false;

    public delegate void OnLeftFootDownEvent();
    public delegate void OnRightFootDownEvent();
    public event OnLeftFootDownEvent OnLeftFootDown;
    public event OnRightFootDownEvent OnRightFootDown;

    private void Awake()
    {
        m_leftLegOrigin = m_leftLeg.localPosition;
        m_rightLegOrigin = m_rightLeg.localPosition;
        m_offset = Random.Range(0.0f, 1.0f);
    }

    private void OnEnable()
    {
        m_mover.OnPreMove += OnCharacterPreMove;
    }

    private void OnDisable()
    {
        m_mover.OnPreMove -= OnCharacterPreMove;        
    }

    private void OnCharacterPreMove(Vector3 movement)
    {
        m_currentSpeed = Mathf.Clamp(movement.magnitude, 0, m_maxSpeed);
        m_currentAmplitude = Mathf.Clamp(movement.magnitude, 0, m_maxAmplitude);
    }

    private void Update()
    {
        float cycle = Mathf.Sin(m_maxSpeed * Time.timeSinceLevelLoad + m_offset) * m_currentAmplitude;

        float leftZ = Mathf.Clamp(cycle, 0, m_maxAmplitude);
        float rightZ = Mathf.Clamp(-cycle, 0, m_maxAmplitude);

        m_leftLeg.localPosition = m_leftLegOrigin + Vector3.up * leftZ;
        m_rightLeg.localPosition = m_rightLegOrigin + Vector3.up * rightZ;

        if (Vector3.Distance(m_leftLeg.localPosition, m_leftLegOrigin) < 0.1f && !m_leftFootDownCalled)
        {
            if (OnLeftFootDown != null)
                OnLeftFootDown();

            m_leftFootDownCalled = true;
        }
        else if (Vector3.Distance(m_leftLeg.localPosition, m_leftLegOrigin) >= 0.1f)
        {
            m_leftFootDownCalled = false;
        }

        if (Vector3.Distance(m_rightLeg.localPosition, m_rightLegOrigin) < 0.1f && !m_rightFootDownCalled)
        {
            if (OnRightFootDown != null)
                OnRightFootDown();

            m_rightFootDownCalled = true;
        }
        else if (Vector3.Distance(m_rightLeg.localPosition, m_rightLegOrigin) >= 0.1f)
        {
            m_rightFootDownCalled = false;
        }
    }
}
