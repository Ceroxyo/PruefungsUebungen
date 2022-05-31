using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{
    [SerializeField] private int m_health;

    public int Health
    {
        get => m_health;
        set
        {
            if(m_health < 0)
            {
                Destroy(this.gameObject);
                return;
            }

            m_health = value;
        }
    }

}
