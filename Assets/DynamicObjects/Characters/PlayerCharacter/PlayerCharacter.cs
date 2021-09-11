using UnityEngine;
using Rewired;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float m_speed = 40.0f;

    const int PLAYER_ID = 0;
    private Player m_player;
    
    private CharacterMover m_characterMover;
    [SerializeField] private CharacterPupilLook m_leftPupilLook;
    [SerializeField] private CharacterPupilLook m_rightPupilLook;

    Vector3 m_eyeMovement = Vector3.zero;

    private void Awake()
    {
        m_characterMover = GetComponentInChildren<CharacterMover>();
    }

    private void Start()
    {
        m_player = ReInput.players.GetPlayer(PLAYER_ID);
    }

    private void OnEnable()
    {
        m_characterMover.OnPostMove += OnPostMove;
    }

    private void OnDisable()
    {
        m_characterMover.OnPostMove -= OnPostMove;
    }

    private void OnPostMove(Vector3 movement)
    {
        m_eyeMovement = movement;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_player.GetAxis("Move Horizontal"), 0, m_player.GetAxis("Move Vertical"));
        m_characterMover.Move(movement.normalized * m_speed);

        m_leftPupilLook.LookDir = m_eyeMovement;
        m_rightPupilLook.LookDir = m_eyeMovement;
    }
}
