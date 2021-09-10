using UnityEngine;

public class CharacterPupilLook : MonoBehaviour
{
    [SerializeField] private CharacterMover m_mover;
    [SerializeField] private float m_radius;
    [SerializeField] private float m_reactionSpeed = 2.0f;
    [SerializeField] private float m_minimumSpeed = 0.05f;

    private Vector3 m_origin;
    private Vector3 m_movement;

    private void Awake()
    {
        m_origin = transform.localPosition;
    }

    private void OnEnable()
    {
        m_mover.OnPreMove += OnPreMove;
    }

    private void OnDisable()
    {
        m_mover.OnPreMove -= OnPreMove;
    }

    private void Update()
    {
        Vector3 target = m_origin + OffsetDir() * m_radius;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * m_reactionSpeed);
    }

    private void OnPreMove(Vector3 movement) => m_movement = movement;

    private Vector3 OffsetDir()
    {
        if (m_movement.magnitude < m_minimumSpeed)
            return Vector3.zero;

        return new Vector3(m_movement.x, m_movement.z, 0).normalized;
    }
}
