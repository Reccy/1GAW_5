using UnityEngine;
using Rewired;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float m_speed = 10.0f;

    const int PLAYER_ID = 0;
    private Player m_player;
    private CharacterMover m_characterMover;

    private void Start()
    {
        m_characterMover = GetComponentInChildren<CharacterMover>();
        m_player = ReInput.players.GetPlayer(PLAYER_ID);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_player.GetAxis("Move Horizontal"), 0, m_player.GetAxis("Move Vertical"));
        m_characterMover.Move(movement.normalized * m_speed);
    }
}
