using UnityEngine;
using Shapes;

public class Sheep : MonoBehaviour
{
    [SerializeField] private float m_speed = 40.0f;
    [SerializeField] private float m_barkDetectRange = 10.0f;

    private CharacterMover m_characterMover;

    private Transform m_playerTransform;
    private PlayerCharacterBark m_barker;

    [SerializeField] private CharacterPupilLook m_leftPupilLook;
    [SerializeField] private CharacterPupilLook m_rightPupilLook;

    private Disc m_leftPupilDisc;
    private Disc m_rightPupilDisc;

    private Vector3 m_moveDir = Vector3.zero;
    private Vector3 m_eyeMovement = Vector3.zero;

    [SerializeField] private GameObject m_sweat;

    private float m_pupilRadiusNormal;
    [SerializeField] private float m_pupilRadiusSpooked = 0.06f;

    [SerializeField] private Disc m_mouth;
    private float m_mouthAngleStartNormal;
    private float m_mouthAngleEndNormal;

    [SerializeField] private float m_mouthAngleStartSpooked;
    [SerializeField] private float m_mouthAngleEndSpooked;

    [SerializeField] private CharacterBlink m_blinking;

    private void Awake()
    {
        m_barker = FindObjectOfType<PlayerCharacterBark>();
        m_characterMover = GetComponentInChildren<CharacterMover>();
        m_playerTransform = m_barker.transform;

        m_leftPupilDisc = m_leftPupilLook.GetComponent<Disc>();
        m_rightPupilDisc = m_rightPupilLook.GetComponent<Disc>();

        m_pupilRadiusNormal = m_leftPupilDisc.Radius;

        m_mouthAngleStartNormal = m_mouth.AngRadiansStart;
        m_mouthAngleEndNormal = m_mouth.AngRadiansEnd;
    }

    private void OnEnable()
    {
        m_barker.OnBark += OnBarkDetected;
        m_characterMover.OnPostMove += OnPostMove;
    }

    private void OnDisable()
    {
        m_barker.OnBark -= OnBarkDetected;
        m_characterMover.OnPostMove -= OnPostMove;
    }

    private void OnBarkDetected(Vector3 barkOrigin)
    {
        float barkDistance = Vector3.Distance(transform.position, barkOrigin);

        if (barkDistance > m_barkDetectRange)
            return;

        m_moveDir = (transform.position - barkOrigin).normalized;
    }

    private void OnPostMove(Vector3 movement)
    {
        m_eyeMovement = movement;
    }

    private void FixedUpdate()
    {
        if (m_moveDir == Vector3.zero)
        {
            if (Vector3.Distance(transform.position, m_playerTransform.position) < m_barkDetectRange)
            {
                Vector3 lookToPlayer = m_playerTransform.position - transform.position;

                m_leftPupilLook.LookDir = lookToPlayer;
                m_rightPupilLook.LookDir = lookToPlayer;

                BecomeSpooked();
            }
            else
            {
                BecomeUnspooked();

                m_leftPupilLook.LookDir = Vector3.zero;
                m_rightPupilLook.LookDir = Vector3.zero;
            }

            return;
        }

        BecomeUnspooked();

        m_characterMover.Move(m_moveDir * m_speed);

        m_leftPupilLook.LookDir = m_eyeMovement;
        m_rightPupilLook.LookDir = m_eyeMovement;
    }

    private void BecomeSpooked()
    {
        m_leftPupilDisc.Radius = m_pupilRadiusSpooked;
        m_rightPupilDisc.Radius = m_pupilRadiusSpooked;

        m_mouth.AngRadiansStart = Mathf.Deg2Rad * m_mouthAngleStartSpooked;
        m_mouth.AngRadiansEnd = Mathf.Deg2Rad * m_mouthAngleEndSpooked;

        m_sweat.SetActive(true);

        m_blinking.BlinkingEnabled = false;
    }

    private void BecomeUnspooked()
    {
        m_sweat.SetActive(false);

        m_leftPupilDisc.Radius = m_pupilRadiusNormal;
        m_rightPupilDisc.Radius = m_pupilRadiusNormal;

        m_mouth.AngRadiansStart = m_mouthAngleStartNormal;
        m_mouth.AngRadiansEnd = m_mouthAngleEndNormal;

        m_blinking.BlinkingEnabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_barkDetectRange);
    }
}
