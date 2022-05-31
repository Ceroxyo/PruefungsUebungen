using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour
{
    public int Points { get => m_points; set => m_points = value; }

    [SerializeField] private int m_points = 0;
}
