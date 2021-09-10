using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager Instance => m_instance;

    private Rewired.InputManager m_inputManager;
    public Rewired.InputManager InputManager => m_inputManager;

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogError("Another GameManager instance was just instantiated!", gameObject);

            return;
        }

        Init();

        DontDestroyOnLoad(gameObject);
        m_instance = this;
    }

    private void Init()
    {
        m_inputManager = GetComponentInChildren<Rewired.InputManager>();
    }
}
