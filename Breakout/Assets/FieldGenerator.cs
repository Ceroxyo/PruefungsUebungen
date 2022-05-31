using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_blocks = new List<GameObject>();

    public List<GameObject> m_placedBlocks = new List<GameObject>();

    private void Start()
    {
    }

    public void GenerateBlocks()
    {
        foreach (GameObject go in m_blocks)
        {
            for (int i = 0; i < 28; i++)
            {
               m_placedBlocks.Add(Instantiate(go, transform));
            }
        }
    }

    public void Delete()
    {
        foreach(GameObject go in m_placedBlocks)
        {
            Destroy(go);
        }
    }
}
