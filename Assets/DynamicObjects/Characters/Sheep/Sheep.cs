using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private float m_speed = 40.0f;
    [SerializeField] private float m_barkDetectRange = 10.0f;

    private CharacterMover m_characterMover;

    private PlayerCharacterBark m_barker;

    private Vector3 m_moveDir = Vector3.zero;

    private void Awake()
    {
        m_barker = FindObjectOfType<PlayerCharacterBark>();
    }

    private void Start()
    {
        m_characterMover = GetComponentInChildren<CharacterMover>();
    }

    private void OnEnable()
    {
        m_barker.OnBark += OnBarkDetected;
    }

    private void OnDisable()
    {
        m_barker.OnBark -= OnBarkDetected;
    }

    private void OnBarkDetected(Vector3 barkOrigin)
    {
        float barkDistance = Vector3.Distance(transform.position, barkOrigin);

        if (barkDistance > m_barkDetectRange)
            return;

        m_moveDir = (transform.position - barkOrigin).normalized;
    }

    private void FixedUpdate()
    {
        if (m_moveDir == Vector3.zero)
            return;

        m_characterMover.Move(m_moveDir * m_speed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_barkDetectRange);
    }
}
