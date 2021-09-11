using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private float m_speed = 40.0f;

    private CharacterMover m_characterMover;

    private void Start()
    {
        m_characterMover = GetComponentInChildren<CharacterMover>();
    }

    private void FixedUpdate()
    {
        float x = Mathf.Sin(Time.timeSinceLevelLoad * 5);
        float y = Mathf.Cos(Time.timeSinceLevelLoad * 5);

        Vector3 movement = new Vector3(x, 0, y);
        m_characterMover.Move(movement.normalized * m_speed);
    }
}
