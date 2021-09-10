using Rewired;
using UnityEngine;
using System.Collections;

public class PlayerCharacterBark : MonoBehaviour
{
    const int PLAYER_ID = 0;
    private Player m_player;

    [SerializeField] private AudioSource m_barkAudio;
    [SerializeField] private GameObject[] m_showNotBarking;
    [SerializeField] private GameObject[] m_showBarking;
    [SerializeField] private GameObject m_barkTextPrefab;
    [SerializeField] private Transform m_barkTextSpawnPosition;

    private float m_originalPitch;

    [SerializeField] [Range(0, 3)] private float m_barkPitchRandomRange;

    private Coroutine m_barkCoroutine;

    private void Start()
    {
        m_player = ReInput.players.GetPlayer(PLAYER_ID);
        m_originalPitch = m_barkAudio.pitch;
    }

    void Update()
    {
        if (m_player.GetButtonDown("Bark"))
        {
            if (m_barkCoroutine != null)
                StopCoroutine(m_barkCoroutine);

            m_barkCoroutine = StartCoroutine(Bark());
        }
    }

    private IEnumerator Bark()
    {
        ShowBarking();

        m_barkAudio.pitch = m_originalPitch + Random.Range(0, m_barkPitchRandomRange);
        m_barkAudio.Play();

        GameObject barkText = Instantiate(m_barkTextPrefab);
        barkText.transform.position = m_barkTextSpawnPosition.position;

        yield return new WaitUntil(() => !m_barkAudio.isPlaying);

        HideBarking();
    }

    private void ShowBarking()
    {
        foreach (GameObject obj in m_showBarking)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in m_showNotBarking)
        {
            obj.SetActive(false);
        }
    }

    private void HideBarking()
    {
        foreach (GameObject obj in m_showBarking)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in m_showNotBarking)
        {
            obj.SetActive(true);
        }
    }
}
