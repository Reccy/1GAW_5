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
    }
}
