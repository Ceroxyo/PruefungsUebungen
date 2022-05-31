using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlight : MonoBehaviour
{
    [SerializeField] private float m_bulletSpeed = 2;

    enum BulletType
    {
        None,
        PlayerBullet,
        InvaderBullet
    }

    [SerializeField]
    private BulletType m_type = BulletType.None;

    void Update()
    {
        if (m_type == BulletType.PlayerBullet)
        {
            transform.position += transform.TransformDirection(Vector3.up) * Time.deltaTime * m_bulletSpeed;
        }
        else
            transform.position += transform.TransformDirection(Vector2.down) * Time.deltaTime * m_bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.m_type == BulletType.PlayerBullet)
        {

            if (collision.gameObject.CompareTag("Invader"))
            {
                GameManager.Instance.Remove(collision.gameObject);
                Destroy(collision.gameObject);
                GameManager.Instance.AddPoints(1);
                Destroy(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
        else
        {
            if(collision.gameObject.CompareTag("PlayerBase"))
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            if(collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().Health--;
            }
        }

    }
}
