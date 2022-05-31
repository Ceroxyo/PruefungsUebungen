using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMPro.TextMeshProUGUI m_pointsText = null;
    [SerializeField] private TMPro.TextMeshProUGUI m_highscoreText = null;

    [SerializeField] private RectTransform m_startPanel = null;
    [SerializeField] private RectTransform m_gamePanel = null;
    [SerializeField] private RectTransform m_pausePanel = null;
    [SerializeField] private GameObject m_player = null;

    [SerializeField] private List<GameObject> m_invaders = new List<GameObject>();

    private int m_previousCount = 0;

    public List<GameObject> Invaders
    {
        get => m_invaders;
        set
        {
            m_invaders = value;
            if (m_invaders.Count % 11 == 0)
            {
                foreach (GameObject go in Invaders)
                {
                    go.GetComponent<InvaderController>().MoveSpeed *= 2;
                }
            }

        }
    }

    public Vector3 PlayerPos { get; set; }



    public bool GameStarted { get; set; } = false;
    public int Points { get; set; } = 0;
    public int Highscore { get; set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        m_previousCount = Invaders.Count;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();

        if (GameStarted)
        {
            PlayerPos = m_player.transform.position;
        }
    }

    [ContextMenu("Fill InvaderList")]
    public void FillList()
    {
        m_invaders = GameObject.FindGameObjectsWithTag("Invader").ToList();
    }

    public void StartGame()
    {
        GameStarted = true;
        m_startPanel.gameObject.SetActive(false);
        m_gamePanel.gameObject.SetActive(true);
    }

    public void StopGame()
    {
        GameStarted = false;
        ResetGame();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void PauseGame()
    {
        if (Time.timeScale < 1)
        {
            Resume();
            return;
        }

        Time.timeScale = 0;
        m_pausePanel.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        m_pausePanel.gameObject.SetActive(false);
    }

    public void SetHighscore(int _score)
    {
        Highscore = _score;
        m_highscoreText.SetText(Highscore.ToString("0"));
    }

    public void AddPoints(int _points)
    {
        Points += _points;
        m_pointsText.SetText(Points.ToString("0"));
    }

    public void ResetGame()
    {
        Points = 0;
        m_pointsText.SetText(string.Empty);
        m_gamePanel.gameObject.SetActive(false);
        m_startPanel.gameObject.SetActive(true);
    }

    public void Remove(GameObject _objToRemove)
    {
        if (Invaders.Contains(_objToRemove))
            Invaders.Remove(_objToRemove);
    }

    public void ExitInGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
