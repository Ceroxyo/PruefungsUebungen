using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 0;
    [SerializeField] private GameObject m_bulletPrefab = null;
    [SerializeField] private RectTransform m_gamePanel = null;
    [SerializeField] private int m_spawnChance = 4;

    [SerializeField] private Vector3 m_direction = Vector3.zero;

    public float MoveSpeed
    {
        get => m_moveSpeed;
        set => m_moveSpeed = value;
    }
    void Start()
    {
        m_direction = transform.TransformDirection(Vector3.left);

        StartCoroutine(Reflect());
        StartCoroutine(Roll());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_direction * Time.deltaTime * m_moveSpeed;

    }

    IEnumerator Reflect()
    {
        while (true)
        {
            yield return new WaitForSeconds(14f);
            m_direction = Vector3.Reflect(m_direction, -m_direction);
            transform.position += Vector3.down * 10;
        }
    }

    IEnumerator Roll()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit2D hit = new RaycastHit2D();
        Debug.DrawRay(transform.position + new Vector3(0, -11, 0), Vector3.down * 30, Color.red, 2.0f);
        hit = Physics2D.Raycast(transform.position + new Vector3(0, -11, 0), Vector2.down, 30);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Invader"))
                return;
        }
        else
        {
            int random = Random.Range(0, 101);
            if (random <= m_spawnChance)
            {
                Instantiate(m_bulletPrefab, transform.position + new Vector3(0,-20,0), Quaternion.identity, m_gamePanel);
            }
        }

    }
}
