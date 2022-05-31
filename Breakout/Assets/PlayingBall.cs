using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingBall : MonoBehaviour
{

    private static float m_ballSpeed = 10;
    [SerializeField] private RectTransform m_playingField;
    [SerializeField] private RectTransform m_thisRect;
    [SerializeField] private Vector3 m_direction;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, LayerMask.NameToLayer("Playfield"), true);
    }

    private void Start()
    {
        m_thisRect = GetComponent<RectTransform>();
        m_direction = transform.TransformDirection(Vector3.down - Vector3.left) * Time.deltaTime * m_ballSpeed;
    }

    private void Update()
    {
        if (!GameManager.Instance.GameStarted) return;


        if (m_thisRect.anchoredPosition.y + (m_thisRect.rect.height *0.5f) < -m_playingField.rect.height * 0.5f)
        {
            GameManager.Instance.StartGame(false);
        }

        transform.position += m_direction * Time.deltaTime * m_ballSpeed;


        if (m_thisRect.anchoredPosition.x - (m_thisRect.rect.width * 0.5f) < -m_playingField.rect.width * 0.5f)
            m_direction = Vector3.Reflect(m_direction, Vector3.left);

        if (m_thisRect.anchoredPosition.x + (m_thisRect.rect.width * 0.5f) > m_playingField.rect.width * 0.5f)
            m_direction = Vector3.Reflect(m_direction, Vector3.right);
    }

    public static void SetBallSpeed(float _speed)
    {
        m_ballSpeed += _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out BlockInfo info))
        {
            GameManager.Instance.UpdatePoints(info.Points);
            m_direction = Vector3.Reflect(m_direction, collision.GetContact(0).normal);
            Destroy(collision.gameObject);
            return;
            //Destroy(collision.gameObject);
        }


        if(collision.collider.gameObject.CompareTag("Player"))
        {
            //Vector3.up
            m_direction = Vector3.Reflect(m_direction, collision.GetContact(0).normal);
        }
    }
}
