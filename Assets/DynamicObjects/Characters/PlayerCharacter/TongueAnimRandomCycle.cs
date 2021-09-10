using UnityEngine;

public class TongueAnimRandomCycle : MonoBehaviour
{
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        float cycleOffset = Random.Range(0.0f, 1.0f);

        Debug.Log($"Cycle Offset {cycleOffset}");

        m_animator.SetFloat("CycleOffset", cycleOffset);
    }
}
