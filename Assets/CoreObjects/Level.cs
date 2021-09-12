using UnityEngine;

public class Level : MonoBehaviour
{
    private int m_sheepInLevel = 0;
    public int SheepInLevel => m_sheepInLevel;

    private int m_sheepHerded = 0;
    public int SheepHerded => m_sheepHerded;

    private Sheep[] m_sheep;

    private void Start()
    {
        GameManager.Instance.CurrentLevel = this;
        m_sheep = FindObjectsOfType<Sheep>();

        m_sheepInLevel = m_sheep.Length;
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
