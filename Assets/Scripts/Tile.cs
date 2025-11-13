using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject m_Parent;
    private GameObject m_MiniTile;
    private GameObject m_GeneratedTile;
    
    public void SetGeneratedTile(GameObject generatedTile)
    {
        m_GeneratedTile = generatedTile;
    }

    public void SetMiniTile(GameObject miniTile)
    {
        m_MiniTile = miniTile;
    }

    public void SetParent(GameObject parent)
    {
        m_Parent = parent;
    }

    public GameObject GetGeneratedTile()
    {
        return m_GeneratedTile;
    }

    public GameObject GetMiniTile()
    {
        return m_MiniTile;
    }
}
