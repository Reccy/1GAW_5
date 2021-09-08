using UnityEngine.SceneManagement;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    private const int INIT_GAME_INDEX = 0;
    private const int FIRST_SCENE_INDEX = 1;

    private static bool m_initialized = false;

    private bool IsInitScene => SceneManager.GetActiveScene().buildIndex == INIT_GAME_INDEX;

    private void Log(string log) => Debug.Log("[GAME INIT] " + log);

    private void Awake()
    {
        if (m_initialized)
            return;

        Log("Init Loader Started");

        m_initialized = true;

        Init();
    }

    private void Init()
    {
        if (IsInitScene)
        {
            Log("Loading First In-Game Scene");
            SceneManager.LoadScene(FIRST_SCENE_INDEX);
        }
        else
        {
            Log("Loading Init Scene");
            SceneManager.LoadScene(INIT_GAME_INDEX);
        }

        FinishInit();
    }

    private void FinishInit()
    {
        Log("Init Loader Finished");

        Destroy(gameObject);
    }
}
