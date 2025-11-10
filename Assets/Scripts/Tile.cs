using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject m_Parent;
    public GameObject m_GeneratedTile;

    public void SetGeneratedTile(GameObject generatedTile)
    {
        m_GeneratedTile = generatedTile;
    }

    public void SetParent(GameObject parent)
    {
        m_Parent = parent;
    }
}
