using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private RectTransform m_startPanel;
    [SerializeField] private RectTransform m_ingamePanel;
    [SerializeField] private TMPro.TextMeshProUGUI m_pointsText;
    [SerializeField] private int m_points;

    [SerializeField] PlayingBall ball;

    [SerializeField] private GridLayoutGroup m_layout;

    public bool GameStarted { get; private set; }

    public int Points
    {
        get => m_points;
        set => m_points = value;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void StartGame(bool _started)
    {
        GameStarted = true;
        m_startPanel.gameObject.SetActive(false);
        FindObjectOfType<FieldGenerator>().GenerateBlocks();
        m_layout.enabled = false;
    }

    public void StopGame()
    {
        GameStarted = false;
        m_startPanel.gameObject.SetActive(true);
        m_layout.enabled = true;
        ball.transform.position = new Vector3(0, 0, 0);
        FindObjectOfType<FieldGenerator>().Delete();
        FindObjectOfType<FieldGenerator>().m_placedBlocks.Clear();
    }

    public void UpdatePoints(int _amountToAdd)
    {
        m_points += _amountToAdd;
        m_pointsText.SetText(m_points.ToString("0"));

        if (m_points > 0 && m_points % 10 == 0)
            PlayingBall.SetBallSpeed(2);
    }
}
