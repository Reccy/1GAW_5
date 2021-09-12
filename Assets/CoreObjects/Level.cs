using UnityEngine;
using Rewired;

public class Level : MonoBehaviour
{
    private int m_sheepInLevel = 0;
    public int SheepInLevel => m_sheepInLevel;

    private int m_sheepHerded = 0;
    public int SheepHerded => m_sheepHerded;

    public bool LevelComplete => m_sheepHerded == m_sheepInLevel;

    private Sheep[] m_sheep;

    private Player m_player;

    private void Start()
    {
        GameManager.Instance.CurrentLevel = this;
        m_sheep = FindObjectsOfType<Sheep>();

        m_sheepInLevel = m_sheep.Length;

        m_player = ReInput.players.GetPlayer(0);
    }

    private void FixedUpdate()
    {
        int herded = 0;

        foreach (Sheep sheep in m_sheep)
        {
            if (sheep.IsVibing)
                herded += 1;
        }

        m_sheepHerded = herded;
    }
}
