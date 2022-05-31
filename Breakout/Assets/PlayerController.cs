using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;

    [SerializeField] private Vector3 m_direction;

    private RectTransform m_thisRect;

    private void Start()
    {
        m_thisRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!GameManager.Instance.GameStarted) return;
        Move();
    }

    private void Move()
    {
        m_direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);


        transform.position += m_direction * Time.deltaTime * m_speed;
        if (m_thisRect.anchoredPosition.x - (m_thisRect.rect.width * 0.5f) < -364 || m_thisRect.anchoredPosition.x + (m_thisRect.rect.width * 0.5f) > 364)
            transform.position -= m_direction * Time.deltaTime * m_speed;


    }
}
