using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject m_Parent;
    private GameObject m_MiniTile;
    private GameObject m_GeneratedTile;

    public Material blank;
    public Material hover;
    public Material select;

    private bool m_IsSelected = false;

    public void SetMaterialOnSelect()
    {
        ToggleSelectTile();
        if (m_IsSelected)
            transform.GetChild(0).GetComponent<MeshRenderer>().material = select;
        else
            transform.GetChild(0).GetComponent<MeshRenderer>().material = blank;
    }

    public void SetMaterialOnHoverEnter()
    {
        if(!m_IsSelected)
            transform.GetChild(0).GetComponent<MeshRenderer>().material = hover;
    }

    public void SetMaterialOnHoverExit()
    {
        if (!m_IsSelected)
            transform.GetChild(0).GetComponent<MeshRenderer>().material = blank;
    }

    public void ToggleSelectTile()
    {
        m_IsSelected = !m_IsSelected;
    }

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

    public bool GetIsSelected()
    {
        return m_IsSelected;
    }
}
