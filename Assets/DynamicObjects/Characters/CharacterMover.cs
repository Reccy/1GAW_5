using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float m_maxVelocity = 10;

    private Rigidbody m_rb;

    public delegate void OnMoveEvent(Vector3 movement);
    public event OnMoveEvent OnPreMove;
    public event OnMoveEvent OnPostMove;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 movement)
    {
        if (OnPreMove != null)
            OnPreMove(m_rb.velocity);

        m_rb.AddForce(movement * Time.deltaTime, ForceMode.Impulse);
        m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, m_maxVelocity);

        if (OnPostMove != null)
            OnPostMove(m_rb.velocity);
    }
}
