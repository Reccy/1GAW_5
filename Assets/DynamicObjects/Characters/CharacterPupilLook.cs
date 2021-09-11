using UnityEngine;

public class CharacterPupilLook : MonoBehaviour
{
    [SerializeField] private float m_radius;
    [SerializeField] private float m_reactionSpeed = 2.0f;
    [SerializeField] private float m_minimumMagnitude = 0.05f;

    private Vector3 m_origin;

    private Vector3 m_lookDir;
    public Vector3 LookDir
    {
        get => m_lookDir;
        set => m_lookDir = value;
    }

    private void Awake()
    {
        m_origin = transform.localPosition;
    }

    private void Update()
    {
        Vector3 target = m_origin + OffsetDir() * m_radius;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * m_reactionSpeed);
    }

    private Vector3 OffsetDir()
    {
        if (m_lookDir.magnitude < m_minimumMagnitude)
            return Vector3.zero;

        return new Vector3(m_lookDir.x, m_lookDir.z, 0).normalized;
    }
}
