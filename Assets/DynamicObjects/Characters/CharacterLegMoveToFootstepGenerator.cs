using UnityEngine;

public class CharacterLegMoveToFootstepGenerator : MonoBehaviour
{
    [SerializeField] private CharacterLegMove m_legMover;
    [SerializeField] private FootstepGenerator m_footstepGenerator;

    private void OnEnable()
    {
        m_legMover.OnLeftFootDown += m_footstepGenerator.DrawFootstepLeft;
        m_legMover.OnRightFootDown += m_footstepGenerator.DrawFootstepRight;
    }

    private void OnDisable()
    {
        m_legMover.OnLeftFootDown -= m_footstepGenerator.DrawFootstepLeft;
        m_legMover.OnRightFootDown -= m_footstepGenerator.DrawFootstepRight;
    }
}
