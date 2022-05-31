using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab = null;
    [SerializeField] private Transform m_bulletSpawn = null;
    [SerializeField] private RectTransform m_gamePanel = null;

    [SerializeField] private float m_moveSpeed = 0;
    [SerializeField] private int m_health = 5;

    public int Health
    {
        get => m_health;
        set
        {
            m_health = value;

            if (m_health <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                GameManager.Instance.GameStarted = false;
                GameManager.Instance.SetHighscore(GameManager.Instance.Points);
            }
        }
    }

    private Vector3 m_moveVector = Vector3.zero;

    private void Update()
    {
        MovePlayer();
        Shoot();
    }

    private void MovePlayer()
    {
        m_moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        transform.position += m_moveVector * Time.deltaTime * m_moveSpeed;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(m_bulletPrefab, m_bulletSpawn.position, Quaternion.identity, m_gamePanel);
    }
}
