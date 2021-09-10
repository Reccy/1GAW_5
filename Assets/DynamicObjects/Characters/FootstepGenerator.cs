using UnityEngine;
using UnityEngine.Pool;

public class FootstepGenerator : MonoBehaviour
{
    [SerializeField] private int m_maxFootsteps = 18;
    [SerializeField] private Footstep m_footstepPrefab;
    [SerializeField] private Transform m_leftFootstepGenLocal;
    [SerializeField] private Transform m_rightFootstepGenLocal;
    [SerializeField] private AudioSource m_leftFootstepAudioSource;
    [SerializeField] private AudioSource m_rightFootstepAudioSource;
    [SerializeField] [Range(0, 2)] private float m_footstepPitchRandomRange;

    ObjectPool<Footstep> m_footstepPool;

    private Footstep[] m_footsteps;
    private int m_currentIdx = 0;

    private float m_defaultPitchLeft;
    private float m_defaultPitchRight;

    private void Awake()
    {
        m_footsteps = new Footstep[m_maxFootsteps];

        m_defaultPitchLeft = m_leftFootstepAudioSource.pitch;
        m_defaultPitchRight = m_rightFootstepAudioSource.pitch;

        m_footstepPool = new ObjectPool<Footstep>(
            createFunc: () => Instantiate(m_footstepPrefab),
            actionOnGet: (obj) => obj.Show(),
            actionOnRelease: (obj) => obj.Hide(),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: m_maxFootsteps,
            maxSize: m_maxFootsteps);
    }

    public void DrawFootstepLeft()
    {
        ReleaseCurrent();

        Footstep footstep = m_footstepPool.Get();
        footstep.transform.position = m_leftFootstepGenLocal.position;
        footstep.transform.rotation = m_leftFootstepGenLocal.rotation;
        m_footsteps[m_currentIdx] = footstep;

        m_leftFootstepAudioSource.pitch = m_defaultPitchLeft + Random.Range(0, m_footstepPitchRandomRange);
        m_leftFootstepAudioSource.Play();

        IncrementCurrentIdx();
    }

    public void DrawFootstepRight()
    {
        ReleaseCurrent();

        Footstep footstep = m_footstepPool.Get();
        footstep.transform.position = m_rightFootstepGenLocal.position;
        footstep.transform.rotation = m_rightFootstepGenLocal.rotation;
        m_footsteps[m_currentIdx] = footstep;

        m_rightFootstepAudioSource.pitch = m_defaultPitchRight + Random.Range(0, m_footstepPitchRandomRange);
        m_rightFootstepAudioSource.Play();

        IncrementCurrentIdx();
    }

    private void ReleaseCurrent()
    {
        if (m_footsteps[m_currentIdx] != null)
        {
            m_footstepPool.Release(m_footsteps[m_currentIdx]);
        }
    }

    private void IncrementCurrentIdx()
    {
        if (m_currentIdx + 1 == m_maxFootsteps)
        {
            m_currentIdx = 0;
            return;
        }

        m_currentIdx += 1;
    }
}
